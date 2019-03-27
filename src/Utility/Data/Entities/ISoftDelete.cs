#region ISoftDelete 文件信息
/***********************************************************
**文 件 名：ISoftDelete 
**命名空间：Utility.Data 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2018/9/4 15:52:53 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion


namespace Utility.Data
{
    /// <summary>
    /// 软删除接口
    /// </summary>
    public interface ISoftDelete
    {
        /// <summary>
        /// 是否标记软删除
        /// </summary>
        bool IsDelete { get; set; }
    }
}
