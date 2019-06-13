#region ICache 文件信息
/***********************************************************
**文 件 名：ICache 
**命名空间：Utility.Caches 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2019-03-14 16:17:36 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion

using System;

namespace Utility.Cache
{
    /// <summary>
    /// 基础缓存服务接口定义
    /// </summary>
    public interface ICacheService
    {
        #region Exists

        /// <summary>
        /// 是否存在指定键的缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        bool Exists(string key);

        #endregion

        #region Get

        /// <summary>
        /// 从缓存中获取数据
        /// </summary>
        /// <typeparam name="T">缓存数据类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <returns></returns>
        T Get<T>(string key);

        /// <summary>
        /// 从缓存中获取数据
        /// </summary>
        /// <typeparam name="T">缓存数据类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        T TryGet<T>(string key, T defaultValue = default(T));

        /// <summary>
        /// 从缓存中获取数据，如果不存在，则执行获取数据操作并添加到缓存中
        /// </summary>
        /// <typeparam name="T">缓存数据类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="func">获取数据操作</param>
        T Get<T>(string key, Func<T> func);

        #endregion

        #region Add

        /// <summary>
        /// 当缓存数据不存在则添加，已存在不会添加，添加成功返回true
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">值</param>
        bool Add(string key, object value);

        /// <summary>
        /// 当缓存数据不存在则添加，已存在不会添加，添加成功返回true
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">值</param>
        /// <param name="expiration">过期时间间隔</param>
        bool Add(string key, object value, TimeSpan expiration);

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">缓存Value</param>
        /// <param name="expiration">缓存时长</param>
        /// <param name="isSliding">是否滑动过期（如果在过期时间内有操作，则以当前时间点延长过期时间）</param>
        /// <returns></returns>
        bool Add(string key, object value, TimeSpan? expiration, bool isSliding = true);

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">缓存Value</param>
        /// <param name="expirationSliding">滑动过期时长（如果在过期时间内有操作，则以当前时间点延长过期时间）</param>
        /// <param name="expirationAbsoulte">绝对过期时长</param>
        /// <returns></returns>
        bool Add(string key, object value, TimeSpan expirationSliding, TimeSpan expirationAbsoulte);

        #endregion

        #region TryAdd

        /// <summary>
        /// 当缓存数据不存在则添加，已存在不会添加，添加成功返回true
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">值</param>
        bool TryAdd(string key, object value);

        /// <summary>
        /// 当缓存数据不存在则添加，已存在不会添加，添加成功返回true
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">值</param>
        /// <param name="expiration">过期时间间隔</param>
        bool TryAdd(string key, object value, TimeSpan expiration);

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">缓存Value</param>
        /// <param name="expiration">缓存时长</param>
        /// <param name="isSliding">是否滑动过期（如果在过期时间内有操作，则以当前时间点延长过期时间）</param>
        /// <returns></returns>
        bool TryAdd(string key, object value, TimeSpan? expiration, bool isSliding = true);

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">缓存Value</param>
        /// <param name="expirationSliding">滑动过期时长（如果在过期时间内有操作，则以当前时间点延长过期时间）</param>
        /// <param name="expirationAbsoulte">绝对过期时长</param>
        /// <returns></returns>
        bool TryAdd(string key, object value, TimeSpan expirationSliding, TimeSpan expirationAbsoulte);

        #endregion

        #region Remove

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="keys">缓存键集合</param>
        void Remove(params string[] keys);

        #endregion

        #region Clear

        /// <summary>
        /// 清空缓存
        /// </summary>
        void Clear();

        #endregion
    }
}
