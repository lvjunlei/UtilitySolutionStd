#region MultiSlaveStrategy 文件信息
/***********************************************************
**文 件 名：MultiSlaveStrategy 
**命名空间：Utility.Data 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2018/7/26 9:24:46 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion

using System;
using System.Linq;

namespace Utility.Data
{
    /// <summary>
    /// 多从/读数据库策略
    /// </summary>
    public class MultiSlaveStrategy : ISlaveStrategy
    {
        /// <summary>
        /// 锁对象
        /// </summary>
        private static readonly object LockObj = new object();

        /// <summary>
        /// 实例对象
        /// </summary>
        private static ISlaveStrategy _instance;

        private Random Random { get; }

        /// <summary>
        /// 获取 MultiSlaveStrategy 对象单例实例
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
                            _instance = new MultiSlaveStrategy();
                        }
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        private MultiSlaveStrategy()
        {
            Random = new Random();
        }

        /// <summary>
        /// 获取数据库DbContext
        /// </summary>
        /// <returns></returns>
        public ISlaveDbContext GetDbContext()
        {
            var services = Configuration.Container.Resolves<ISlaveDbContext>() as ISlaveDbContext[];

            if (services == null || !services.Any())
            {
                throw new Exception("未找到已注册的读/从数据库实例！");
            }
            // 采用随机方式选择服务
            return services[Random.Next(services.Length)];
        }
    }
}
