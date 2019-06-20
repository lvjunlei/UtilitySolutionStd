#region ICommandHandler 文件信息
/***********************************************************
**文 件 名：ICommandHandler 
**命名空间：Utility.Commands 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2019/6/17 15:32:57 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明：版权所有，盗版必究
************************************************************/
#endregion


using System.Threading.Tasks;

namespace Utility.Commands
{
    /// <summary>
    /// ICommandHandler接口定义
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    public interface ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="cmd">命令</param>
        Task HandleAsync(TCommand cmd);
    }
}
