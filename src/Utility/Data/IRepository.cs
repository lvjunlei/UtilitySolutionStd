#region IRepository 文件信息
/***********************************************************
**文 件 名：IRepository 
**命名空间：Cares.Core 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2017/11/14 8:59:37 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Utility.Data
{
    /// <summary>
    /// 仓储接口
    /// </summary>
    /// <typeparam name="TEntity">数据实体类型</typeparam>
    /// <typeparam name="TKey">数据实体主键</typeparam>
    public interface IRepository<TEntity, TKey> where TEntity : EntityBase<TKey>
    {
        /// <summary>
        /// 实体集合
        /// </summary>
        IQueryable<TEntity> Entities { get; }

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
        void ChangeTable(string tableName);

        #endregion

        #region 事务提交

        /// <summary>
        /// 提交事务
        /// </summary>
        /// <returns>影响的行数</returns>
        int Commit();

        /// <summary>
        /// 异步 提交事务
        /// </summary>
        /// <returns>影响的行数</returns>
        Task<int> CommitAsync();

        #endregion

        #region 插入操作

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TEntity Insert(TEntity entity);

        /// <summary>
        /// 批量添加数据
        /// </summary>
        /// <param name="entities"></param>
        void Insert(IEnumerable<TEntity> entities);

        /// <summary>
        /// 异步 批量添加数据
        /// </summary>
        /// <param name="entities"></param>
        void InsertAsync(IEnumerable<TEntity> entities);

        #endregion

        #region Delete

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        void Delete(TEntity entity);

        /// <summary>
        /// 根据主键删除数据
        /// </summary>
        /// <param name="id">实体主键</param>
        /// <returns></returns>
        void Delete(TKey id);

        /// <summary>
        /// 根据主键集合删除数据
        /// </summary>
        /// <param name="ids">主键集合</param>
        /// <returns></returns>
        void Delete(IEnumerable<TKey> ids);

        /// <summary>
        /// 删除符合条件的数据
        /// </summary>
        /// <param name="predicate">删除条件</param>
        /// <returns></returns>
        void Delete(Expression<Func<TEntity, bool>> predicate);

        #endregion

        #region Update

        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        void Update(TEntity entity);

        /// <summary>
        /// 修改实体指定的属性值
        /// </summary>
        /// <param name="entity"></param>
        void Update(IUpdateBase<TKey> entity);

        /// <summary>
        /// 修改指定的实体集合
        /// </summary>
        /// <param name="entities">实体集合</param>
        void UpdateRange(params TEntity[] entities);

        #endregion

        #region Values

        /// <summary>
        /// 修改实体指定的属性值
        /// </summary>
        /// <param name="id">数据ID</param>
        /// <param name="obj">要修改的属性</param>
        void SetValues(TKey id, object obj);

        /// <summary>
        /// 获取指定数据的指定属性名称的值
        /// </summary>
        /// <param name="id">数据ID</param>
        /// <param name="propertyName">属性名称</param>
        /// <returns></returns>
        TValue GetValue<TValue>(TKey id, string propertyName);

        #endregion

        #region Find

        /// <summary>
        /// 根据ID查找数据
        /// </summary>
        /// <param name="id">实体ID</param>
        /// <param name="includes">要包含的关联数据集合</param>
        /// <returns></returns>
        TEntity FindById(TKey id, params string[] includes);

        /// <summary>
        /// 根据给定的主键值查找实体
        /// </summary>
        /// <param name="keyValues">主键值集合</param>
        /// <returns></returns>
        TEntity Find(params object[] keyValues);

        /// <summary>
        /// 异步 根据给定的主键值查找实体
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="keyValues">主键值集合</param>
        /// <returns></returns>
        Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues);

        /// <summary>
        /// 查找符合条件的第一条数据
        /// </summary>
        /// <param name="predicate">查找条件</param>
        /// <param name="includes">要包含的关联数据集合</param>
        /// <returns></returns>
        TEntity First(Expression<Func<TEntity, bool>> predicate, params string[] includes);

        /// <summary>
        /// 异步 查找符合条件的第一条数据
        /// </summary>
        /// <param name="predicate">查找条件</param>
        /// <param name="includes">要包含的关联数据集合</param>
        /// <returns></returns>
        Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate, params string[] includes);

        /// <summary>
        /// 查找符合条件的第一条数据（查询不到返回默认值）
        /// </summary>
        /// <param name="predicate">查找条件</param>
        /// <param name="includes">要包含的关联数据集合</param>
        /// <returns></returns>
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate, params string[] includes);

        /// <summary>
        /// 异步 查找符合条件的第一条数据（查询不到返回默认值）
        /// </summary>
        /// <param name="predicate">查找条件</param>
        /// <param name="includes">要包含的关联数据集合</param>
        /// <returns></returns>
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, params string[] includes);

        /// <summary>
        /// 查询指定前几条符合条件的数据 
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="top">前几条数据</param>
        /// <param name="order">排序方式</param>
        /// <param name="isDescending">升序/降序，默认为升序</param>
        /// <returns></returns>
        IQueryable<TEntity> Top(int top, Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, bool>> order = null, bool isDescending = false);

        #endregion

        #region Query

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <param name="includes">要包含的关联数据集合</param>
        /// <returns></returns>
        IQueryable<TEntity> GetAlls(params string[] includes);

        /// <summary>
        /// 查询所有数据
        /// 不跟踪
        /// </summary>
        /// <param name="includes">要包含的关联数据集合</param>
        /// <returns></returns>
        IQueryable<TEntity> GetAllAsNoTracking(params string[] includes);

        /// <summary>
        /// 查询符合条件的数据
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns></returns>
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 查询符合条件的数据
        /// 并排序
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="order">排序方式</param>
        /// <param name="isDescending">升序/降序</param>
        /// <returns></returns>
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, bool>> order, bool isDescending = false);

        /// <summary>
        /// 查询符合条件的分页数据
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="pageIndex">分页索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="total">符合条件的总数据量</param>
        /// <returns></returns>
        IQueryable<TEntity> Query(int pageIndex, int pageSize, out int total, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 查询符合条件的分页数据
        /// 并排序
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="pageIndex">分页索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="total">符合条件的总数据量</param>
        /// <param name="order">排序方式</param>
        /// <param name="isDescending">升序/降序</param>
        /// <returns></returns>
        IQueryable<TEntity> Query(int pageIndex, int pageSize, out int total,
            Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, dynamic>> order,
            bool isDescending = false);
        #endregion

        #region ExceuteSQL

        /// <summary>
        /// 执行 非查询 SQL 语句
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        int Execute(string sql, params object[] parameters);

        /// <summary>
        /// 异步  执行 非查询 SQL 语句
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        Task<int> ExecuteAsync(string sql, params object[] parameters);

        /// <summary>
        /// 执行 查询 SQL 语句
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        IQueryable<TEntity> FromSql(string sql, params object[] parameters);

        /// <summary>
        /// 异步 执行查询 SQL 语句
        /// </summary>
        /// <param name="sql">SQL 语句</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        Task<IQueryable<TEntity>> FromSqlAsync(string sql, params object[] parameters);
        #endregion

        #region IsExist

        /// <summary>
        /// 查询指定 ID 的数据是否存在
        /// </summary>
        /// <param name="id">指定的数据ID</param>
        /// <returns></returns>
        bool IsExist(TKey id);

        /// <summary>
        /// 查询指定 条件 的数据是否存在
        /// </summary>
        /// <param name="predicate">指定的条件</param>
        /// <returns></returns>
        bool IsExist(Expression<Func<TEntity, bool>> predicate);

        #endregion
    }
}
