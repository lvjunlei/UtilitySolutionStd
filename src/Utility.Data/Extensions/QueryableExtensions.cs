using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Utility.Extensions;
using static System.Linq.Expressions.Expression;

namespace Utility.EntityFramework.Extensions
{
    /// <summary>
    /// QueryableExtensions
    /// </summary>
    public static class QueryableExtensions
    {
        /// <summary>
        /// 查询目标数据
        /// </summary>
        /// <typeparam name="TModel">目标数据类型</typeparam>
        /// <param name="query">IQueryable对象</param>
        /// <returns></returns>
        public static IQueryable<TModel> Select<TModel>(this IQueryable<object> query)
        {
            return Queryable.Select(query, GetLamda<object, TModel>(query.GetType().GetGenericArguments()[0]));
        }

        /// <summary>
        /// 查询目标数据
        /// </summary>
        /// <typeparam name="TSource">源数据类型</typeparam>
        /// <typeparam name="TModel">目标数据类型</typeparam>
        /// <param name="query">IQueryable对象</param>
        /// <returns></returns>
        public static IQueryable<TModel> Select<TSource, TModel>(this IQueryable<TSource> query)
        {
            return Queryable.Select(query, GetLamda<TSource, TModel>());
        }

        /// <summary>
        /// 获取lamda表达式
        /// </summary>
        /// <typeparam name="TSource">源数据类型</typeparam>
        /// <typeparam name="TTarget">目标数据类型</typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        private static Expression<Func<TSource, TTarget>> GetLamda<TSource, TTarget>(Type type = null)
        {
            var sourceType = typeof(TSource);
            var targetType = typeof(TTarget);
            var parameter = Parameter(sourceType);
            Expression propertyParameter;
            if (type != null)
            {
                propertyParameter = Convert(parameter, type);
                sourceType = type;
            }
            else
                propertyParameter = parameter;

            return Lambda<Func<TSource, TTarget>>(GetExpression(propertyParameter, sourceType, targetType), parameter);
        }

        /// <summary>
        /// 获取表达式
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="sourceType"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        private static MemberInitExpression GetExpression(Expression parameter, Type sourceType, Type targetType)
        {
            var memberBindings = new List<MemberBinding>();
            foreach (var targetItem in targetType.GetProperties().Where(x => x.CanWrite))
            {
                var fromEntityAttr = targetItem.GetCustomAttribute<FromEntityAttribute>();
                if (fromEntityAttr != null)
                {
                    var property = GetFromEntityExpression(parameter, sourceType, fromEntityAttr);
                    if (property != null)
                        memberBindings.Add(Bind(targetItem, property));
                    continue;
                }

                var sourceItem = sourceType.GetProperty(targetItem.Name);
                if (sourceItem == null)//当没有对应的属性时，查找 实体名+属性
                {
                    var complexSourceItemProperty = GetCombinationExpression(parameter, sourceType, targetItem);
                    if (complexSourceItemProperty != null)
                        memberBindings.Add(Bind(targetItem, complexSourceItemProperty));
                    continue;
                }

                //判断实体的读写权限
                if (sourceItem == null || !sourceItem.CanRead)
                    continue;

                //标注NotMapped特性的属性忽略转换
                if (sourceItem.GetCustomAttribute<NotMappedAttribute>() != null)
                    continue;

                var sourceProperty = Property(parameter, sourceItem);

                //当非值类型且类型不相同时
                if (!sourceItem.PropertyType.IsValueType && sourceItem.PropertyType != targetItem.PropertyType && targetItem.PropertyType != targetType)
                {
                    //判断都是(非泛型、非数组)class
                    if (sourceItem.PropertyType.IsClass && targetItem.PropertyType.IsClass
                        && !sourceItem.PropertyType.IsArray && !targetItem.PropertyType.IsArray
                        && !sourceItem.PropertyType.IsGenericType && !targetItem.PropertyType.IsGenericType)
                    {
                        var expression = GetExpression(sourceProperty, sourceItem.PropertyType, targetItem.PropertyType);
                        memberBindings.Add(Bind(targetItem, expression));
                    }
                    continue;
                }

                if (targetItem.PropertyType != sourceItem.PropertyType)
                    continue;

                memberBindings.Add(Bind(targetItem, sourceProperty));
            }

            return MemberInit(New(targetType), memberBindings);
        }

        /// <summary>
        /// 根据FromEntityAttribute 的值获取属性对应的路径
        /// </summary>
        /// <param name="sourceProperty"></param>
        /// <param name="sourceType"></param>
        /// <param name="fromEntityAttribute"></param>
        /// <returns></returns>
        private static Expression GetFromEntityExpression(Expression sourceProperty, Type sourceType, FromEntityAttribute fromEntityAttribute)
        {
            var findType = sourceType;
            var resultProperty = sourceProperty;
            var tableNames = fromEntityAttribute.EntityNames;
            if (tableNames == null)
            {
                var columnProperty = findType.GetProperty(fromEntityAttribute.EntityColuum);
                if (columnProperty == null)
                    return null;
                else
                    return Property(resultProperty, columnProperty);
            }

            for (int i = tableNames.Length - 1; i >= 0; i--)
            {
                var tableProperty = findType.GetProperty(tableNames[i]);
                if (tableProperty == null)
                    return null;

                findType = tableProperty.PropertyType;
                resultProperty = Property(resultProperty, tableProperty);
            }

            var property = findType.GetProperty(fromEntityAttribute.EntityColuum);
            if (property == null)
                return null;
            else
                return Property(resultProperty, property);
        }

        /// <summary>
        /// 根据组合字段获取其属性路径
        /// </summary>
        /// <param name="sourceProperty"></param>
        /// <param name="sourcePropertys"></param>
        /// <param name="targetItem"></param>
        /// <returns></returns>
        private static Expression GetCombinationExpression(Expression sourceProperty, Type sourceType, PropertyInfo targetItem)
        {
            foreach (var item in sourceType.GetProperties().Where(x => x.CanRead))
            {
                if (targetItem.Name.StartsWith(item.Name))
                {
                    if (item != null && item.CanRead && item.PropertyType.IsClass && !item.PropertyType.IsGenericType)
                    {
                        var rightName = targetItem.Name.Substring(item.Name.Length);

                        var complexSourceItem = item.PropertyType.GetProperty(rightName);
                        if (complexSourceItem != null && complexSourceItem.CanRead)
                            return Property(Property(sourceProperty, item), complexSourceItem);
                    }
                }
            }

            return null;
        }
    }
}
