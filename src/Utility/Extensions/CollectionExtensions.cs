#region CollectionExtensions 文件信息
/***********************************************************
**文 件 名：CollectionExtensions 
**命名空间：Utility.Extensions 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2019/6/13 10:46:03 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明：版权所有，盗版必究
************************************************************/
#endregion

using System.Collections.Generic;

namespace Utility.Extensions
{
    /// <summary>
    /// CollectionExtensions
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// 集合是否为NULL或者为空
        /// </summary>
        /// <typeparam name="T">集合类型</typeparam>
        /// <param name="source">集合对象</param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this ICollection<T> source)
        {
            return source == null || source.Count <= 0;
        }
    }
}
