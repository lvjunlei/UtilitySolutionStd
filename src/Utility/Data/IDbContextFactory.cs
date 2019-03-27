#region IDbContyextFactory 文件信息
/***********************************************************
**文 件 名：IDbContyextFactory 
**命名空间：Utility.Data 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2018/7/26 8:41:40 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion


namespace Utility.Data
{
    /// <summary>
    /// DbContext工厂
    /// 支持一主多从模式
    /// </summary>
    public interface IDbContextFactory
    {
        /// <summary>
        /// 获取主数据库实例（写）
        /// </summary>
        /// <returns></returns>
        IMasterDbContext GetMasterDbContext();

        /// <summary>
        /// 获取从数据库实例（读）
        /// </summary>
        /// <returns></returns>
        ISlaveDbContext GetSlaveDbContext();
    }
}
