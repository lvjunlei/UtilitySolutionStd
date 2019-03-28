﻿using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace MqConsumer
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
                using (var channel = connection.CreateModel())
                {
                    //声明一个Fanout类型的交换机
                    channel.ExchangeDeclare(exchange: "FanoutDemo", type: ExchangeType.Direct);

                    //声明一个消息队列并获取它的名字
                    var queueName = channel.QueueDeclare().QueueName;

                    Console.WriteLine("请输入要绑定的RoutingKey");
                    var routingKey = Console.ReadLine();
                    foreach (var key in routingKey.Split(','))
                    {
                        //把消息队列和交换机绑定
                        channel.QueueBind(exchange: "FanoutDemo", queue: queueName, routingKey: key);
                    }


                    //创建消费者
                    var consume = new EventingBasicConsumer(channel);

                    //把消费者和队列绑定
                    channel.BasicConsume(queue: queueName, autoAck: true, consumer: consume);

                    consume.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine($"收到消息：{message}");
                    };

                    Console.ReadLine();
                }
            }
        }
    }
}