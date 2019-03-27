using System;

namespace Utility.Extensions
{
    /// <summary>
    /// 用于标注字段 来自哪个表的的哪一列(仅限于有关联的表中)
    /// </summary>
    public class FromEntityAttribute : Attribute
    {
        /// <summary>
        /// 类名(表名)
        /// </summary>
        public string[] EntityNames { get; }

        /// <summary>
        /// 字段(列名)
        /// </summary>
        public string EntityColuum { get; }

        /// <summary>
        /// 列名 + 该列的表名 + 该列的表的上一级表名
        /// </summary>
        /// <param name="entityColuum"></param>
        /// <param name="entityNames"></param>
        public FromEntityAttribute(string entityColuum, params string[] entityNames)
        {
            EntityNames = entityNames;
            EntityColuum = entityColuum;
        }
    }
}
