#region 文件信息和作者

/*************************************************************************************
      * CLR 版本:   4.0.30319.42000
      * 类 名 称:   DictionaryExtensions
      * 机器名称:   LVJUNLEI-PC
      * 命名空间:   Utility.Extensions
      * 文 件 名:   DictionaryExtensions
      * 创建时间:   16.5.5 13:29:17
      * 作   者:    LvJunlei

  * 创建年份:       2016
      * 修改时间:
      * 修 改 人:
*************************************************************************************
 * Copyright @ 凯亚开发部 2016. All rights reserved.
*************************************************************************************/
#endregion

using System.Collections.Generic;

namespace Utility.Extensions
{
    /// <summary>
    /// Dictionary扩展集合
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// 尝试将键和值添加到字典中：如果不存在，才添加；存在，不添加也不抛导常
        /// </summary>
        public static Dictionary<TKey, TValue> TryAdd<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            if (!dict.ContainsKey(key))
            {
                dict.Add(key, value);
            }
            return dict;
        }

        /// <summary>
        /// 将键和值添加或替换到字典中：如果不存在，则添加；存在，则更新键值
        /// </summary>
        public static Dictionary<TKey, TValue> AddOrUpdate<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            if (dict.ContainsKey(key))
            {
                dict[key] = value;
            }
            else
            {
                dict.Add(key, value);
            }

            return dict;
        }

        /// <summary>
        /// 获取与指定的键相关联的值，如果没有则返回输入的默认值
        /// </summary>
        public static TValue GetValue<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue defaultValue = default(TValue))
        {
            return dict.ContainsKey(key) ? dict[key] : defaultValue;
        }

        /// <summary>
        /// 向字典中批量添加键值对
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="values"></param>
        /// <param name="updateExisted">如果已存在，是否替换</param>
        public static Dictionary<TKey, TValue> AddRange<TKey, TValue>(this Dictionary<TKey, TValue> dict, IEnumerable<KeyValuePair<TKey, TValue>> values, bool updateExisted)
        {
            foreach (var item in values)
            {
                if (!dict.ContainsKey(item.Key) || updateExisted)
                    dict[item.Key] = item.Value;
            }
            return dict;
        }

        /// <summary>
        /// 把字典的Value集合转换为List集合
        /// </summary>
        /// <typeparam name="TKey">字典key类型</typeparam>
        /// <typeparam name="TValue">字典值类型</typeparam>
        /// <param name="dict">字典</param>
        /// <returns></returns>
        public static List<TValue> ToList<T,TKey, TValue>(this Dictionary<TKey, TValue> dict)
        {
            var vs = new List<TValue>();
            foreach (var value in dict.Values)
            {
                vs.Add(value);
            }
            return vs;
        }
    }
}
