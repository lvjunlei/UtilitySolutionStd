using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Extensions;

namespace Utility.Data.BulkExtensions
{
    /// <summary>
    /// RepositoryBulkExtensions
    /// 仓储 批量操作扩展
    /// </summary>
    public static class RepositoryBulkExtensions
    {
        #region 变量

        private static Dictionary<Type, DbType> typeMap = new Dictionary<Type, DbType>
        {
            {typeof(byte), DbType.Byte},
            {typeof(sbyte) ,DbType.SByte},
            { typeof(short),DbType.Int16},
            { typeof(ushort), DbType.UInt16},
            {typeof(int) ,DbType.Int32},
            { typeof(uint),DbType.UInt32},
            { typeof(long),DbType.Int64},
            {typeof(ulong),DbType.UInt64},
            {typeof(float),DbType.Single },
            { typeof(double),DbType.Double},
            {typeof(decimal), DbType.Decimal},
            { typeof(bool),DbType.Boolean},
            {typeof(string) ,DbType.String},
            { typeof(char),DbType.StringFixedLength},
            { typeof(Guid),DbType.Binary},
            { typeof(DateTime),DbType.DateTime},
            {typeof(DateTimeOffset), DbType.DateTimeOffset},
            {typeof(byte[]) ,DbType.Binary},
            {typeof(byte?), DbType.Byte},
            {typeof(sbyte?) ,DbType.SByte},
            { typeof(short?),DbType.Int16},
            { typeof(ushort?), DbType.UInt16},
            {typeof(int?) ,DbType.Int32},
            { typeof(uint?),DbType.UInt32},
            { typeof(long?),DbType.Int64},
            {typeof(ulong?),DbType.UInt64},
            {typeof(float?),DbType.Single },
            { typeof(double?),DbType.Double},
            {typeof(decimal?), DbType.Decimal},
            { typeof(bool?),DbType.Boolean},
            { typeof(char?),DbType.StringFixedLength},
            { typeof(Guid?),DbType.Guid},
            { typeof(DateTime?),DbType.DateTime},
            {typeof(DateTimeOffset?), DbType.DateTimeOffset},
            //{typeof(System.Data.Linq.Binary) ,DbType.Binary}
        };
        #endregion

        #region BulkInsert

        /// <summary>
        /// 批量插入操作
        /// 使用Dapper执行批量操作
        /// </summary>
        /// <typeparam name="TEntity">要执行实体类型</typeparam>
        /// <typeparam name="TKey">实体主键类型</typeparam>
        /// <param name="repository">仓储信息</param>
        /// <param name="entities">实体数据集合</param>
        /// <returns>影响的行数</returns>
        public static int BulkInsert<TEntity, TKey>(this IRepository<TEntity, TKey> repository, IEnumerable<TEntity> entities, DbTransaction transaction = null)
            where TEntity : EntityBase<TKey>
        {
            var sql = SqlGenerateFactory.CreateInsertSql(typeof(TEntity));
            return repository.Connection.Execute(sql, entities, transaction);
        }

        /// <summary>
        /// 批量插入操作 异步
        /// 使用Dapper执行批量操作
        /// </summary>
        /// <typeparam name="TEntity">要执行实体类型</typeparam>
        /// <typeparam name="TKey">实体主键类型</typeparam>
        /// <param name="repository">仓储信息</param>
        /// <param name="entities">实体数据集合</param>
        /// <returns>影响的行数</returns>
        public static Task<int> BulkInsertAsync<TEntity, TKey>(this IRepository<TEntity, TKey> repository, IEnumerable<TEntity> entities, DbTransaction transaction = null)
            where TEntity : EntityBase<TKey>
        {
            var sql = SqlGenerateFactory.CreateInsertSql(typeof(TEntity));
            return repository.Connection.ExecuteAsync(sql, entities, transaction);
        }

        #endregion

        #region BulkUpdate

        /// <summary>
        /// 批量更新操作
        /// 使用Dapper执行批量操作
        /// </summary>
        /// <typeparam name="TEntity">要执行实体类型</typeparam>
        /// <typeparam name="TKey">实体主键类型</typeparam>
        /// <param name="repository">仓储信息</param>
        /// <param name="sql">SQL语句</param>
        /// <param name="entities">实体数据集合</param>
        /// <returns>影响的行数</returns>
        public static int BulkUpdate<TEntity, TKey>(this IRepository<TEntity, TKey> repository, IEnumerable<TEntity> entities, DbTransaction transaction = null)
            where TEntity : EntityBase<TKey>
        {
            var sql = SqlGenerateFactory.CreateUpdateSql(typeof(TEntity));
            return repository.Connection.Execute(sql, entities, transaction);
        }

        /// <summary>
        /// 批量更新操作
        /// 使用Dapper执行批量操作
        /// </summary>
        /// <typeparam name="TEntity">要执行实体类型</typeparam>
        /// <typeparam name="TKey">实体主键类型</typeparam>
        /// <param name="repository">仓储信息</param>
        /// <param name="sql">SQL语句</param>
        /// <param name="entities">实体数据集合</param>
        /// <returns>影响的行数</returns>
        public static Task<int> BulkUpdateAsync<TEntity, TKey>(this IRepository<TEntity, TKey> repository, IEnumerable<TEntity> entities, DbTransaction transaction = null)
            where TEntity : EntityBase<TKey>
        {
            var sql = SqlGenerateFactory.CreateUpdateSql(typeof(TEntity));
            return repository.Connection.ExecuteAsync(sql, entities, transaction);
        }

        #endregion

        #region BulkDelete

        /// <summary>
        /// 批量删除操作
        /// 使用Dapper执行批量操作
        /// </summary>
        /// <typeparam name="TEntity">要执行实体类型</typeparam>
        /// <typeparam name="TKey">实体主键类型</typeparam>
        /// <param name="repository">仓储信息</param>
        /// <param name="sql">SQL语句</param>
        /// <param name="entities">实体数据集合</param>
        /// <returns>影响的行数</returns>
        public static int BulkDelete<TEntity, TKey>(this IRepository<TEntity, TKey> repository, IEnumerable<TEntity> entities, DbTransaction transaction = null)
            where TEntity : EntityBase<TKey>
        {
            var type = typeof(TEntity);
            var property = typeof(TEntity).GetProperties().FirstOrDefault(u => u.Name == "Id");
            if (property == null)
            {
                throw new ArgumentNullException($"对象 {type} 没有Id属性");
            }
            var sql = SqlGenerateFactory.CreateDeleteSql(type);

            var conn = repository.Connection;
            var command = conn.CreateCommand();
            command.CommandText = sql;
            if (transaction != null)
            {
                command.Transaction = transaction;
            }
            var count = 0;
            foreach (var entity in entities)
            {
                var parameter = command.CreateParameter();
                parameter.ParameterName = property.Name;
                parameter.DbType = typeMap[property.PropertyType];
                parameter.Value = property.GetValue(entity);

                command.Parameters.Add(parameter);
                count += command.ExecuteNonQuery();
                command.Parameters.Clear();
            }
            return count;
        }

        /// <summary>
        /// 批量删除操作
        /// 使用Dapper执行批量操作
        /// </summary>
        /// <typeparam name="TEntity">要执行实体类型</typeparam>
        /// <typeparam name="TKey">实体主键类型</typeparam>
        /// <param name="repository">仓储信息</param>
        /// <param name="sql">SQL语句</param>
        /// <param name="entities">实体数据集合</param>
        /// <returns>影响的行数</returns>
        public static Task<int> BulkDeleteAsync<TEntity, TKey>(this IRepository<TEntity, TKey> repository, IEnumerable<TEntity> entities, DbTransaction transaction = null)
            where TEntity : EntityBase<TKey>
        {
            return Task.Run(() =>
            {
                var type = typeof(TEntity);
                var property = typeof(TEntity).GetProperties().FirstOrDefault(u => u.Name == "Id");
                if (property == null)
                {
                    throw new ArgumentNullException($"对象 {type} 没有Id属性");
                }
                var sql = SqlGenerateFactory.CreateDeleteSql(type);

                var conn = repository.Connection;
                var command = conn.CreateCommand();
                command.CommandText = sql;
                if (transaction != null)
                {
                    command.Transaction = transaction;
                }
                var count = 0;
                foreach (var entity in entities)
                {
                    var parameter = command.CreateParameter();
                    parameter.ParameterName = property.Name;
                    parameter.DbType = typeMap[property.PropertyType];
                    parameter.Value = property.GetValue(entity);

                    command.Parameters.Add(parameter);
                    count += command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }
                return count;
            });
        }

        #endregion

        #region Execute SQL

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="sql">SQL语句</param>
        /// <param name="entities">参数集合</param>
        /// <returns></returns>
        private static int Execute(this IDbConnection conn, string sql, IEnumerable<object> entities, DbTransaction transaction = null)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            var command = conn.CreateCommand();
            command.CommandText = sql;
            if (transaction != null)
            {
                command.Transaction = transaction;
            }
            var properties = entities.First().GetType().GetProperties();
            var count = 0;
            foreach (var entity in entities)
            {
                foreach (var property in properties)
                {
                    var parameter = command.CreateParameter();
                    parameter.ParameterName = property.Name;
                    parameter.DbType = typeMap[property.PropertyType];
                    parameter.Value = property.GetValue(entity);

                    command.Parameters.Add(parameter);
                }
                count += command.ExecuteNonQuery();
                command.Parameters.Clear();
            }
            return count;
        }

        /// <summary>
        /// 执行SQL语句 异步
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="sql">SQL语句</param>
        /// <param name="entities">参数集合</param>
        /// <returns></returns>
        private static Task<int> ExecuteAsync(this IDbConnection conn, string sql, IEnumerable<object> entities, DbTransaction transaction = null)
        {
            return Task.Run(() =>
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                var command = conn.CreateCommand();
                command.CommandText = sql;
                if (transaction != null)
                {
                    command.Transaction = transaction;
                }
                var properties = entities.First().GetType().GetProperties();
                var count = 0;
                foreach (var entity in entities)
                {
                    foreach (var property in properties)
                    {
                        var parameter = command.CreateParameter();
                        parameter.ParameterName = property.Name;
                        parameter.DbType = typeMap[property.PropertyType];
                        parameter.Value = property.GetValue(entity);

                        command.Parameters.Add(parameter);
                    }
                    count += command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }
                return count;
            });
        }

        #endregion
    }
}
