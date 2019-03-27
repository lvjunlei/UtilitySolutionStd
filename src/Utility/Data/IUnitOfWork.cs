#region IUnitOfWork 文件信息
/***********************************************************
**文 件 名：IUnitOfWork 
**命名空间：Utility.Data
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2017/11/14 9:00:24 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion

using System;
using System.Linq;
using System.Threading.Tasks;

namespace Utility.Data
{
    /// <summary>
    /// 工作单元接口
    /// </summary>
    public interface IUnitOfWork : IUnitOfWork<IDbContext>
    {

    }

    /// <summary>
    /// 工作单元接口（泛型）
    /// </summary>
    public interface IUnitOfWork<TDbContext> : IDisposable
        where TDbContext : IDbContext
    {
        /// <summary>
        /// 数据库上下文
        /// </summary>
        TDbContext Context { get; }

        /// <summary>
        /// 事务是否已提交
        /// </summary>
        bool IsCommitted { get; }

        /// <summary>
        /// 开始 事务
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// 提交事务操作
        /// </summary>
        /// <returns>操作影响的行数</returns>
        int Commit();

        /// <summary>
        /// 提交事务操作
        /// </summary>
        /// <returns>操作影响的行数</returns>
        Task<int> CommitAsync();

        /// <summary>
        /// 获取仓储
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <typeparam name="TKey">实体主键</typeparam>
        /// <returns></returns>
        IRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : EntityBase<TKey>;

        #region Execute

        /// <summary>
        /// 执行 SQL 语句
        /// 不需要 Commit
        /// </summary>
        /// <param name="sql">要执行的 SQL 语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>影响的行数</returns>
        int Execute(string sql, params object[] parameters);

        /// <summary>
        /// 异步 执行 SQL 语句
        /// 不需要 Commit
        /// </summary>
        /// <param name="sql">要执行的 SQL 语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>影响的行数</returns>
        Task<int> ExecuteAsync(string sql, params object[] parameters);

        #endregion

        #region Query

        /// <summary>
        /// 执行 SQL 查询
        /// </summary>
        /// <typeparam name="TEntity">要查询的数据类型</typeparam>
        /// <param name="sql">要执行的 SQL 语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>查询到的结果集合</returns>
        IQueryable<TEntity> Query<TEntity>(string sql, params object[] parameters)
            where TEntity : class, new();

        /// <summary>
        /// 异步 执行 SQL 查询
        /// </summary>
        /// <typeparam name="TEntity">要查询的数据类型</typeparam>
        /// <param name="sql">要执行的 SQL 语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>查询到的结果集合</returns>
        Task<IQueryable<TEntity>> QueryAsync<TEntity>(string sql, params object[] parameters)
            where TEntity : class, new();

        #endregion
    }
}
