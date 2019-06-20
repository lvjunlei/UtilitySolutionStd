#region ICommandBus 文件信息
/***********************************************************
**文 件 名：ICommandBus 
**命名空间：Utility.Commands 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2019/6/17 16:04:40 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明：版权所有，盗版必究
************************************************************/
#endregion


using System.Threading.Tasks;

namespace Utility.Commands
{
    /// <summary>
    /// ICommandBus
    /// </summary>
    public interface ICommandBus
    {
        /// <summary>
        /// 发送命令
        /// </summary>
        /// <typeparam name="TCommand">Command类型</typeparam>
        /// <param name="cmd">命令</param>
        Task PublishAsync<TCommand>(TCommand cmd) where TCommand : ICommand;
    }
}
