#region DynamicWebApiServiceExtensions 文件信息
/***********************************************************
**文 件 名：DynamicWebApiServiceExtensions 
**命名空间：Utility.DynamicWebApi 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2019/6/13 9:23:43 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明：版权所有，盗版必究
************************************************************/
#endregion

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace Utility.Extensions
{
    /// <summary>
    /// IServiceCollectionExtensions
    /// </summary>
    public static partial class IServiceCollectionExtensions
    {
        #region ServiceCollectionExtensions

        /// <summary>
        /// 是否已存在指定类型的服务
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <param name="services">IServiceCollection对象</param>
        /// <returns></returns>
        public static bool Exists<TService>(this IServiceCollection services)
        {
            return services.Exists(typeof(TService));
        }

        /// <summary>
        /// 是否已存在指定类型的对象
        /// </summary>
        /// <param name="services">IServiceCollection对象</param>
        /// <param name="type">对象类型</param>
        /// <returns></returns>
        public static bool Exists(this IServiceCollection services, Type type)
        {
            return services.Any(d => d.ServiceType == type);
        }

        /// <summary>
        /// 获取指定类型服务的单例
        /// 查询不到则返回NULL
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <param name="services">IServiceCollection</param>
        /// <returns></returns>
        public static TService TryGetSingletonInstance<TService>(this IServiceCollection services)
        {
            return (TService)services
                .FirstOrDefault(d => d.ServiceType == typeof(TService))
                ?.ImplementationInstance;
        }

        /// <summary>
        /// 获取指定类型服务的单例
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <param name="services">IServiceCollection</param>
        /// <returns></returns>
        public static TService GetSingletonInstance<TService>(this IServiceCollection services)
        {
            var service = services.TryGetSingletonInstance<TService>();
            if (service == null)
            {
                throw new InvalidOperationException("Could not find singleton service: " + typeof(TService).AssemblyQualifiedName);
            }

            return service;
        }

        /// <summary>
        /// 生成 IServiceProvider 对象
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        /// <returns></returns>
        public static IServiceProvider BuildServiceProviderFromFactory(this IServiceCollection services)
        {
            foreach (var service in services)
            {
                var factoryInterface = service.ImplementationInstance?.GetType()
                    .GetTypeInfo()
                    .GetInterfaces()
                    .FirstOrDefault(i => i.GetTypeInfo().IsGenericType &&
                                         i.GetGenericTypeDefinition() == typeof(IServiceProviderFactory<>));

                if (factoryInterface == null)
                {
                    continue;
                }

                var containerBuilderType = factoryInterface.GenericTypeArguments[0];
                return (IServiceProvider)typeof(IServiceCollectionExtensions)
                    .GetTypeInfo()
                    .GetMethods()
                    .Single(m => m.Name == nameof(BuildServiceProviderFromFactory) && m.IsGenericMethod)
                    .MakeGenericMethod(containerBuilderType)
                    .Invoke(null, new object[] { services, null });
            }

            return services.BuildServiceProvider();
        }

        /// <summary>
        /// 生成 IServiceProvider 对象
        /// </summary>
        /// <typeparam name="TContainerBuilder"></typeparam>
        /// <param name="services"></param>
        /// <param name="builderAction"></param>
        /// <returns></returns>
        public static IServiceProvider BuildServiceProviderFromFactory<TContainerBuilder>(this IServiceCollection services, Action<TContainerBuilder> builderAction = null)
        {
            var serviceProviderFactory = services.TryGetSingletonInstance<IServiceProviderFactory<TContainerBuilder>>();
            if (serviceProviderFactory == null)
            {
                throw new Exception($"Could not find {typeof(IServiceProviderFactory<TContainerBuilder>).FullName} in {services}.");
            }

            var builder = serviceProviderFactory.CreateBuilder(services);
            builderAction?.Invoke(builder);
            return serviceProviderFactory.CreateServiceProvider(builder);
        }

        #endregion
    }
}
