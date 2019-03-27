#region IDbContext 文件信息
/***********************************************************
**文 件 名：IDbContext 
**命名空间：Cares.Core 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2017/11/14 8:58:44 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Utility.Data
{
    /// <summary>
    /// DbContext接口类
    /// </summary>
    public interface IDbContext : IDisposable
    {
        /// <summary>
        /// 持久化数据
        /// </summary>
        /// <returns>影响的行数</returns>
        int SaveChanges();

        /// <summary>
        /// 异步 持久化数据
        /// </summary>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>影响的行数</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
