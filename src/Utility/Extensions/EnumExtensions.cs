#region EnumExtensions 文件信息
/***********************************************************
**文 件 名：EnumExtensions 
**命名空间：Utility.Extensions 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2016-09-20 13:32:34 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Utility.Extensions
{
    /// <summary>
    /// Enum扩展类
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// 获取Enum类型的DescriptionAttribute标注的内容信息
        /// </summary>
        /// <param name="this">Enum属性本身</param>
        /// <returns>标注内容</returns>
        public static string GetDescription(this Enum @this)
        {
            var name = @this.ToString();
            var field = @this.GetType().GetField(name);
            if (field == null)
            {
                return name;
            }

            var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute), false);
            return attribute == null ? field.Name : ((DescriptionAttribute)attribute).Description;
        }

        /// <summary>
        /// 获取枚举列表
        /// </summary>
        /// <param name="enumType">枚举的类型</param>
        /// <returns>枚举列表</returns>
        public static Dictionary<int, string> ToDictionary(this Type enumType)
        {
            var dic = new Dictionary<int, string>();
            try
            {
                var fd = enumType.GetFields();
                for (var index = 1; index < fd.Length; ++index)
                {
                    var info = fd[index];
                    var fieldValue = Enum.Parse(enumType, fd[index].Name);
                    var attrs = info.GetCustomAttributes(typeof(DescriptionAttribute), false);
                    foreach (DescriptionAttribute attr in attrs)
                    {
                        var key = (int)fieldValue;
                        if (key == -100) continue;
                        var value = attr.Description;
                        dic.Add(key, value);
                    }
                }
                return dic;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 获取枚举列表
        /// </summary>
        /// <param name="enumType">枚举的类型</param>
        /// <returns>枚举列表</returns>
        public static Dictionary<string, string> ToStringDictionary(this Type enumType)
        {
            var dic = new Dictionary<string, string>();
            try
            {
                var fd = enumType.GetFields();
                for (var index = 1; index < fd.Length; ++index)
                {
                    var info = fd[index];
                    var fieldValue = Enum.Parse(enumType, fd[index].Name);
                    var attrs = info.GetCustomAttributes(typeof(DescriptionAttribute), false);
                    foreach (DescriptionAttribute attr in attrs)
                    {
                        var key = fieldValue.ToString();
                        if (key == "-100")
                        {
                            continue;
                        }
                        var value = attr.Description;
                        dic.Add(key, value);
                    }
                }
                return dic;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 获取枚举列表
        /// </summary>
        /// <param name="enumType">枚举的类型</param>
        /// <returns>枚举列表</returns>
        public static Dictionary<int, string> GetEnumList(this Type enumType)
        {
            var dic = new Dictionary<int, string>();
            try
            {
                var fd = enumType.GetFields();
                for (var index = 1; index < fd.Length; ++index)
                {
                    var info = fd[index];
                    var fieldValue = Enum.Parse(enumType, fd[index].Name);
                    var attrs = info.GetCustomAttributes(typeof(DescriptionAttribute), false);
                    foreach (DescriptionAttribute attr in attrs)
                    {
                        var key = (int)fieldValue;
                        if (key == -100) continue;
                        var value = attr.Description;
                        dic.Add(key, value);
                    }
                }
                return dic;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 获取枚举名称
        /// </summary>
        /// <param name="enumType">枚举的类型</param>
        /// <param name="id">枚举值</param>
        /// <returns>如果枚举值存在，返回对应的枚举名称，否则，返回空字符</returns>
        public static string GetEnumTextById(this Type enumType, int id)
        {
            var ret = string.Empty;
            try
            {
                var dic = enumType.GetEnumList();
                foreach (var item in dic)
                {
                    if (item.Key != id) continue;
                    ret = item.Value;
                    break;
                }
                return ret;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 根据枚举值获取对应中文描述
        /// </summary>
        /// <param name="enumValue">枚举值</param>
        /// <returns>枚举值中文描述</returns>
        public static string GetEnumTextByEnum(this object enumValue)
        {
            var ret = string.Empty;
            if ((int)enumValue == -1) return ret;
            try
            {
                var dic = enumValue.GetType().GetEnumList();
                foreach (var item in dic)
                {
                    if (item.Key != (int)enumValue) continue;
                    ret = item.Value;
                    break;
                }
                return ret;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 获取枚举名称
        /// </summary>
        /// <param name="enumType">枚举的类型</param>
        /// <param name="index">枚举值的位置编号</param>
        /// <returns>如果枚举值存在，返回对应的枚举名称，否则，返回空字符</returns>
        public static string GetEnumTextByIndex(this Type enumType, int index)
        {
            var ret = string.Empty;
            var dic = enumType.GetEnumList();
            if (index < 0 || index > dic.Count)
                return ret;
            var i = 0;
            foreach (var item in dic)
            {
                if (i == index)
                {
                    ret = item.Value;
                    break;
                }
                i++;
            }

            return ret;
        }

        /// <summary>
        /// 获取枚举值
        /// </summary>
        /// <param name="enumType">枚举的类型</param>
        /// <param name="name">枚举名称</param>
        /// <returns>如果枚举名称存在，返回对应的枚举值，否则，返回-1</returns>
        public static int GetEnumIdByName(this Type enumType, string name)
        {
            var ret = -1;
            if (string.IsNullOrEmpty(name))
                return ret;
            var dic = enumType.GetEnumList();
            foreach (var item in dic)
            {
                if (string.Compare(item.Value, name, StringComparison.Ordinal) != 0) continue;
                ret = item.Key;
                break;
            }
            return ret;
        }

        /// <summary>
        /// 获取名字对应枚举值
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="name">枚举名称</param>
        /// <returns></returns>
        public static T GetEnumIdByName<T>(string name) where T : new()
        {
            var type = typeof(T);

            var enumItem = (T)TypeDescriptor.GetConverter(type).ConvertFrom("-1");
            if (string.IsNullOrEmpty(name))
                return enumItem;

            try
            {
                var fd = typeof(T).GetFields();
                for (var index = 1; index < fd.Length; ++index)
                {
                    var info = fd[index];
                    var fieldValue = Enum.Parse(type, fd[index].Name);
                    var attrs = info.GetCustomAttributes(typeof(DescriptionAttribute), false);
                    if (attrs.Length != 1) continue;
                    var attr = (DescriptionAttribute)attrs[0];
                    if (!name.Equals(attr.Description)) continue;
                    enumItem = (T)fieldValue;
                    break;
                }
                return enumItem;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 获取枚举值所在的位置编号
        /// </summary>
        /// <param name="enumType">枚举的类型</param>
        /// <param name="name">枚举名称</param>
        /// <returns>如果枚举名称存在，返回对应的枚举值的位置编号，否则，返回-1</returns>
        public static int GetEnumIndexByName(Type enumType, string name)
        {
            var ret = -1;

            if (string.IsNullOrEmpty(name))
                return ret;

            var dic = GetEnumList(enumType);
            var i = 0;
            foreach (var item in dic)
            {
                if (string.Compare(item.Value, name, StringComparison.Ordinal) == 0)
                {
                    ret = i;
                    break;
                }
                i++;
            }

            return ret;
        }
    }
}
