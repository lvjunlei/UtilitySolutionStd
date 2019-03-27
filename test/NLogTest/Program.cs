using NLog;
using System;

namespace NLogTest
{
    class Program
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            if (string.Empty == "")
            {
                Console.WriteLine();
            }
            //var logEvent = new LogEventInfo(LogLevel.Info, "", "Hello World /");
            //logEvent.Properties["account"] = "admin";

            var random=new Random();
            //logger.Log(logEvent);
            for (int i = 0; i < 1000; i++)
            {
                var v = random.Next(3);
                Console.WriteLine(v);
            }
            Console.WriteLine("执行完成");
            Console.ReadLine();
        }
    }
}
