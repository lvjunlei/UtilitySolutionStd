#region CommandBus 文件信息
/***********************************************************
**文 件 名：CommandBus 
**命名空间：Utility.Commands 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2019/6/17 16:06:54 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明：版权所有，盗版必究
************************************************************/
#endregion

using System;
using System.Threading.Tasks;

namespace Utility.Commands
{
    /// <summary>
    /// CommandBus默认实现
    /// </summary>
    public class CommandBus : ICommandBus
    {
        private readonly ICommandHandlerFactory _handlerFactory;

        public CommandBus(ICommandHandlerFactory handlerFactory)
        {
            _handlerFactory = handlerFactory;
        }

        /// <summary>
        /// 发布命令
        /// </summary>
        /// <typeparam name="TCommand">Command类型</typeparam>
        /// <param name="cmd">命令</param>
        public async Task PublishAsync<TCommand>(TCommand cmd) where TCommand : ICommand
        {
            // 获取处理程序
            var handler = await _handlerFactory.GetHandlerAsync<TCommand>();
            if (handler == null)
            {
                throw new Exception($"未找到Command类型：{typeof(TCommand)} 对应的处理程序");
            }

            // 执行命令
            await handler.HandleAsync(cmd);
        }
    }
}
