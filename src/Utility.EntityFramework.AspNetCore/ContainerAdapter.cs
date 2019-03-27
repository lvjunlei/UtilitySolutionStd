#region ContainerAdapter 文件信息
/***********************************************************
**文 件 名：ContainerAdapter 
**命名空间：Utility.EntityFramework 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2019-02-27 16:16:35 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using Utility.Data;

namespace Utility.EntityFramework
{
    /// <summary>
    /// 
    /// </summary>
    public class ContainerAdapter : IContainerAdapter
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceProvider"></param>
        public ContainerAdapter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        public TService Resolve<TService>()
        {
            return _serviceProvider.GetService<TService>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public TService Resolve<TService>(string serviceName)
        {
            return _serviceProvider.GetServices<TService>()
                .FirstOrDefault(u => u.GetType().Name == serviceName);
        }

        /// <summary>
        /// NotImplementedException
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="serviceKey"></param>
        /// <returns></returns>
        public TService Resolve<TService>(object serviceKey)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取服务实例集合
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <returns></returns>
        public IEnumerable<TService> Resolves<TService>()
        {
            return _serviceProvider.GetServices<TService>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        public bool IsRegistered<TService>()
        {
            return _serviceProvider.GetService<TService>() != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public bool IsRegistered(Type serviceType)
        {
            return _serviceProvider.GetService(serviceType) != null;
        }
    }
}
