using System;
using Utility.RabbitMQ;
using Utility.RabbitMQ.Common;

namespace MqServiceTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Test();
        }

        static void Test()
        {
            var config = new MqConfig()
            {
                HostIp = "127.0.0.1",
                Port = 5672,
                UserName = "admin",
                Password = "admin"
            };

            var manager = new MqServcieManager();
            manager.AddService(new DemoMqService(config));
            manager.OnAction = OnActionOutput;
            manager.Start();

            Console.WriteLine("服务已启动");
            Console.ReadLine();

            manager.Stop();
            Console.WriteLine("服务已停止,按任意键退出...");
            Console.ReadLine();
        }

        static void OnActionOutput(MessageLevel level, string message, Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("{0} | {1} | {2}", level, message, ex?.StackTrace);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
