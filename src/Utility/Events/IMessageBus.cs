#region IMessageBus 文件信息
/***********************************************************
**文 件 名：IMessageBus 
**命名空间：Utility.Events 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2019-03-14 13:15:33 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion

using System.Threading.Tasks;

namespace Utility.Events
{
    /// <summary>
    /// 消息总线接口定义
    /// </summary>
    public interface IMessageBus
    {
        /// <summary>
        /// 发布消息
        /// </summary>
        /// <typeparam name="TMessage">消息类型</typeparam>
        /// <param name="message">消息</param>
        /// <returns></returns>
        Task PublishAsync<TMessage>(TMessage message) where TMessage : IMessage;

        /// <summary>
        /// 发布消息
        /// </summary>
        /// <param name="name">消息名称</param>
        /// <param name="data">消息数据</param>
        /// <param name="callback">回掉名称</param>
        /// <returns></returns>
        Task PublishAsync(string name, object data, string callback = default(string));
    }
}
