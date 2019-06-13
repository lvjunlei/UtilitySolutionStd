#region ReflectionHelper 文件信息
/***********************************************************
**文 件 名：ReflectionHelper 
**命名空间：Utility.DynamicWebApi.Helpers 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2019/6/13 9:04:35 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明：版权所有，盗版必究
************************************************************/
#endregion

using System;
using System.Linq;
using System.Reflection;

namespace Utility.DynamicWebApi.Helpers
{
    /// <summary>
    /// ReflectionExtensions
    /// </summary>
    public static class ReflectionExtensions
    {
        public static TAttribute GetSingleAttributeOrDefaultByFullSearch<TAttribute>(TypeInfo info)
            where TAttribute : Attribute
        {
            var attributeType = typeof(TAttribute);
            if (info.IsDefined(attributeType, true))
            {
                return info.GetCustomAttributes(attributeType, true).Cast<TAttribute>().First();
            }
            else
            {
                foreach (var implInter in info.ImplementedInterfaces)
                {
                    var res = GetSingleAttributeOrDefaultByFullSearch<TAttribute>(implInter.GetTypeInfo());

                    if (res != null)
                    {
                        return res;
                    }
                }
            }

            return null;
        }

        public static TAttribute GetSingleAttributeOrDefault<TAttribute>(MemberInfo memberInfo, TAttribute defaultValue = default(TAttribute), bool inherit = true)
       where TAttribute : Attribute
        {
            var attributeType = typeof(TAttribute);
            if (memberInfo.IsDefined(typeof(TAttribute), inherit))
            {
                return memberInfo.GetCustomAttributes(attributeType, inherit).Cast<TAttribute>().First();
            }

            return defaultValue;
        }


        /// <summary>
        /// Gets a single attribute for a member.
        /// </summary>
        /// <typeparam name="TAttribute">Type of the attribute</typeparam>
        /// <param name="memberInfo">The member that will be checked for the attribute</param>
        /// <param name="inherit">Include inherited attributes</param>
        /// <returns>Returns the attribute object if found. Returns null if not found.</returns>
        public static TAttribute GetSingleAttributeOrNull<TAttribute>(this MemberInfo memberInfo, bool inherit = true)
            where TAttribute : Attribute
        {
            if (memberInfo == null)
            {
                throw new ArgumentNullException(nameof(memberInfo));
            }

            var attrs = memberInfo.GetCustomAttributes(typeof(TAttribute), inherit).ToArray();
            if (attrs.Length > 0)
            {
                return (TAttribute)attrs[0];
            }

            return default(TAttribute);
        }


        public static TAttribute GetSingleAttributeOfTypeOrBaseTypesOrNull<TAttribute>(this Type type, bool inherit = true)
            where TAttribute : Attribute
        {
            var attr = type.GetTypeInfo().GetSingleAttributeOrNull<TAttribute>();
            if (attr != null)
            {
                return attr;
            }

            if (type.GetTypeInfo().BaseType == null)
            {
                return null;
            }

            return type.GetTypeInfo().BaseType.GetSingleAttributeOfTypeOrBaseTypesOrNull<TAttribute>(inherit);
        }

    }
}
