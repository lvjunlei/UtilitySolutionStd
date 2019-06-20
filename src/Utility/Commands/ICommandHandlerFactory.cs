#region ICommandHandlerFactory 文件信息
/***********************************************************
**文 件 名：ICommandHandlerFactory 
**命名空间：Utility.Commands 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2019/6/17 15:39:15 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明：版权所有，盗版必究
************************************************************/
#endregion


using System.Threading.Tasks;

namespace Utility.Commands
{
    /// <summary>
    /// ICommandHandlerFactory
    /// </summary>
    public interface ICommandHandlerFactory
    {
        /// <summary>
        /// 获取Command处理器
        /// </summary>
        /// <typeparam name="TCommand">Command类型</typeparam>
        /// <returns></returns>
        Task<ICommandHandler<TCommand>> GetHandlerAsync<TCommand>() where TCommand : ICommand;
    }
}
