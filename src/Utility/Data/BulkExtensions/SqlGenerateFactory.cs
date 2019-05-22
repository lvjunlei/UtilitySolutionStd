using System;
using System.Linq;
using System.Text;
using Utility.Extensions;

namespace Utility.Data.BulkExtensions
{
    /// <summary>
    /// SQL 语句生成工厂
    /// </summary>
    public class SqlGenerateFactory
    {
        #region Generate SQL 

        #region Insert SQL

        /// <summary>
        /// 生成 Insert SQL语句
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <returns></returns>
        public static string CreateInsertSql(Type type)
        {
            switch (Configuration.DatabaseType)
            {
                case DatabaseType.SQLServer:
                    return CreateSqlServerInsertSql(type);
                case DatabaseType.Oracle:
                    return CreateOracleInsertSql(type);
                case DatabaseType.MySQL:
                    return CreateMySqlInsertSql(type);
            }

            throw new ArgumentException($"暂不支持该类型的数据库：{Configuration.DatabaseType}");
        }

        /// <summary>
        /// 生成 Insert SQL语句
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <returns></returns>
        private static string CreateOracleInsertSql(Type type)
        {
            var sb1 = new StringBuilder();
            var sb2 = new StringBuilder();
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                sb1.Append($", \"{property.Name}\"");
                sb2.Append($", :{property.Name}");
            }

            return $"INSERT INTO \"{type.Name.ToPlural()}\"({sb1.ToString().TrimStart(',')}) VALUES({sb2.ToString().TrimStart(',')})";
        }

        /// <summary>
        /// 生成 Insert SQL语句
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <returns></returns>
        private static string CreateSqlServerInsertSql(Type type)
        {
            var sb1 = new StringBuilder();
            var sb2 = new StringBuilder();
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                sb1.Append($", {property.Name}");
                sb2.Append($", @{property.Name}");
            }

            return $"INSERT INTO {type.Name.ToPlural()}({sb1.ToString().TrimStart(',')}) VALUES({sb2.ToString().TrimStart(',')})";
        }

        /// <summary>
        /// 生成 Insert SQL语句
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <returns></returns>
        private static string CreateMySqlInsertSql(Type type)
        {
            var sb1 = new StringBuilder();
            var sb2 = new StringBuilder();
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                sb1.Append($", {property.Name}");
                sb2.Append($", '${property.Name}'");
            }

            return $"INSERT INTO {type.Name.ToPlural()}({sb1.ToString().TrimStart(',')}) VALUES({sb2.ToString().TrimStart(',')})";
        }

        #endregion

        #region Update SQL

        /// <summary>
        /// 生成 Update SQL语句
        /// </summary>
        /// <param name="type">对象</param>
        /// <returns></returns>
        public static string CreateUpdateSql(Type type)
        {
            switch (Configuration.DatabaseType)
            {
                case DatabaseType.SQLServer:
                    return CreateSqlServerUpdateSql(type);
                case DatabaseType.Oracle:
                    return CreateOracleUpdateSql(type);
                case DatabaseType.MySQL:
                    return CreateMySqlUpdateSql(type);
            }

            throw new ArgumentException($"暂不支持该类型的数据库：{Configuration.DatabaseType}");
        }

        /// <summary>
        /// 生成 Update SQL语句
        /// </summary>
        /// <param name="type">对象</param>
        /// <returns></returns>
        private static string CreateSqlServerUpdateSql(Type type)
        {
            var properties = type.GetProperties();
            if (!properties.Any(u => u.Name == "Id"))
            {
                throw new ArgumentNullException($"对象 {type} 没有Id属性");
            }
            var sb1 = new StringBuilder();
            foreach (var property in properties)
            {
                sb1.Append($", {property.Name}=@{property.Name}");
            }

            return $"UPDATE {type.Name.ToPlural()} SET {sb1.ToString().TrimStart(',')} WHERE Id=@Id ";
        }

        /// <summary>
        /// 生成 Update SQL语句
        /// </summary>
        /// <param name="type">对象</param>
        /// <returns></returns>
        private static string CreateOracleUpdateSql(Type type)
        {
            var properties = type.GetProperties();
            if (!properties.Any(u => u.Name == "Id"))
            {
                throw new ArgumentNullException($"对象 {type} 没有Id属性");
            }
            var sb1 = new StringBuilder();
            foreach (var property in properties)
            {
                sb1.Append($", \"{property.Name}\"=:{property.Name}");
            }

            return $"UPDATE \"{type.Name.ToPlural()}\" SET {sb1.ToString().TrimStart(',')} WHERE \"Id\"=:Id ";
        }

        /// <summary>
        /// 生成 Update SQL语句
        /// </summary>
        /// <param name="type">对象</param>
        /// <returns></returns>
        private static string CreateMySqlUpdateSql(Type type)
        {
            var properties = type.GetProperties();
            if (!properties.Any(u => u.Name == "Id"))
            {
                throw new ArgumentNullException($"对象 {type} 没有Id属性");
            }
            var sb1 = new StringBuilder();
            foreach (var property in properties)
            {
                sb1.Append($", {property.Name}='${property.Name}'");
            }

            return $"UPDATE {type.Name.ToPlural()} SET {sb1.ToString().TrimStart(',')} WHERE Id='$Id' ";
        }

        #endregion

        #region Delete SQL

        /// <summary>
        /// 生成 Delete SQL语句
        /// </summary>
        /// <param name="type">对象</param>
        /// <returns></returns>
        public static string CreateDeleteSql(Type type)
        {
            switch (Configuration.DatabaseType)
            {
                case DatabaseType.SQLServer:
                    return CreateSqlServerDeleteSql(type);
                case DatabaseType.Oracle:
                    return CreateOracleDeleteSql(type);
                case DatabaseType.MySQL:
                    return CreateMySqlDeleteSql(type);
            }

            throw new ArgumentException($"暂不支持该类型的数据库：{Configuration.DatabaseType}");
        }

        /// <summary>
        /// 生成 Delete SQL语句
        /// </summary>
        /// <param name="type">对象</param>
        /// <returns></returns>
        private static string CreateSqlServerDeleteSql(Type type)
        {
            var properties = type.GetProperties();
            if (!properties.Any(u => u.Name == "Id"))
            {
                throw new ArgumentNullException($"对象 {type.GetType()} 没有Id属性");
            }
            return $"DELETE {type.Name.ToPlural()} WHERE Id=@Id ";
        }

        /// <summary>
        /// 生成 Delete SQL语句
        /// </summary>
        /// <param name="type">对象</param>
        /// <returns></returns>
        private static string CreateOracleDeleteSql(Type type)
        {
            var properties = type.GetProperties();
            if (!properties.Any(u => u.Name == "Id"))
            {
                throw new ArgumentNullException($"对象 {type.GetType()} 没有Id属性");
            }
            return $"DELETE \"{type.Name.ToPlural()}\" WHERE \"Id\"=:Id ";
        }

        /// <summary>
        /// 生成 Delete SQL语句
        /// </summary>
        /// <param name="type">对象</param>
        /// <returns></returns>
        private static string CreateMySqlDeleteSql(Type type)
        {
            var properties = type.GetProperties();
            if (!properties.Any(u => u.Name == "Id"))
            {
                throw new ArgumentNullException($"对象 {type.GetType()} 没有Id属性");
            }
            return $"DELETE {type.Name.ToPlural()} WHERE Id='$Id' ";
        }

        #endregion

        #endregion
    }
}
