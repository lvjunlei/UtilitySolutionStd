#region ServiceCollectionExtensions 文件信息
/***********************************************************
**文 件 名：ServiceCollectionExtensions 
**命名空间：Utility.EntityFramework.Extensions 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2019-02-27 15:52:05 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion


using Microsoft.Extensions.DependencyInjection;
using Utility.Data;

namespace Utility.EntityFramework.Extensions
{
    /// <summary>
    /// IServiceCollectionExtensions
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            var provider = services.BuildServiceProvider();
            Configuration.SetResolver(new ContainerAdapter(provider));
            return services;
        }
    }
}
