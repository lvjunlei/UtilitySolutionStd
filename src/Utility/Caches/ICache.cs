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

namespace Utility.Caches
{
    /// <summary>
    /// 基础缓存接口定义
    /// </summary>
    public interface ICache
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
        /// 从缓存中获取数据，如果不存在，则执行获取数据操作并添加到缓存中
        /// </summary>
        /// <typeparam name="T">缓存数据类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="func">获取数据操作</param>
        /// <param name="expiration">过期时间间隔</param>
        T Get<T>(string key, Func<T> func, TimeSpan? expiration = null);

        #endregion

        #region Add

        /// <summary>
        /// 当缓存数据不存在则添加，已存在不会添加，添加成功返回true
        /// </summary>
        /// <typeparam name="T">缓存数据类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="value">值</param>
        /// <param name="expiration">过期时间间隔</param>
        bool TryAdd<T>(string key, T value, TimeSpan? expiration = null);

        #endregion

        #region Remove

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        void Remove(string key);

        #endregion

        #region Clear

        /// <summary>
        /// 清空缓存
        /// </summary>
        void Clear();

        #endregion
    }
}
