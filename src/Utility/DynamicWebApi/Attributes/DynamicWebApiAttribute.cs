#region DynamicWebApiAttribute 文件信息
/***********************************************************
**文 件 名：DynamicWebApiAttribute 
**命名空间：Utility.DynamicWebApi.Attributes 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2019/6/13 9:00:03 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明：版权所有，盗版必究
************************************************************/
#endregion

using System;
using System.Reflection;
using Utility.DynamicWebApi.Helpers;

namespace Utility.DynamicWebApi
{
    /// <summary>
    /// 动态生成 WebAPI 标签
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class)]
    public class DynamicWebApiAttribute : Attribute
    {
        /// <summary>
        /// Equivalent to AreaName
        /// </summary>
        public string Module { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        internal static bool IsExplicitlyEnabledFor(Type type)
        {
            var remoteServiceAttr = type.GetTypeInfo().GetSingleAttributeOrNull<DynamicWebApiAttribute>();
            return remoteServiceAttr != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        internal static bool IsExplicitlyDisabledFor(Type type)
        {
            var remoteServiceAttr = type.GetTypeInfo().GetSingleAttributeOrNull<DynamicWebApiAttribute>();
            return remoteServiceAttr != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        internal static bool IsMetadataExplicitlyEnabledFor(Type type)
        {
            var remoteServiceAttr = type.GetTypeInfo().GetSingleAttributeOrNull<DynamicWebApiAttribute>();
            return remoteServiceAttr != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        internal static bool IsMetadataExplicitlyDisabledFor(Type type)
        {
            var remoteServiceAttr = type.GetTypeInfo().GetSingleAttributeOrNull<DynamicWebApiAttribute>();
            return remoteServiceAttr != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        internal static bool IsMetadataExplicitlyDisabledFor(MethodInfo method)
        {
            var remoteServiceAttr = method.GetSingleAttributeOrNull<DynamicWebApiAttribute>();
            return remoteServiceAttr != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        internal static bool IsMetadataExplicitlyEnabledFor(MethodInfo method)
        {
            var remoteServiceAttr = method.GetSingleAttributeOrNull<DynamicWebApiAttribute>();
            return remoteServiceAttr != null;
        }
    }
}
