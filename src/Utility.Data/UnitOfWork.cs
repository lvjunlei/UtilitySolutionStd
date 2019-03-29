#region UnitOfWork 文件信息
/***********************************************************
**文 件 名：UnitOfWork 
**命名空间：Cares.Core 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2017/11/14 9:00:32 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Utility.Data;
using Utility.EntityFramework.Extensions;

namespace Utility.EntityFramework
{
    /// <summary>
    /// 工作单元
    /// </summary>
    public class UnitOfWork : UnitOfWork<IDbContext>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="context"></param>
        public UnitOfWork(IDbContext context) : base(context)
        {
        }
    }

    /// <summary>
    /// 工作单元
    /// </summary>
    public class UnitOfWork<TDbContext> : IUnitOfWork<TDbContext>
        where TDbContext : IDbContext
    {
        #region 私有属性

        /// <summary>
        /// 是否已释放
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// 事务
        /// </summary>
        private IDbContextTransaction _dbTransaction;

        /// <summary>
        /// 当前请求涉及的仓储对象的scope生命的仓储对象
        /// </summary>
        private readonly Hashtable _repositorys;

        #endregion

        #region 公共属性

        /// <summary>
        /// 数据库上下文
        /// </summary>
        private DbContext Db
        {
            get
            {
                var context = Context as DbContext;
                if (context == null)
                {
                    throw new Exception("读/从数据库为空");
                }
                return context;
            }
        }

        /// <summary>
        /// 数据库上下文
        /// </summary>
        public TDbContext Context { get; }

        /// <summary>
        /// 事务是否已提交
        /// </summary>
        public bool IsCommitted { get; private set; }


        #endregion

        #region 构造函数

        /// <summary>
        /// 初始化单元操作
        /// </summary>
        /// <param name="context">数据库上下文</param>
        public UnitOfWork(TDbContext context)
        {
            Context = context;
            _repositorys = new Hashtable();
        }

        #endregion

        #region Execute

        /// <summary>
        /// 执行 SQL 语句
        /// 不需要 Commit
        /// </summary>
        /// <param name="sql">要执行的 SQL 语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>影响的行数</returns>
        public int Execute(string sql, params object[] parameters)
        {
            return Db.Database.ExecuteSqlCommand(sql, parameters);
        }

        /// <summary>
        /// 异步 执行 SQL 语句
        /// 不需要 Commit
        /// </summary>
        /// <param name="sql">要执行的 SQL 语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>影响的行数</returns>
        public async Task<int> ExecuteAsync(string sql, params object[] parameters)
        {
            return await Db.Database.ExecuteSqlCommandAsync(sql, parameters);
        }

        #endregion

        #region Query

        /// <summary>
        /// 执行 SQL 查询
        /// </summary>
        /// <typeparam name="TEntity">要查询的数据类型</typeparam>
        /// <param name="sql">要执行的 SQL 语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>查询到的结果集合</returns>
        public IQueryable<TEntity> Query<TEntity>(string sql, params object[] parameters)
            where TEntity : class, new()
        {
            return Db.Database.SqlQuery<TEntity>(sql, parameters).AsQueryable();
        }

        /// <summary>
        /// 异步 执行 SQL 查询
        /// </summary>
        /// <typeparam name="TEntity">要查询的数据类型</typeparam>
        /// <param name="sql">要执行的 SQL 语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>查询到的结果集合</returns>
        public async Task<IQueryable<TEntity>> QueryAsync<TEntity>(string sql, params object[] parameters)
            where TEntity : class, new()
        {
            var result = await Db.Database.SqlQueryAsync<TEntity>(sql, parameters);
            return result.AsQueryable();
        }

        #endregion

        #region 事务操作

        /// <summary>
        /// 开始 事务
        /// </summary>
        public void BeginTransaction()
        {
            _dbTransaction = Db.Database.BeginTransaction();
        }

        /// <summary>
        /// 提交事务操作
        /// </summary>
        /// <returns>操作影响的行数</returns>
        public int Commit()
        {
            int result;
            try
            {
                ChangeEntityUpdateTime(Db);
                result = Context.SaveChanges();
                _dbTransaction?.Commit();
                IsCommitted = true;
            }
            catch (Exception ex)
            {
                IsCommitted = false;
                CleanChanges(Db);
                _dbTransaction.Rollback();
                if (Configuration.Container != null)
                {
                    if (Configuration.Container.IsRegistered<ILogService>())
                    {
                        var logger = Configuration.Container.Resolve<ILogService>();
                        logger.Error($"Commit 异常：{ex.InnerException}/r{ ex.Message}");
                        logger.Error(ex);
                    }
                }
                throw new Exception($"Commit 异常：{ex.InnerException}/r{ ex.Message}");
            }
            return result;
        }

        /// <summary>
        /// 提交事务操作 异步
        /// </summary>
        /// <returns>操作影响的行数</returns>
        public async Task<int> CommitAsync()
        {
            int result;
            try
            {
                ChangeEntityUpdateTime(Db);
                result = await Context.SaveChangesAsync();
                _dbTransaction?.Commit();
                IsCommitted = true;
            }
            catch (Exception ex)
            {
                IsCommitted = false;
                CleanChanges(Db);
                _dbTransaction.Rollback();
                if (Configuration.Container != null)
                {
                    if (Configuration.Container.IsRegistered<ILogService>())
                    {
                        var logger = Configuration.Container.Resolve<ILogService>();
                        logger.Error($"Commit 异常：{ex.InnerException}/r{ ex.Message}");
                        logger.Error(ex);
                    }
                }
                throw new Exception($"Commit 异常：{ex.InnerException}/r{ ex.Message}");
            }
            return await Task.FromResult(result);
        }

        /// <summary>
        /// 操作失败，还原跟踪状态
        /// </summary>
        /// <param name="context"></param>
        private static void CleanChanges(DbContext context)
        {
            var entries = context.ChangeTracker.Entries().ToArray();
            foreach (var entry in entries)
            {
                entry.State = EntityState.Detached;
            }
        }

        /// <summary>
        /// 修改实体更新时间
        /// </summary>
        /// <param name="context"></param>
        private static void ChangeEntityUpdateTime(DbContext context)
        {
            var entries = context.ChangeTracker.Entries().ToArray();
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Modified)
                {
                    if (entry.Entity is IUpdate e)
                    {
                        e.UpdateTime = DateTime.Now;
                    }
                }
            }
        }

        #endregion

        #region IRepository

        /// <summary>
        /// 获取仓储
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <typeparam name="TKey">实体主键</typeparam>
        /// <returns></returns>
        public IRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : EntityBase<TKey>
        {
            var name = $"Repository_{typeof(TEntity).Name}";
            if (!_repositorys.ContainsKey(name))
            {
                var repository = new Repository<TEntity, TKey>(Db);
                _repositorys.Add(name, repository);
            }

            return (IRepository<TEntity, TKey>)_repositorys[name];
        }

        #endregion

        #region ChangeDatabase

        /// <summary>
        /// 改变数据库名称
        /// 要求数据库在同一台机器上
        /// 目前只在MYSQL数据库有效
        /// </summary>
        /// <param name="database">数据库名称</param>
        public void ChangeDatabase(string database)
        {
            var connection = Db.Database.GetDbConnection();
            if (connection.State.HasFlag(ConnectionState.Open))
            {
                connection.ChangeDatabase(database);
            }
            else
            {
                var connectionString = Regex.Replace(connection.ConnectionString.Replace(" ", ""), @"(?<=[Dd]atabase=)\w+(?=;)", database, RegexOptions.Singleline);
                connection.ConnectionString = connectionString;
            }

            // Following code only working for mysql.
            var items = Db.Model.GetEntityTypes();
            foreach (var item in items)
            {
                if (item.Relational() is RelationalEntityTypeAnnotations extensions)
                {
                    extensions.Schema = database;
                }
            }
        }

        #endregion

        #region IDispose

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            _dbTransaction?.Dispose();
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            _disposed = true;
        }

        #endregion
    }
}
