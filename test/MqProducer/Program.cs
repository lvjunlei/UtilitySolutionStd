using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Framing;

namespace MqProducer
{
    class Program
    {
        static void Main(string[] args)
        {
            //创建连接工厂
            var factory = new ConnectionFactory() { HostName = "localhost" };

            //创建连接
            using (var connection = factory.CreateConnection())
            {
                //创建会话
                using (var chancel = connection.CreateModel())
                {
                    //声明交换机
                    chancel.ExchangeDeclare(exchange: "FanoutDemo", type: ExchangeType.Direct);
                    chancel.QueueBind(exchange: "FanoutDemo", queue: "hello", routingKey: "rpc");
                    //创建消费者
                    var consume = new EventingBasicConsumer(chancel);

                    //把消费者和队列绑定
                    chancel.BasicConsume(queue: "hello", autoAck: true, consumer: consume);

                    consume.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine($"收到消息：{message}");
                    };
                    string readMsg = "helloWorld";
                    while (readMsg?.ToLower() != "exit")
                    {
                        var body = Encoding.UTF8.GetBytes(readMsg);
                        Console.WriteLine("请输入RoutingKey");
                        var routingKey = Console.ReadLine();
                        var bp=new BasicProperties();
                        bp.CorrelationId = Guid.NewGuid().ToString();
                        bp.ReplyTo = "hello";
                        
                        //给交换机发送消息
                        chancel.BasicPublish(exchange: "FanoutDemo", routingKey: routingKey, basicProperties:bp, body: body);
                        Console.WriteLine($"成功发送消息{readMsg}");
                        Console.WriteLine("请输入要发送的内容！");
                        readMsg = Console.ReadLine();
                    }
                }
            }
        }
    }
}
