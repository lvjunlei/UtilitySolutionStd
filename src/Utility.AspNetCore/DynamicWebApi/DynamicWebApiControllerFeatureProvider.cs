#region DynamicWebApiControllerFeatureProvider 文件信息
/***********************************************************
**文 件 名：DynamicWebApiControllerFeatureProvider 
**命名空间：Utility.DynamicWebApi 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2019/6/13 9:13:01 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明：版权所有，盗版必究
************************************************************/
#endregion

using Microsoft.AspNetCore.Mvc.Controllers;
using System.Reflection;
using Utility.DynamicWebApi.Helpers;

namespace Utility.DynamicWebApi
{
    /// <summary>
    /// DynamicWebApiControllerFeatureProvider
    /// </summary>
    public class DynamicWebApiControllerFeatureProvider : ControllerFeatureProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeInfo"></param>
        /// <returns></returns>
        protected override bool IsController(TypeInfo typeInfo)
        {
            var type = typeInfo.AsType();

            if (!typeof(IDynamicWebApi).IsAssignableFrom(type) ||
                !typeInfo.IsPublic || typeInfo.IsAbstract || typeInfo.IsGenericType)
            {
                return false;
            }


            var attr = ReflectionExtensions.GetSingleAttributeOrDefaultByFullSearch<DynamicWebApiAttribute>(typeInfo);

            if (attr == null)
            {
                return false;
            }

            if (ReflectionExtensions.GetSingleAttributeOrDefaultByFullSearch<NonDynamicWebApiAttribute>(typeInfo) != null)
            {
                return false;
            }

            return true;
        }
    }
}

