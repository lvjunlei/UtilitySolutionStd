#region IMessageEventHandler 文件信息
/***********************************************************
**文 件 名：IMessageEventHandler 
**命名空间：Utility.Eventbus.RabbitMQ 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2019-03-29 22:35:00 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion

using Utility.Events;
using Utility.Events.Handlers;

namespace Utility.Events.Handlers
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEvent"></typeparam>
    public interface IMessageHandler<in TEvent> : IEventHandler<TEvent> where TEvent : IEvent
    {
    }
}
