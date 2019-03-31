#region MessageHandlerTest 文件信息
/***********************************************************
**文 件 名：MessageHandlerTest 
**命名空间：MqConsumer 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2019-03-31 13:27:39 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion

using MqCommon;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Utility.Events.Handlers;

namespace MqConsumer
{
    public class MessageHandlerTest : IMessageHandler<TestEvent>
    {
        public bool Durable => false;

        public bool Exclusive => false;

        public bool AutoDelete => false;

        public Task HandleAsync(TestEvent @event)
        {
            Console.WriteLine($"【MessageHandlerTest】{@event.EventTime:yyyy-MM-dd HH:mm:ss.fff} | {@event.Id} {@event.Message}");
            return Task.CompletedTask;
        }
    }
}
