﻿#region IEventHandlerManager 文件信息
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

using System.Collections.Generic;

namespace Utility.Events.Handlers
{
    /// <summary>
    /// 事件处理器服务接口定义
    /// </summary>
    public interface IEventHandlerManager
    {
        /// <summary>
        /// 获取事件处理器列表
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <returns>事件处理器列表</returns>
        IEnumerable<IEventHandler<TEvent>> GetHandlers<TEvent>() where TEvent : IEvent;

        /// <summary>
        /// 注册事件处理器
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="handler">事件处理器</param>
        /// <returns></returns>
        void RegisterHandler<TEvent>(IEventHandler<TEvent> handler) where TEvent : IEvent;

        /// <summary>
        /// 批量注册事件处理器
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="handler">事件处理器</param>
        /// <returns></returns>
        void RegisterHandlers<TEvent>(IEnumerable<IEventHandler<TEvent>> handlers) where TEvent : IEvent;
    }
}
