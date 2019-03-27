using System;
using System.Threading.Tasks;
using Utility.Events;
using Utility.Events.Handlers;

namespace Utility.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var manager = new EventHandlerManager();
            manager.RegisterHandler(new TestEventHandler($"Hello 1"));
            manager.RegisterHandler(new Test2EventHandler($"Hello 2"));

            var bus = new EventBus(manager);

            bus.PublishAsync(new TestEvent()).Wait();
            Console.ReadLine();
        }
    }

    public class TestEventHandler : IEventHandler<TestEvent>
    {
        private string _msg;
        public TestEventHandler(string msg)
        {
            _msg = msg;
        }
        public async Task HandleAsync(TestEvent @event)
        {
            Console.WriteLine($"{@event.EventTime} 收到消息 {@event.Id}:{_msg}");
            await Task.CompletedTask;
        }
    }

    public class Test2EventHandler : IEventHandler<TestEvent>
    {
        private string _msg;
        public Test2EventHandler(string msg)
        {
            _msg = msg;
        }
        public async Task HandleAsync(TestEvent @event)
        {
            Console.WriteLine($"{@event.EventTime} 收到消息 {@event.Id}:{_msg}");
            await Task.CompletedTask;
        }
    }

    public class TestEvent : Event, IEvent
    {
    }
}
