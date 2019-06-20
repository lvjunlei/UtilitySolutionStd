#region IEventHandlerFactory 文件信息
/***********************************************************
**文 件 名：IEventHandlerFactory 
**命名空间：Utility.Events 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2019/6/17 15:41:37 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明：版权所有，盗版必究
************************************************************/
#endregion

using Utility.Events.Handlers;

namespace Utility.Events
{
    /// <summary>
    /// IEventHandlerFactory
    /// </summary>
    public interface IEventHandlerFactory
    {
        /// <summary>
        /// 获取 EventHandler
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <returns></returns>
        IEventHandler<TEvent> GetEventHandler<TEvent>() where TEvent : IEvent;
    }
}
