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
using System.Linq;
using System.Threading.Tasks;
using Utility.Events.Handlers;
using Utility.Extensions;

namespace Utility.Events
{
    /// <summary>
    /// 事件处理服务
    /// </summary>
    public class EventHandlerManager : IEventHandlerManager, IDisposable
    {
        #region 私有属性

        /// <summary>
        /// 事件字典集合
        /// </summary>
        private readonly ConcurrentDictionary<string, Dictionary<Type, object>> _handlers;

        /// <summary>
        /// EventHandlerManager单例实例
        /// </summary>
        private static EventHandlerManager _instance;

        /// <summary>
        /// 实例锁
        /// </summary>
        private static readonly object InstanceLock = new object();

        #endregion

        #region 公共属性

        /// <summary>
        /// EventHandlerManager单例实例
        /// </summary>
        public static EventHandlerManager Instance
        {
            get
            {
                lock (InstanceLock)
                {
                    return _instance ?? (_instance = new EventHandlerManager());
                }
            }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 初始化 EventHandlerManager
        /// </summary>
        private EventHandlerManager()
        {
            _handlers = new ConcurrentDictionary<string, Dictionary<Type, object>>();
        }

        #endregion

        #region GetHandlers

        /// <summary>
        /// 获取指定 事件类型 的事件处理器
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <returns>事件处理器集合</returns>
        public List<IEventHandler<TEvent>> GetHandlers<TEvent>() where TEvent : IEvent
        {
            var key = typeof(TEvent).Name;
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
        /// 获取指定 事件类型 的事件处理器
        /// 异步
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <returns>事件处理器集合</returns>
        public Task<List<IEventHandler<TEvent>>> GetHandlersAsync<TEvent>() where TEvent : IEvent
        {
            return Task.Run(() => GetHandlers<TEvent>());
        }

        #endregion

        #region Register

        /// <summary>
        /// 注册事件处理器
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="handler">事件处理器</param>
        /// <returns></returns>
        public void Register<TEvent>(IEventHandler<TEvent> handler)
            where TEvent : class, IEvent
        {
            var key = typeof(TEvent).Name;
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
        /// <param name="handlers">事件处理器</param>
        /// <returns></returns>
        public void Registers<TEvent>(IEnumerable<IEventHandler<TEvent>> handlers)
            where TEvent : class, IEvent
        {
            foreach (var handler in handlers)
            {
                Register(handler);
            }
        }

        #endregion

        #region Unregister

        /// <summary>
        /// 取消指定类型的事件处理器
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="handlerType">处理器类型</param>
        public void Unregister<TEvent>(Type handlerType)
            where TEvent : class, IEvent
        {
            var key = typeof(TEvent).Name;
            if (_handlers.ContainsKey(key))
            {
                if (_handlers[key].ContainsKey(handlerType))
                {
                    _handlers[key].TryRemove(handlerType);
                }
            }
        }

        /// <summary>
        /// 取消指定 事件类型 的所有事件处理器
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        public void Unregister<TEvent>()
            where TEvent : class, IEvent
        {
            var key = typeof(TEvent).Name;
            if (_handlers.ContainsKey(key))
            {
                foreach (var handler in _handlers[key].Values)
                {
                    Unregister<TEvent>(handler.GetType());
                }
            }
        }

        /// <summary>
        /// 取消指定的事件处理器
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="handler">处理器</param>
        public void Unregister<TEvent>(IEventHandler<TEvent> handler)
            where TEvent : class, IEvent
        {
            Unregister<TEvent>(handler.GetType());
        }

        #endregion

        #region Publish

        /// <summary>
        /// 发布事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="event">事件</param>
        /// <returns></returns>
        public Task PublishAsync<TEvent>(TEvent @event) where TEvent : IEvent
        {
            return Task.Run(() =>
            {
                var handlers = GetHandlers<TEvent>();
                if (handlers == null || !handlers.Any())
                {
                    return;
                }
                foreach (var handler in handlers)
                {
                    handler?.HandleAsync(@event);
                }
            });
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            _handlers.Clear();
        }

        #endregion
    }
}
