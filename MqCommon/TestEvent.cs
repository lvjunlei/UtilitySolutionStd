#region TestEvent 文件信息
/***********************************************************
**文 件 名：TestEvent 
**命名空间：MqCommon 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2019-03-30 20:56:07 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion

using System;
using Utility.Events;

namespace MqCommon
{
    public class TestEvent : IEvent
    {
        public Guid Id { get; set; }

        public DateTime EventTime { get; set; }

        public object Message { get; set; }

        public TestEvent()
        {
            Id = Guid.NewGuid();
            EventTime = DateTime.Now;
        }
    }
}
