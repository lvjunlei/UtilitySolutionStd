using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq;
using System.Reflection;

namespace Utility.EntityFramework
{
    /// <summary>
    /// EFCore帮助类
    /// </summary>
    public static class EfCoreHelper
    {
        /// <summary>
        /// 获取上下文
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        internal static DbContext GetDbContext(IQueryable query)
        {
            var bindingFlags = BindingFlags.NonPublic | BindingFlags.Instance;
            var memberInfo = typeof(EntityQueryProvider).GetField("_queryCompiler", bindingFlags);
            if (memberInfo != null)
            {
                var queryCompiler = memberInfo.GetValue(query.Provider);
                var fieldInfo = queryCompiler.GetType().GetField("_queryContextFactory", bindingFlags);
                if (fieldInfo != null)
                {
                    var queryContextFactory = fieldInfo.GetValue(queryCompiler);

                    var propertyInfo = typeof(RelationalQueryContextFactory).GetProperty("Dependencies", bindingFlags);
                    if (propertyInfo != null)
                    {
                        var dependencies = propertyInfo.GetValue(queryContextFactory);
                        var queryContextDependencies = typeof(DbContext).Assembly.GetType(typeof(QueryContextDependencies).FullName);
                        var property = queryContextDependencies.GetProperty("StateManager", bindingFlags | BindingFlags.Public);
                        if (property != null)
                        {
                            var stateManagerProperty = property.GetValue(dependencies);
                            var stateManager = (IStateManager)stateManagerProperty;

                            return stateManager.Context;
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 将query 转化为sql语句
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        internal static string ToSql<TEntity>(this IQueryable<TEntity> query) where TEntity : class
        {
            var queryCompiler = (QueryCompiler)QueryCompilerField.GetValue(query.Provider);
            var modelGenerator = (QueryModelGenerator)QueryModelGeneratorField.GetValue(queryCompiler);
            var queryModel = modelGenerator.ParseQuery(query.Expression);
            var database = (IDatabase)DataBaseField.GetValue(queryCompiler);
            var databaseDependencies = (DatabaseDependencies)DatabaseDependenciesField.GetValue(database);
            var queryCompilationContext = databaseDependencies.QueryCompilationContextFactory.Create(false);
            var modelVisitor = (RelationalQueryModelVisitor)queryCompilationContext.CreateQueryModelVisitor();

            modelVisitor.CreateQueryExecutor<TEntity>(queryModel);
            var sql = modelVisitor.Queries.First().ToString();
            return sql;
        }

        private static readonly TypeInfo QueryCompilerTypeInfo = typeof(QueryCompiler).GetTypeInfo();

        private static readonly FieldInfo QueryCompilerField = typeof(EntityQueryProvider).GetTypeInfo().DeclaredFields.First(x => x.Name == "_queryCompiler");

        private static readonly FieldInfo QueryModelGeneratorField = QueryCompilerTypeInfo.DeclaredFields.First(x => x.Name == "_queryModelGenerator");

        private static readonly FieldInfo DataBaseField = QueryCompilerTypeInfo.DeclaredFields.Single(x => x.Name == "_database");

        private static readonly PropertyInfo DatabaseDependenciesField = typeof(Database).GetTypeInfo().DeclaredProperties.Single(x => x.Name == "Dependencies");
    }
}
