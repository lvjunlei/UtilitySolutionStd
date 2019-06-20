#region IMessageHandlerFactory 文件信息
/***********************************************************
**文 件 名：IMessageHandlerFactory 
**命名空间：Utility.Messages 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2019/6/17 16:52:25 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明：版权所有，盗版必究
************************************************************/
#endregion

using System.Threading.Tasks;

namespace Utility.Messages
{
    /// <summary>
    /// IMessageHandlerFactory
    /// </summary>
    public interface IMessageHandlerFactory
    {
        /// <summary>
        /// 获取 MessageHandler 处理器
        /// </summary>
        /// <typeparam name="TMessage">Message类型</typeparam>
        /// <returns></returns>
        Task<IMessageHandler<TMessage>> GetHandlerAsync<TMessage>() where TMessage : IMessage;
    }
}
