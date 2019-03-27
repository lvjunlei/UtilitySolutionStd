#region IEvent 文件信息
/***********************************************************
**文 件 名：IEvent 
**命名空间：Utility.Events 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2019-03-14 11:28:18 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion


using System;

namespace Utility.Events
{
    /// <summary>
    /// IEvent
    /// </summary>
    public interface IEvent
    {
        /// <summary>
        /// 事件ID
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// 事件发生时间
        /// </summary>
        DateTime EventTime { get; }
    }
}
