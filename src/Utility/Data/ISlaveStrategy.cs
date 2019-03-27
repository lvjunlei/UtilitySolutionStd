#region IReadDbStrategy 文件信息
/***********************************************************
**文 件 名：IReadDbStrategy 
**命名空间：Utility.Data 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2018/7/26 9:21:44 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion


namespace Utility.Data
{
    /// <summary>
    /// 从数据库获取策略接口
    /// </summary>
    public interface ISlaveStrategy
    {
        /// <summary>
        /// 获取读库
        /// </summary>
        /// <returns></returns>
        ISlaveDbContext GetDbContext();
    }
}
