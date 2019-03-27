#region DbContextFactory 文件信息
/***********************************************************
**文 件 名：DbContextFactory 
**命名空间：Utility.Data 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2018/7/26 9:29:39 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion


//using System.Runtime.Remoting.Messaging;


namespace Utility.Data
{
    /// <summary>
    /// DbContextFactory
    /// </summary>
    public class DbContextFactory
    {
        private static readonly ISlaveStrategy Strategy;

        /// <summary>
        /// 当前请求涉及的仓储对象的scope生命的仓储对象
        /// </summary>
        //private readonly Hashtable Contexts;

        /// <summary>
        /// 初始化 DbContextFactory
        /// </summary>
        static DbContextFactory()
        {
            Strategy = Configuration.IsUseMultiSlaveStrategy
                ? MultiSlaveStrategy.Instance
                : SingleSlaveStrategy.Instance;
            //Contexts = new Hashtable();
        }

        /// <summary>
        /// 获取主/写数据库DbContext
        /// </summary>
        /// <returns></returns>
        public static IMasterDbContext GetMasterDbContext()
        {
            //            var key = $"MasterDbContext_{typeof(IMasterDbContext).Name}";
            //            if (!Contexts.ContainsKey(key))
            //            {
            //#if DEBUG
            //                Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} {System.Threading.Thread.CurrentThread.GetHashCode()} 新建MasterDbContext");
            //#endif
            //                var context = Configuration.Container.Resolve<IMasterDbContext>();
            //                Contexts.Add(key, context);
            //            }
            return Configuration.Container.Resolve<IMasterDbContext>();
        }

        /// <summary>
        /// 获取从/读数据库DbContext
        /// </summary>
        /// <returns></returns>
        public static ISlaveDbContext GetSlaveDbContext()
        {
            //            var key = $"SlaveDbContext_{typeof(ISlaveDbContext).Name}";
            //            if (!Contexts.ContainsKey(key))
            //            {
            //#if DEBUG
            //                Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} {System.Threading.Thread.CurrentThread.GetHashCode()} 新建SlaveDbContext");
            //#endif
            //                var context = Configuration.IsUseMasterSlaveDatabase
            //                    ? Strategy.GetDbContext()
            //                    : GetMasterDbContext();
            //                Contexts.Add(key, context);
            //            }
            return (ISlaveDbContext)(Configuration.IsUseMasterSlaveDatabase
                    ? Strategy.GetDbContext()
                    : GetMasterDbContext());
        }
    }
}
