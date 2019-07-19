using Eventbus.Common;
using Mapster;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Eventbus.RabbitMQ;
using Utility.Events.Handlers;

namespace ObjectMapperTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var mqconfig = new MqConfig
            {
                UserName = "admin",
                Password = "admin",
                HostIp = "localhost",
                Port = 5672,
                VirtualHost = "/",
                Exchange = "Abs.Exchange",
                ExchangeType = "direct",
                Durable = true,
                AutoDelete = false
            };
            var ehm = new MessageHandlerManager(mqconfig);
            //_eventbus = new EventBus(ehm);
            ehm.Register<HelloEvent>(new HelloEventHandler());
            Console.ReadLine();
        }

        public static string CreateInsertSql(Type type)
        {
            var sb1 = new StringBuilder();
            var sb2 = new StringBuilder();
            var properties = type.GetProperties().Where(u => u.PropertyType.IsValueType || u.PropertyType == typeof(string));
            foreach (var property in properties)
            {
                sb1.Append($", \"{property.Name}\"");
                sb2.Append($", :{property.Name}");
            }

            return $"INSERT INTO \"{type.Name}\"({sb1.ToString().TrimStart(',')}) VALUES({sb2.ToString().TrimStart(',')})";
        }

    }


    public class HelloEventHandler : IMessageHandler<HelloEvent>
    {

        public Task HandleAsync(HelloEvent @event)
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.ffff} | {@event.Data}");
            });
        }
    }
    public class StudentDto
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public DateTime EntryTime { get; set; }

        public string AddressCode { get; set; }

        public string AddressTelphone { get; set; }
    }



    public class Student
    {
        public string Name { get; set; }

        public long Age { get; set; }

        public DateTime EntryTime { get; set; }

        public Address Address { get; set; }

        public virtual ICollection<Address> As { get; set; }
    }

    public class Address
    {
        public string Code { get; set; }

        public string Telphone { get; set; }
    }
}
