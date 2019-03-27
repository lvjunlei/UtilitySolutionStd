#region IContainerAdapter 文件信息
/***********************************************************
**文 件 名：IContainerAdapter 
**命名空间：Utility.Framework 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2018/7/23 15:52:46 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion


using System;
using System.Collections.Generic;

namespace Utility.Data
{
    /// <summary>
    /// 容器接口适配器
    /// </summary>
    public interface IContainerAdapter
    {
        /// <summary>
        /// 获取指定类型的事例
        /// </summary>
        /// <typeparam name="TService">实例的类型</typeparam>
        /// <returns></returns>
        TService Resolve<TService>();

        /// <summary>
        /// 获取服务实例
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <param name="serviceName">服务名称</param>
        /// <returns></returns>
        TService Resolve<TService>(string serviceName);

        /// <summary>
        /// 获取服务实例
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <param name="serviceKey">服务KEY</param>
        /// <returns></returns>
        TService Resolve<TService>(object serviceKey);

        /// <summary>
        /// 获取服务实例集合
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <returns></returns>
        IEnumerable<TService> Resolves<TService>();

        /// <summary>
        /// 判断服务是否已注册
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        bool IsRegistered<TService>();

        /// <summary>
        /// 判断服务是否已注册
        /// </summary>
        /// <returns></returns>
        bool IsRegistered(Type serviceType);
    }
}
