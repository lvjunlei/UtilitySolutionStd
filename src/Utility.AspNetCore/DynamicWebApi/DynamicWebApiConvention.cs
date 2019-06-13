#region DynamicWebApiConvention 文件信息
/***********************************************************
**文 件 名：DynamicWebApiConvention 
**命名空间：Utility.DynamicWebApi 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2019/6/13 9:15:11 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明：版权所有，盗版必究
************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Utility.DynamicWebApi.Helpers;
using Utility.Extensions;

namespace Utility.DynamicWebApi
{
    public class DynamicWebApiConvention : IApplicationModelConvention
    {
        private readonly IServiceCollection _services;
        public DynamicWebApiConvention(IServiceCollection services)
        {
            this._services = services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="application"></param>
        public void Apply(ApplicationModel application)
        {
            foreach (var controller in application.Controllers)
            {
                var type = controller.ControllerType.AsType();

                var dynamicWebApiAttr = ReflectionExtensions.GetSingleAttributeOrDefaultByFullSearch<DynamicWebApiAttribute>(type.GetTypeInfo());
                if (typeof(IDynamicWebApi).GetTypeInfo().IsAssignableFrom(type))
                {
                    controller.ControllerName = controller.ControllerName.TrimPostFix(AppConsts.ControllerPostfixes.ToArray());
                    ConfigureArea(controller, dynamicWebApiAttr);
                    ConfigureDynamicWebApi(controller, dynamicWebApiAttr);
                }
                else
                {
                    if (dynamicWebApiAttr != null)
                    {
                        ConfigureArea(controller, dynamicWebApiAttr);
                        ConfigureDynamicWebApi(controller, dynamicWebApiAttr);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="attr"></param>
        private void ConfigureArea(ControllerModel controller, DynamicWebApiAttribute attr)
        {
            if (attr == null)
            {
                throw new ArgumentException(nameof(attr));
            }

            if (!controller.RouteValues.ContainsKey("area"))
            {
                if (!string.IsNullOrEmpty(attr.Module))
                {
                    controller.RouteValues["area"] = attr.Module;
                }
                else if (!string.IsNullOrEmpty(AppConsts.DefaultAreaName))
                {
                    controller.RouteValues["area"] = AppConsts.DefaultAreaName;
                }
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="controllerAttr"></param>
        private void ConfigureDynamicWebApi(ControllerModel controller, DynamicWebApiAttribute controllerAttr)
        {
            ConfigureApiExplorer(controller);
            ConfigureSelector(controller, controllerAttr);
            ConfigureParameters(controller);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        private void ConfigureParameters(ControllerModel controller)
        {
            foreach (var action in controller.Actions)
            {
                foreach (var para in action.Parameters)
                {
                    if (para.BindingInfo != null)
                    {
                        continue;
                    }

                    if (!para.ParameterInfo.ParameterType.IsPrimitiveExtendedIncludingNullable())
                    {
                        if (CanUseFormBodyBinding(action, para))
                        {
                            para.BindingInfo = BindingInfo.GetBindingInfo(new[] { new FromBodyAttribute() });
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private bool CanUseFormBodyBinding(ActionModel action, ParameterModel parameter)
        {
            if (AppConsts.FormBodyBindingIgnoredTypes.Any(t => t.IsAssignableFrom(parameter.ParameterInfo.ParameterType)))
            {
                return false;
            }

            foreach (var selector in action.Selectors)
            {
                if (selector.ActionConstraints == null)
                {
                    continue;
                }

                foreach (var actionConstraint in selector.ActionConstraints)
                {
                    var httpMethodActionConstraint = actionConstraint as HttpMethodActionConstraint;
                    if (httpMethodActionConstraint == null)
                    {
                        continue;
                    }

                    if (httpMethodActionConstraint.HttpMethods.All(hm => hm.IsIn("GET", "DELETE", "TRACE", "HEAD")))
                    {
                        return false;
                    }
                }
            }

            return true;
        }


        #region ConfigureApiExplorer

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        private void ConfigureApiExplorer(ControllerModel controller)
        {
            if (controller.ApiExplorer.GroupName.IsNullOrEmpty())
            {
                controller.ApiExplorer.GroupName = controller.ControllerName;
            }

            if (controller.ApiExplorer.IsVisible == null)
            {
                controller.ApiExplorer.IsVisible = true;
            }

            foreach (var action in controller.Actions)
            {
                ConfigureApiExplorer(action);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        private void ConfigureApiExplorer(ActionModel action)
        {
            if (action.ApiExplorer.IsVisible == null)
            {
                action.ApiExplorer.IsVisible = true;
            }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="controllerAttr"></param>
        private void ConfigureSelector(ControllerModel controller, DynamicWebApiAttribute controllerAttr)
        {
            RemoveEmptySelectors(controller.Selectors);

            if (controller.Selectors.Any(selector => selector.AttributeRouteModel != null))
            {
                return;
            }

            var areaName = string.Empty;

            if (controllerAttr != null)
            {
                areaName = controllerAttr.Module;
            }

            foreach (var action in controller.Actions)
            {
                ConfigureSelector(areaName, controller.ControllerName, action);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="areaName"></param>
        /// <param name="controllerName"></param>
        /// <param name="action"></param>
        private void ConfigureSelector(string areaName, string controllerName, ActionModel action)
        {
            RemoveEmptySelectors(action.Selectors);

            var nonAttr = ReflectionExtensions.GetSingleAttributeOrDefault<NonDynamicWebApiAttribute>(action.ActionMethod);

            if (nonAttr != null)
            {
                return;
            }

            if (!action.Selectors.Any())
            {
                AddAppServiceSelector(areaName, controllerName, action);
            }
            else
            {
                NormalizeSelectorRoutes(areaName, controllerName, action);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="areaName"></param>
        /// <param name="controllerName"></param>
        /// <param name="action"></param>
        private void AddAppServiceSelector(string areaName, string controllerName, ActionModel action)
        {
            string verb;
            var verbKey = action.ActionName.ToPascalOrCamelCaseFirstWord().ToLower();
            verb = AppConsts.HttpVerbs.ContainsKey(verbKey) ? AppConsts.HttpVerbs[verbKey] : AppConsts.DefaultHttpVerb;

            action.ActionName = GetRestFulActionName(action.ActionName);
            var appServiceSelectorModel = new SelectorModel
            {
                AttributeRouteModel = CreateActionRouteModel(areaName, controllerName, action.ActionName)
            };

            appServiceSelectorModel.ActionConstraints.Add(new HttpMethodActionConstraint(new[] { verb }));

            action.Selectors.Add(appServiceSelectorModel);
        }

        /// <summary>
        /// Processing action name
        /// </summary>
        /// <param name="actionName"></param>
        /// <returns></returns>
        private static string GetRestFulActionName(string actionName)
        {
            // Remove Postfix
            actionName = actionName.TrimPostFix(AppConsts.ActionPostfixes.ToArray());

            // Remove Prefix
            var verbKey = actionName.ToPascalOrCamelCaseFirstWord().ToLower();
            if (AppConsts.HttpVerbs.ContainsKey(verbKey))
            {
                if (actionName.Length == verbKey.Length)
                {
                    return "";
                }
                else
                {
                    return actionName.Substring(verbKey.Length);
                }
            }
            else
            {
                return actionName;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="areaName"></param>
        /// <param name="controllerName"></param>
        /// <param name="action"></param>
        private static void NormalizeSelectorRoutes(string areaName, string controllerName, ActionModel action)
        {
            action.ActionName = GetRestFulActionName(action.ActionName);
            foreach (var selector in action.Selectors)
            {
                selector.AttributeRouteModel = selector.AttributeRouteModel == null ?
                    CreateActionRouteModel(areaName, controllerName, action.ActionName) :
                    AttributeRouteModel.CombineAttributeRouteModel(CreateActionRouteModel(areaName, controllerName, ""), selector.AttributeRouteModel);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="areaName"></param>
        /// <param name="controllerName"></param>
        /// <param name="actionName"></param>
        /// <returns></returns>
        private static AttributeRouteModel CreateActionRouteModel(string areaName, string controllerName, string actionName)
        {
            var routeStr =
                $"{AppConsts.DefaultApiPreFix}/{areaName}/{controllerName}/{actionName}".Replace("//", "/");
            return new AttributeRouteModel(new RouteAttribute(routeStr));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selectors"></param>
        private static void RemoveEmptySelectors(IList<SelectorModel> selectors)
        {
            selectors
                .Where(IsEmptySelector)
                .ToList()
                .ForEach(s => selectors.Remove(s));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        private static bool IsEmptySelector(SelectorModel selector)
        {
            return selector.AttributeRouteModel == null && selector.ActionConstraints.IsNullOrEmpty();
        }
    }
}
