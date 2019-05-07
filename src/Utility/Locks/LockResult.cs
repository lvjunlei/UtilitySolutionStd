using System;
using System.Collections.Generic;
using System.Text;

namespace Utility.Locks
{
    /// <summary>
    /// 获取锁结果
    /// </summary>
    public class LockResult : IDisposable
    {
        #region 公共属性

        /// <summary>
        /// 获取结果ID
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// 是否获取成功
        /// </summary>
        public bool Acquired { get; }

        /// <summary>
        /// 扩展数
        /// </summary>
        public int ExtendCount { get; }

        #endregion 公共属性

        /// <summary>
        /// 初始化LockResult
        /// </summary>
        /// <param name="acquired">是否获取成功</param>
        /// <param name="extendCount">扩展数</param>
        public LockResult(bool acquired, int extendCount)
        {
            Acquired = acquired;
            ExtendCount = extendCount;
        }

        #region IDisposable

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            // throw new NotImplementedException();
        }

        #endregion IDisposable
    }
}