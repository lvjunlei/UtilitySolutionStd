#region IEventHandlerManager 文件信息
/***********************************************************
**文 件 名：IEventHandlerManager 
**命名空间：Utility.Events.Handlers 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2019-03-14 13:11:49 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Utility.Events.Handlers
{
    /// <summary>
    /// 事件处理器服务接口定义
    /// </summary>
    public interface IEventHandlerManager
    {
        #region GetHandlers

        /// <summary>
        /// 获取指定 事件类型 的事件处理器
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <returns>事件处理器集合</returns>
        List<IEventHandler<TEvent>> GetHandlers<TEvent>() where TEvent : IEvent;

        /// <summary>
        /// 获取指定 事件类型 的事件处理器
        /// 异步
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <returns>事件处理器集合</returns>
        Task<List<IEventHandler<TEvent>>> GetHandlersAsync<TEvent>() where TEvent : IEvent;

        #endregion

        #region Register

        /// <summary>
        /// 注册事件处理器
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="handler">事件处理器</param>
        /// <returns></returns>
        void Register<TEvent>(IEventHandler<TEvent> handler) where TEvent : class, IEvent;

        /// <summary>
        /// 批量注册事件处理器
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="handlers">事件处理器</param>
        /// <returns></returns>
        void Registers<TEvent>(IEnumerable<IEventHandler<TEvent>> handlers)
            where TEvent : class, IEvent;

        #endregion

        #region Unregister

        /// <summary>
        /// 取消指定类型的事件处理器
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="handlerType">处理器类型</param>
        void Unregister<TEvent>(Type handlerType) where TEvent : class, IEvent;

        /// <summary>
        /// 取消指定 事件类型 的所有事件处理器
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        void Unregister<TEvent>() where TEvent : class, IEvent;

        /// <summary>
        /// 取消指定的事件处理器
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="handler">处理器</param>
        void Unregister<TEvent>(IEventHandler<TEvent> handler)
            where TEvent : class, IEvent;

        #endregion

        #region Publish

        /// <summary>
        /// 发布事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="event">事件</param>
        /// <returns></returns>
        Task PublishAsync<TEvent>(TEvent @event) where TEvent : IEvent;

        #endregion
    }
}
