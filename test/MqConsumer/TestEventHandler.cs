#region TestEventHandler 文件信息
/***********************************************************
**文 件 名：TestEventHandler 
**命名空间：MqConsumer 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2019-03-30 21:00:28 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion

using MqCommon;
using System;
using System.Threading.Tasks;
using Utility.Events.Handlers;

namespace MqConsumer
{
    public class TestEventHandler : IEventHandler<TestEvent>
    {
        public Task HandleAsync(TestEvent @event)
        {
            //throw new NotImplementedException();
            Console.WriteLine($"【TestEventHandler】{@event.EventTime:yyyy-MM-dd HH:mm:ss.fff} | {@event.Id} {@event.Message}");
            return Task.CompletedTask;
        }
    }
}
