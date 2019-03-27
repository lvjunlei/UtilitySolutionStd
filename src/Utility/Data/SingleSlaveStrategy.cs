#region SingleStrategy 文件信息
/***********************************************************
**文 件 名：SingleStrategy 
**命名空间：Utility.Data 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2018/7/26 9:23:46 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion


namespace Utility.Data
{
    /// <summary>
    /// 单个从/读数据库策略
    /// </summary>
    public class SingleSlaveStrategy : ISlaveStrategy
    {
        private static readonly object LockObj = new object();
        private static ISlaveStrategy _instance;

        /// <summary>
        /// 获取 SingleSlaveStrategy 对象单例实例
        /// </summary>
        public static ISlaveStrategy Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (LockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new SingleSlaveStrategy();
                        }
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// 获取数据库上下文
        /// </summary>
        /// <returns></returns>
        public ISlaveDbContext GetDbContext()
        {
            return Configuration.Container.Resolve<ISlaveDbContext>();
        }
    }
}
