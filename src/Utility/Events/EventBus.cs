﻿#region EventBus 文件信息
/***********************************************************
**文 件 名：EventBus 
**命名空间：Utility.Events 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2019-03-14 13:28:40 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion

using System;
using System.Linq;
using System.Threading.Tasks;
using Utility.Events.Handlers;

namespace Utility.Events
{
    /// <summary>
    /// 基于内存的简单事件总线
    /// </summary>
    public class EventBus : IEventBus
    {
        /// <summary>
        /// 事件处理器服务
        /// </summary>
        public IEventHandlerManager Manager { get; }

        /// <summary>
        /// 初始化事件总线
        /// </summary>
        /// <param name="manager"></param>
        public EventBus(IEventHandlerManager manager)
        {
            Manager = manager ?? throw new ArgumentNullException(nameof(manager));
        }

        /// <summary>
        /// 发布事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="event">事件</param>
        /// <returns></returns>
        public async Task PublishAsync<TEvent>(TEvent @event) where TEvent : IEvent
        {
            var handlers = Manager.GetHandlers<TEvent>();
            if (handlers == null || !handlers.Any())
            {
                return;
            }
            foreach (var handler in handlers)
            {
                if (handler != null)
                {
                    await handler.HandleAsync(@event);
                }
            }
        }
    }
}