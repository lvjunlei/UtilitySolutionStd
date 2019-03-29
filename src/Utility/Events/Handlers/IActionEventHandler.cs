#region IActionEventHandler 文件信息
/***********************************************************
**文 件 名：IActionEventHandler 
**命名空间：Utility.Events.Handlers 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2019-03-28 17:03:37 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion


namespace Utility.Events.Handlers
{
    /// <summary>
    /// 基于 Action 的事件处理器
    /// </summary>
    /// <typeparam name="TEvent">事件</typeparam>
    public interface IActionEventHandler<in TEvent> : IEventHandler<TEvent> where TEvent : IEvent
    {
    }
}
