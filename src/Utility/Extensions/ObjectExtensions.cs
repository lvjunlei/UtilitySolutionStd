using System;

namespace Utility.Extensions
{
    /// <summary>
    /// ObjectExtensions
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// 获取指定名称的属性值
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="propertyName">属性名称</param>
        /// <returns>属性值</returns>
        public static object GetPropertyValue(this object obj, string propertyName)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
            return obj.GetType()
                .GetProperty(propertyName)
                ?.GetValue(obj, null);
        }

        /// <summary>
        /// 获取指定名称的属性值
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="propertyName">属性名称</param>
        /// <returns>属性值</returns>
        public static T GetPropertyValue<T>(this object obj, string propertyName)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
            var v = obj.GetType()
                .GetProperty(propertyName)
                ?.GetValue(obj, null);
            if (v == null)
            {
                return default;
            }
            return (T)v;
        }

        /// <summary>
        /// 设置指定属性名称的属性值
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="propertyName">属性名称</param>
        /// <param name="value">属性值</param>
        /// <returns></returns>
        public static bool SetPropertyValue(this object obj, string propertyName, object value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
            var t = obj.GetType();
            var property = t.GetProperty(propertyName);
            if (property == null)
            {
                return false;
            }
            var v = Convert.ChangeType(value, property.PropertyType);
            property.SetValue(obj, v, null);
            return true;
        }
    }
}