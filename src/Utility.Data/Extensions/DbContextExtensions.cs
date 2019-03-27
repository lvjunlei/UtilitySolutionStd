#region DbContextExtensions 文件信息
/***********************************************************
**文 件 名：DbContextExtensions 
**命名空间：Utility.EntityFramework.Extensions 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2019-02-26 15:06:18 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Dynamic;
using System.Threading.Tasks;

namespace Utility.EntityFramework.Extensions
{
    /// <summary>
    /// DbContextExtensions
    /// </summary>
    public static class DbContextExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="parameters"></param>
        private static void CombineParams(ref DbCommand command, params object[] parameters)
        {
            if (parameters != null)
            {
                foreach (SqlParameter parameter in parameters)
                {
                    if (!parameter.ParameterName.Contains("@"))
                        parameter.ParameterName = $"@{parameter.ParameterName}";
                    command.Parameters.Add(parameter);
                }
            }
        }

        /// <summary>
        /// 创建 DbCommand
        /// </summary>
        /// <param name="dbFacade">DatabaseFacade</param>
        /// <param name="sql"></param>
        /// <param name="dbConn"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private static DbCommand CreateCommand(DatabaseFacade dbFacade, string sql, out DbConnection dbConn, params object[] parameters)
        {
            var conn = dbFacade.GetDbConnection();
            dbConn = conn;
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            CombineParams(ref cmd, parameters);
            return cmd;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbFacade"></param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static DataTable SqlQuery(this DatabaseFacade dbFacade, string sql, params object[] parameters)
        {
            var cmd = CreateCommand(dbFacade, sql, out DbConnection conn, parameters);
            var reader = cmd.ExecuteReader();
            var dt = new DataTable();
            dt.Load(reader);
            reader.Close();
            conn.Close();
            return dt;
        }

        /// <summary>
        /// 异步执行 SQL 语句 扩展
        /// </summary>
        /// <param name="dbFacade"></param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static async Task<DataTable> SqlQueryAsync(this DatabaseFacade dbFacade, string sql, params object[] parameters)
        {
            return await Task.Run(async () =>
            {
                var cmd = CreateCommand(dbFacade, sql, out DbConnection conn, parameters);
                var reader = await cmd.ExecuteReaderAsync();
                var dt = new DataTable();
                dt.Load(reader);
                reader.Close();
                conn.Close();
                return await Task.FromResult<DataTable>(dt);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbFacade"></param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static IEnumerable<T> SqlQuery<T>(this DatabaseFacade dbFacade, string sql, params object[] parameters)
            where T : class, new()
        {
            var dt = SqlQuery(dbFacade, sql, parameters);
            return dt.ToEnumerable<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbFacade"></param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static Task<IEnumerable<T>> SqlQueryAsync<T>(this DatabaseFacade dbFacade, string sql, params object[] parameters)
            where T : class, new()
        {
            return Task.Run(async () =>
            {
                var dt = await SqlQueryAsync(dbFacade, sql, parameters);
                return dt.ToEnumerable<T>();
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static IEnumerable<T> ToEnumerable<T>(this DataTable dt) where T : class, new()
        {
            var propertyInfos = typeof(T).GetProperties();
            var ts = new T[dt.Rows.Count];
            var i = 0;
            foreach (DataRow row in dt.Rows)
            {
                var t = new T();
                foreach (var p in propertyInfos)
                {
                    if (dt.Columns.IndexOf(p.Name) != -1 && row[p.Name] != DBNull.Value)
                        p.SetValue(t, row[p.Name], null);
                }
                ts[i] = t;
                i++;
            }
            return ts;
        }

        /// <summary>
        /// 执行Sql语句返回动态查询结果
        /// </summary>
        /// <param name="db">DatabaseFacade</param>
        /// <param name="Sql">Sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public static IEnumerable<dynamic> SqlQueryDynamic(this DatabaseFacade db, string Sql, params SqlParameter[] parameters)
        {
            using (var cmd = db.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = Sql;

                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                foreach (var p in parameters)
                {
                    var dbParameter = cmd.CreateParameter();
                    dbParameter.DbType = p.DbType;
                    dbParameter.ParameterName = p.ParameterName;
                    dbParameter.Value = p.Value;
                    cmd.Parameters.Add(dbParameter);
                }

                using (var dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var row = new ExpandoObject() as IDictionary<string, object>;
                        for (var fieldCount = 0; fieldCount < dataReader.FieldCount; fieldCount++)
                        {
                            row.Add(dataReader.GetName(fieldCount), dataReader[fieldCount]);
                        }
                        yield return row;
                    }
                }
            }
        }
    }
}
