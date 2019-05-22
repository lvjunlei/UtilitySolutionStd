#region Repository 文件信息
/***********************************************************
**文 件 名：Repository 
**命名空间：Cares.Core 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2017/11/14 8:59:57 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Utility.Data;

namespace Utility.EntityFramework
{
    /// <summary>
    /// 仓储基类
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TKey">实体主键类型</typeparam>
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : EntityBase<TKey>
    {
        /// <summary>
        /// 当前数据库连接字符串
        /// </summary>
        public DbConnection Connection
        {
            get
            {
                return _dbContext.Database.GetDbConnection();
            }
        }

        #region _reaDbContext

        /// <summary>
        /// DbContext
        /// </summary>
        private readonly DbContext _dbContext;

        /// <summary>
        /// DbSet
        /// </summary>
        private readonly DbSet<TEntity> _dbSet;

        /// <summary>
        /// 数据库实体集合
        /// </summary>
        public IQueryable<TEntity> Entities => _dbSet.AsNoTracking();

        #endregion

        #region 构造函数

        /// <summary>
        /// 初始化仓储基类
        /// </summary>
        public Repository(DbContext context)
        {
            _dbContext = context;
            _dbSet = _dbContext.Set<TEntity>();
        }

        #endregion

        #region ChangeTable

        /// <summary>
        /// 改变表名
        /// 两张表必须在同一个数据库内才行
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <remarks>
        /// This only been used for supporting multiple tables in the same model. 
        /// This require the tables in the same database.
        /// </remarks>
        public void ChangeTable(string tableName)
        {
            if (_dbContext.Model.FindEntityType(typeof(TEntity)).Relational() is RelationalEntityTypeAnnotations relational)
            {
                relational.TableName = tableName;
            }
        }

        #endregion

        #region 事务操作

        /// <summary>
        /// 提交事务
        /// </summary>
        /// <returns>影响的行数</returns>
        public int Commit()
        {
            return _dbContext.SaveChanges();
        }

        /// <summary>
        /// 异步 提交事务
        /// </summary>
        /// <returns>影响的行数</returns>
        public async Task<int> CommitAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        #endregion

        #region Delete

        /// <summary>
        /// 删除指定实体
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(TEntity entity)
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            var delete = entity as ISoftDelete;
            if (delete != null)
            {
                delete.IsDelete = true;
            }
            else
            {
                _dbSet.Remove(entity);
            }
        }

        /// <summary>
        /// 删除指定ID的数据
        /// </summary>
        /// <param name="id"></param>
        public void Delete(TKey id)
        {
            var entity = _dbSet.First(u => u.Id.Equals(id));
            Delete(entity);
        }

        /// <summary>
        /// 删除数据集合
        /// </summary>
        /// <param name="ids">数据ID集合</param>
        public void Delete(IEnumerable<TKey> ids)
        {
            var entities = _dbSet.Where(u => ids.Contains(u.Id));
            foreach (var entity in entities)
            {
                Delete(entity);
            }
        }

        /// <summary>
        /// 删除符合条件的数据
        /// </summary>
        /// <param name="predicate">删除条件</param>
        public void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = Entities.Where(predicate);
            foreach (var entity in entities)
            {
                Delete(entity);
            }
        }

        #endregion

        #region Query

        /// <summary>
        /// 查询指定ID的数据
        /// </summary>
        /// <param name="id">数据ID</param>
        /// <param name="includes">要包含的关联数据集合</param>
        /// <returns></returns>
        public TEntity FindById(TKey id, params string[] includes)
        {
            var query = _dbSet.AsQueryable();
            return includes.Aggregate(query, (current, include) => current.Include(include))
                .First(u => u.Id.Equals(id));
        }

        /// <summary>
        /// 根据给定的主键值查找实体
        /// </summary>
        /// <param name="keyValues">主键值集合</param>
        /// <returns></returns>
        public TEntity Find(params object[] keyValues)
        {
            return _dbSet.Find(keyValues);
        }

        /// <summary>
        /// 异步 根据给定的主键值查找实体
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="keyValues">主键值集合</param>
        /// <returns></returns>
        public async Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return await _dbSet.FindAsync(cancellationToken, keyValues);
        }

        /// <summary>
        /// 查询符合条件的第一条数据
        /// 如果数据集为空则会抛出异常
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="includes">要包含的关联数据集合</param>
        /// <returns></returns>
        public TEntity First(Expression<Func<TEntity, bool>> predicate, params string[] includes)
        {
            var query = _dbSet.AsQueryable();
            return includes.Aggregate(query, (current, include) => current.Include(include))
                .First(predicate);
        }

        /// <summary>
        /// 异步 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includes">要包含的关联数据集合</param>
        /// <returns></returns>
        public async Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate, params string[] includes)
        {
            var query = _dbSet.AsQueryable();
            return await includes.Aggregate(query, (current, include) => current.Include(include))
                .FirstAsync(predicate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includes">要包含的关联数据集合</param>
        /// <returns></returns>
        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate, params string[] includes)
        {
            var query = _dbSet.AsQueryable();
            return includes.Aggregate(query, (current, include) => current.Include(include)).FirstOrDefault(predicate);
        }

        /// <summary>
        /// 异步 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includes">要包含的关联数据集合</param>
        /// <returns></returns>
        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, params string[] includes)
        {
            var query = _dbSet.AsQueryable();
            return await includes.Aggregate(query, (current, include) => current.Include(include)).FirstOrDefaultAsync(predicate);
        }

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <param name="includes">要包含的关联数据集合</param>
        /// <returns></returns>
        public IQueryable<TEntity> GetAlls(params string[] includes)
        {
            var query = _dbSet.AsQueryable();
            return includes.Aggregate(query, (current, include) => current.Include(include));
        }

        /// <summary>
        /// 查询所有数据
        /// 不跟踪
        /// </summary>
        /// <param name="includes">要包含的关联数据集合</param>
        /// <returns></returns>
        public IQueryable<TEntity> GetAllAsNoTracking(params string[] includes)
        {
            var query = _dbSet.AsNoTracking();
            return includes.Aggregate(query, (current, include) => current.Include(include));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAlls().Where(predicate).AsNoTracking();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="order"></param>
        /// <param name="isDescending"></param>
        /// <returns></returns>
        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, bool>> order, bool isDescending = false)
        {
            if (!isDescending)
            {
                return GetAlls().Where(predicate).OrderBy(order).AsNoTracking();
            }
            return GetAlls().Where(predicate).OrderByDescending(order).AsNoTracking();
        }

        /// <summary>
        /// 查询符合条件的分页信息
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="pageIndex">分页索引</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="total">总数据</param>
        /// <returns></returns>
        public IQueryable<TEntity> Query(int pageIndex,
            int pageSize,
            out int total,
            Expression<Func<TEntity, bool>> predicate)
        {
            var query = GetAllAsNoTracking().Where(predicate);
            total = query.Count();

            return query.Skip((pageIndex - 1) * pageSize).Take(pageSize).AsQueryable();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="total"></param>
        /// <param name="order"></param>
        /// <param name="isDescending"></param>
        /// <returns></returns>
        public IQueryable<TEntity> Query(int pageIndex,
            int pageSize,
            out int total,
            Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, dynamic>> order,
            bool isDescending = false)
        {
            IQueryable<TEntity> query = !isDescending
                ? GetAllAsNoTracking().Where(predicate).OrderBy(order)
                : GetAllAsNoTracking().Where(predicate).OrderByDescending(order);
            total = query.Count();
            return query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }

        /// <summary>
        /// 查询指定前几条符合条件的数据 
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="top">前几条数据</param>
        /// <param name="order">排序方式</param>
        /// <param name="isDescending">升序/降序，默认为升序</param>
        /// <returns></returns>
        public IQueryable<TEntity> Top(int top,
            Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, bool>> order = null,
            bool isDescending = false)
        {
            var query = GetAllAsNoTracking().Where(predicate);
            if (order != null)
            {
                query = isDescending ? query.OrderBy(order) : query.OrderByDescending(order);
            }
            return query.Take(top);
        }

        #endregion

        #region Update

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="entity"></param>
        public void Update(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// 修改实体指定的属性值
        /// </summary>
        /// <param name="entity"></param>
        public void Update(IUpdateBase<TKey> entity)
        {
            var model = _dbSet.FirstOrDefault(u => u.Id.Equals(entity.Id));
            if (model == null)
            {
                throw new Exception($"找不到主键为 {entity.Id} 的对象~");
            }
            _dbContext.Entry(model).CurrentValues.SetValues(entity);
        }

        /// <summary>
        /// 修改指定的实体集合
        /// </summary>
        /// <param name="entities">实体集合</param>
        public void UpdateRange(params TEntity[] entities)
        {
            _dbSet.UpdateRange(entities);
        }

        #endregion

        #region Values

        /// <summary>
        /// 修改实体指定的属性值
        /// </summary>
        /// <param name="id">数据ID</param>
        /// <param name="obj">要修改的属性对象</param>
        public void SetValues(TKey id, object obj)
        {
            var model = _dbSet.FirstOrDefault(u => u.Id.Equals(id));
            if (model == null)
            {
                throw new Exception($"找不到主键为 {id} 的对象~");
            }
            _dbContext.Entry(model).CurrentValues.SetValues(obj);
        }

        /// <summary>
        /// 获取指定数据的指定属性名称的值
        /// </summary>
        /// <param name="id">数据ID</param>
        /// <param name="propertyName">属性名称</param>
        /// <returns></returns>
        public TValue GetValue<TValue>(TKey id, string propertyName)
        {
            var model = _dbSet.FirstOrDefault(u => u.Id.Equals(id));
            if (model == null)
            {
                throw new Exception($"找不到主键为 {id} 的对象~");
            }
            return _dbContext.Entry(model).CurrentValues.GetValue<TValue>(propertyName);
        }

        #endregion

        #region Insert

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public TEntity Insert(TEntity entity)
        {
            return _dbSet.Add(entity).Entity;
        }

        /// <summary>
        /// 批量添加数据
        /// </summary>
        /// <param name="entities"></param>
        public void Insert(IEnumerable<TEntity> entities)
        {
            var adc = _dbContext.ChangeTracker.AutoDetectChangesEnabled;
            try
            {
                _dbContext.ChangeTracker.AutoDetectChangesEnabled = true;
                _dbSet.AddRange(entities);
            }
            finally
            {
                _dbContext.ChangeTracker.AutoDetectChangesEnabled = adc;
            }
        }

        /// <summary>
        /// 异步 批量添加数据
        /// </summary>
        /// <param name="entities"></param>
        public async void InsertAsync(IEnumerable<TEntity> entities)
        {
            await Task.Run(() =>
            {
                foreach (var entity in entities)
                {
                    _dbSet.Add(entity);
                }
            });
        }

        #endregion

        #region IsExist

        /// <summary>
        /// 指定条件的数据是否存在
        /// </summary>
        /// <param name="id">数据ID</param>
        /// <returns></returns>
        public bool IsExist(TKey id)
        {
            return GetAllAsNoTracking().Any(u => u.Id.Equals(id));
        }

        /// <summary>
        /// 指定条件的数据是否存在
        /// </summary>
        /// <param name="predicate">指定条件</param>
        /// <returns></returns>
        public bool IsExist(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAllAsNoTracking().Any(predicate);
        }

        #endregion

        #region ExceuteSQL

        /// <summary>
        /// 执行 SQL 语句
        /// </summary>
        /// <param name="sql">要执行的 SQL 语句</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public int Execute(string sql, params object[] parameters)
        {
            return _dbContext.Database.ExecuteSqlCommand(sql, parameters);
        }

        /// <summary>
        /// 异步 执行 SQL 语句 
        /// </summary>
        /// <param name="sql">要执行的 SQL 语句</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public async Task<int> ExecuteAsync(string sql, params object[] parameters)
        {
            return await _dbContext.Database.ExecuteSqlCommandAsync(sql, parameters);
        }

        /// <summary>
        /// 执行查询 SQL 语句
        /// </summary>
        /// <param name="sql">SQL 语句</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public IQueryable<TEntity> FromSql(string sql, params object[] parameters)
        {
            return _dbSet.FromSql(sql, parameters);
        }

        /// <summary>
        /// 异步 执行查询 SQL 语句
        /// </summary>
        /// <param name="sql">SQL 语句</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public async Task<IQueryable<TEntity>> FromSqlAsync(string sql, params object[] parameters)
        {
            return await Task.Run(() => _dbSet.FromSql(sql, parameters));
        }

        #endregion
    }
}
