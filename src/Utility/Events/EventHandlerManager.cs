#region EventHandlerManager 文件信息
/***********************************************************
**文 件 名：EventHandlerManager 
**命名空间：Utility.Events 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2019-03-14 13:37:12 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Utility.Events.Handlers;
using Utility.Extensions;

namespace Utility.Events
{
    /// <summary>
    /// 事件处理服务
    /// </summary>
    public class EventHandlerManager : IEventHandlerManager
    {
        private readonly ConcurrentDictionary<Type, Dictionary<Type, object>> _handlers;

        public EventHandlerManager()
        {
            _handlers = new ConcurrentDictionary<Type, Dictionary<Type, object>>();
        }

        /// <summary>
        /// 获取事件处理器
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <returns></returns>
        public IEnumerable<IEventHandler<TEvent>> GetHandlers<TEvent>() where TEvent : IEvent
        {
            var key = typeof(TEvent);
            var vs = new List<IEventHandler<TEvent>>();
            if (_handlers.ContainsKey(key))
            {
                foreach (var value in _handlers[key].Values)
                {
                    vs.Add((IEventHandler<TEvent>)value);
                }
            }

            return vs;
        }

        /// <summary>
        /// 注册事件处理器
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="handler">事件处理器</param>
        /// <returns></returns>
        public void RegisterHandler<TEvent>(IEventHandler<TEvent> handler) where TEvent : IEvent
        {
            var key = typeof(TEvent);
            if (!_handlers.ContainsKey(key))
            {
                _handlers.TryAdd(key, new Dictionary<Type, object>());
            }

            _handlers[key].AddOrUpdate(handler.GetType(), handler);
        }

        /// <summary>
        /// 批量注册事件处理器
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="handler">事件处理器</param>
        /// <returns></returns>
        public void RegisterHandlers<TEvent>(IEnumerable<IEventHandler<TEvent>> handlers) where TEvent : IEvent
        {
            var key = typeof(TEvent);
            if (!_handlers.ContainsKey(key))
            {
                _handlers.TryAdd(key, new Dictionary<Type, object>());
            }
            foreach (var handler in handlers)
            {
                _handlers[key].AddOrUpdate(handler.GetType(), handler);
            }
        }
    }
}
