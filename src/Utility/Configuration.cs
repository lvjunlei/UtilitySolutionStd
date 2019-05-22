#region Configuration 文件信息
/***********************************************************
**文 件 名：Configuration 
**命名空间：Utility 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2018/7/26 17:56:51 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion

using System;
using Utility.Data;

namespace Utility
{
    /// <summary>
    /// Utility配置项
    /// </summary>
    public static class Configuration
    {
        #region IOC 容器配置

        /// <summary>
        /// DependenceResolve
        /// </summary>
        public static IContainerAdapter Container { get; private set; }

        /// <summary>
        /// 设置IOC容器
        /// </summary>
        /// <param name="container">IContainerAdapter</param>
        public static void SetResolver(IContainerAdapter container)
        {
            Container = container;
        }

        /// <summary>
        /// 获取指定类型的服务对象
        /// </summary>
        /// <typeparam name="TService">服务对象类型</typeparam>
        /// <returns>服务对象实例</returns>
        public static TService GetService<TService>()
        {
            if (!Container.IsRegistered<TService>())
            {
                throw new ArgumentException($"服务类型 {typeof(TService)} 未在容器中注册！");
            }
            return Container.Resolve<TService>();
        }

        #endregion

        #region 数据库策略

        /// <summary>
        /// 数据库类型
        /// 默认为 SQLServer 数据库
        /// </summary>
        public static DatabaseType DatabaseType { get; set; } = DatabaseType.SQLServer;

        /// <summary>
        /// 是否使用主-从（读-写）数据库模式
        /// 默认false，不使用
        /// </summary>
        public static bool IsUseMasterSlaveDatabase { get; private set; }

        /// <summary>
        /// 使用 主-从（读-写 分离）数据库模式
        /// </summary>
        public static void UseMasterSlaveDatabase()
        {
            IsUseMasterSlaveDatabase = true;
        }

        /// <summary>
        /// 是否使用一主多从（一夫多妻制）数据库模式
        /// 默认false，不使用
        /// </summary>
        public static bool IsUseMultiSlaveStrategy { get; private set; }

        /// <summary>
        /// 使用一主多从（一夫多妻制）数据库模式
        /// </summary>
        public static void UseMultiSlaveStrategy()
        {
            IsUseMultiSlaveStrategy = true;
        }

        #endregion
    }
}
