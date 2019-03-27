#region PageData 文件信息
/***********************************************************
**文 件 名：PageData 
**命名空间：Afas.Dtos 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2017/9/19 12:52:32 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion

using System.Collections.Generic;

namespace Utility.Data
{
    /// <summary>
    /// 分页数据
    /// </summary>
    /// <typeparam name="T">分页数据类型</typeparam>
    public class PageData<T>
    {
        /// <summary>
        /// 数据总数
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// 当前分页索引
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 分页大小
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 当前页面数据
        /// </summary>
        public IEnumerable<T> Datas { get; set; }

        public PageData()
        {
            Datas = new List<T>();
        }
    }
}
