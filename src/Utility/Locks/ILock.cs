using System;
using System.Threading;
using System.Threading.Tasks;

namespace Utility.Locks
{
    /// <summary>
    /// 分布式锁接口定义
    /// </summary>
    public interface ILock //: IDisposable
    {
        /// <summary>
        /// 获取资源所
        /// </summary>
        /// <param name="resourceKey">资源Key</param>
        /// <returns>结果</returns>
        LockResult Lock(string resourceKey);

        /// <summary>
        /// 获取资源所 异步
        /// </summary>
        /// <param name="resourceKey">资源Key</param>
        /// <returns>结果</returns>
        Task<LockResult> LockAsync(string resourceKey);

        /// <summary>
        /// 获取资源所
        /// </summary>
        /// <param name="resourceKey">资源Key</param>
        /// <param name="expiryTime">过期时间</param>
        /// <returns>结果</returns>
        LockResult Lock(string resourceKey, TimeSpan expiryTime);

        /// <summary>
        /// 获取资源所 异步
        /// </summary>
        /// <param name="resourceKey">资源Key</param>
        /// <param name="expiryTime">过期时间</param>
        /// <returns>结果</returns>
        Task<LockResult> LockAsync(string resourceKey, TimeSpan expiryTime);

        /// <summary>
        /// 获取资源所
        /// </summary>
        /// <param name="resourceKey">资源Key</param>
        /// <param name="expiryTime">过期时间</param>
        /// <param name="waitTime">等待时间</param>
        /// <param name="retryTime">重试时间</param>
        /// <returns>结果</returns>
        LockResult Lock(string resourceKey, TimeSpan expiryTime, TimeSpan waitTime, TimeSpan retryTime);

        /// <summary>
        /// 获取资源所 异步
        /// </summary>
        /// <param name="resourceKey">资源Key</param>
        /// <param name="expiryTime">过期时间</param>
        /// <param name="waitTime">等待时间</param>
        /// <param name="retryTime">重试时间</param>
        /// <returns>结果</returns>
        Task<LockResult> LockAsync(string resourceKey, TimeSpan expiryTime, TimeSpan waitTime, TimeSpan retryTime);

        /// <summary>
        /// 获取资源所（可取消）
        /// </summary>
        /// <param name="resourceKey">资源Key</param>
        /// <param name="expiryTime">过期时间</param>
        /// <param name="waitTime">等待时间</param>
        /// <param name="retryTime">重试时间</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>结果</returns>
        LockResult Lock(string resourceKey, TimeSpan expiryTime, TimeSpan waitTime, TimeSpan retryTime, CancellationToken cancellationToken);

        /// <summary>
        /// 获取资源所（可取消） 异步
        /// </summary>
        /// <param name="resourceKey">资源Key</param>
        /// <param name="expiryTime">过期时间</param>
        /// <param name="waitTime">等待时间</param>
        /// <param name="retryTime">重试时间</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>结果</returns>
        Task<LockResult> LockAsync(string resourceKey, TimeSpan expiryTime, TimeSpan waitTime, TimeSpan retryTime, CancellationToken cancellationToken);
    }
}