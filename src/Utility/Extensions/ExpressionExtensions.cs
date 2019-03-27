#region ExpressionExtensions 文件信息
/***********************************************************
**文 件 名：ExpressionExtensions 
**命名空间：Utility.Extensions 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2019-03-12 15:52:20 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Utility.Extensions
{
    /// <summary>
    /// ExpressionExtensions
    /// </summary>
    public static class ExpressionExtensions
    {
        /// <summary>
        /// And 表达式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            return Compose(left, right, Expression.AndAlso);
        }

        /// <summary>
        /// Or 表达式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            return Compose(left, right, Expression.OrElse);
        }

        private static Expression<T> Compose<T>(this Expression<T> left, Expression<T> right, Func<Expression, Expression, Expression> merge)
        {
            // build parameter map (from parameters of right to parameters of left)
            var map = left.Parameters.Select((f, i) => new { f, s = right.Parameters[i] }).ToDictionary(p => p.s, p => p.f);

            // replace parameters in the right lambda expression with parameters from the left
            var rightBody = ParameterRebinder.ReplaceParameters(map, right.Body);

            // apply composition of lambda expression bodies to parameters from the left expression 
            return Expression.Lambda<T>(merge(left.Body, rightBody), left.Parameters);
        }

        partial class ParameterRebinder : ExpressionVisitor
        {
            private readonly Dictionary<ParameterExpression, ParameterExpression> map;

            public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
            {
                this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
            }

            public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
            {
                return new ParameterRebinder(map).Visit(exp);
            }

            protected override Expression VisitParameter(ParameterExpression p)
            {
                if (map.TryGetValue(p, out ParameterExpression replacement))
                {
                    p = replacement;
                }
                return base.VisitParameter(p);
            }
        }
    }
}
