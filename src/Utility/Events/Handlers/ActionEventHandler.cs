#region ActionEventHandler 文件信息
/***********************************************************
**文 件 名：ActionEventHandler 
**命名空间：Utility.Events.Handlers 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2019-03-28 16:09:45 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion

using System;
using System.Threading.Tasks;

namespace Utility.Events.Handlers
{
    /// <summary>
    /// 支持 Action 的事件处理器
    /// </summary>
    public class ActionEventHandler<TEvent> : IEventHandler<TEvent> where TEvent : IEvent
    {
        private readonly Action<TEvent> _action;

        /// <summary>
        /// 初始化 ActionEventHandler
        /// </summary>
        /// <param name="handler">处理器（处理方法）</param>
        public ActionEventHandler(Action<TEvent> handler)
        {
            _action = handler ?? throw new ArgumentNullException(nameof(handler));
        }

        /// <summary>
        /// 处理事件
        /// </summary>
        /// <param name="event">事件数据</param>
        /// <returns></returns>
        public Task HandleAsync(TEvent @event)
        {
            return Task.Run(() =>
            {
                _action(@event);
            });
        }
    }
}
