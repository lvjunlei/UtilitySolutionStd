#region Event 文件信息
/***********************************************************
**文 件 名：Event 
**命名空间：Utility.Events 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2019-03-14 13:01:40 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion

using System;
using Utility.Helpers;

namespace Utility.Events
{
    /// <summary>
    /// 事件
    /// </summary>
    public class Event : IEvent
    {
        /// <summary>
        /// 事件标识
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// 事件发生时间
        /// </summary>
        public DateTime EventTime { get; }

        /// <summary>
        /// 
        /// </summary>
        public Event()
        {
            Id = GuidHelper.CreateSequentialGuid();
            EventTime = DateTime.Now;
        }
    }
}
