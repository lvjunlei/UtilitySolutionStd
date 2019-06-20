#region CommandHandlerFactory 文件信息
/***********************************************************
**文 件 名：CommandHandlerFactory 
**命名空间：Utility.Commands 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2019/6/17 15:45:48 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明：版权所有，盗版必究
************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Utility.Commands
{
    /// <summary>
    /// 默认 CommandHandler 工厂实现
    /// </summary>
    public class CommandHandlerFactory : ICommandHandlerFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandHandlerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// 获取 ICommandHandler
        /// </summary>
        /// <typeparam name="TCommand">Command类型</typeparam>
        /// <returns></returns>
        public async Task<ICommandHandler<TCommand>> GetHandlerAsync<TCommand>() where TCommand : ICommand
        {
            return await Task.Run(() =>
            {
                var types = GetHandlerTypes<TCommand>();
                if (!types.Any())
                {
                    return null;
                }

                var handler = _serviceProvider.GetService(types.FirstOrDefault()) as ICommandHandler<TCommand>;

                return handler;
            });
        }

        /// <summary>
        /// 查找Command对应的CommandHandler类型
        /// </summary>
        /// <typeparam name="TCommand">命令类型</typeparam>
        /// <returns></returns>
        private IEnumerable<Type> GetHandlerTypes<TCommand>()
            where TCommand : ICommand
        {
            var handlers = typeof(ICommandHandler<>).Assembly.GetExportedTypes()
                .Where(u => u.GetInterfaces()
                .Any(a => a.IsGenericType && a.GetGenericTypeDefinition() == typeof(ICommandHandler<>)))
                .Where(b => b.GetInterfaces()
                .Any(c => c.GetGenericArguments()
                .Any(d => d == typeof(TCommand))))
                .ToList();

            return handlers;
        }
    }
}
