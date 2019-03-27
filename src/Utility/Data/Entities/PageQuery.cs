#region PageQuery 文件信息
/***********************************************************
**文 件 名：PageQuery 
**命名空间：Afas.Dtos 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2017/9/19 13:23:39 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion


namespace Utility.Data
{
    /// <summary>
    /// 分页查询条件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageQuery<T>
    {
        /// <summary>
        /// 分页大小
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 分页号
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 查询分页条件
        /// </summary>
        public T Condition { get; set; }
    }
}
