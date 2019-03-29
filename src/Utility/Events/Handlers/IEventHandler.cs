#region IEventHandler 文件信息
/***********************************************************
**文 件 名：IEventHandler 
**命名空间：Utility.Events.Handlers 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2019-03-14 13:08:54 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion


using System.Threading.Tasks;

namespace Utility.Events.Handlers
{
    /// <summary>
    /// 泛型事件处理器
    /// </summary>
    /// <typeparam name="TEvent"></typeparam>
    public interface IEventHandler<in TEvent> where TEvent : IEvent
    {
        /// <summary>
        /// 处理事件
        /// </summary>
        /// <param name="event">事件</param>
        /// <returns></returns>
        Task HandleAsync(TEvent @event);
    }
}
