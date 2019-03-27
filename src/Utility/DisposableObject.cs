using System;
using System.ComponentModel;

namespace Utility
{
    /// <summary>
    /// 可释放对象
    /// </summary>
    [Serializable]
    public abstract class DisposableObject : IDisposable
    {
        #region 变量

        /// <summary>
        /// 对象释放状态
        /// </summary>
        private DisposeState _disposeState = DisposeState.None;

        #endregion

        #region 析构函数

        ~DisposableObject()
        {
            Dispose(false);
        }

        #endregion

        #region 事件

        /// <summary>
        /// 对象被释放事件
        /// </summary>
        public event EventHandler Disposed;

        protected virtual void OnDisposed()
        {
            Disposed?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region 公共属性

        /// <summary>
        /// 是否正在释放
        /// </summary>
        public bool IsDisposing => _disposeState == DisposeState.Disposing;

        /// <summary>
        /// 是否已释放完成
        /// </summary>
        public bool IsDisposed => _disposeState == DisposeState.Disposed;

        #endregion

        #region 方法

        /// <summary>
        /// 释放托管资源
        /// </summary>
        protected virtual void DisposeManagedResources() { }

        /// <summary>
        /// 释放非托管资源
        /// </summary>
        protected virtual void DisposeUnManagedResources() { }

        /// <summary>
        /// 释放对象
        /// </summary>
        /// <param name="disposing">true：通过Dispose()方法释放，false：通过GC释放</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposeState == DisposeState.None)
            {
                try
                {
                    _disposeState = DisposeState.Disposing;
                    if (disposing)
                    {
                        DisposeManagedResources();
                        DisposeUnManagedResources();
                        OnDisposed();
                        GC.SuppressFinalize(this);
                    }
                    else
                    {
                        DisposeManagedResources();
                    }
                    _disposeState = DisposeState.Disposed;
                }
                catch
                {
                    _disposeState = DisposeState.None;
                    throw;
                }
            }
        }

        /// <summary>
        /// 检查对象释放状态，释放或者释放中则会抛出ObjectDisposedException异常
        /// </summary>
        protected void CheckDisposed()
        {
            if (_disposeState != DisposeState.None)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        /// <summary>
        /// 释放对象资源
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        #endregion
    }

    /// <summary>
    /// 资源释放状态
    /// </summary>
    [Serializable]
    [Description("资源释放状态")]
    public enum DisposeState
    {
        /// <summary>
        /// 未释放
        /// </summary>
        [Description("未释放")]
        None,

        /// <summary>
        /// 释放中
        /// </summary>
        [Description("释放中")]
        Disposing,

        /// <summary>
        /// 已释放
        /// </summary>
        [Description("已释放")]
        Disposed
    }
}
