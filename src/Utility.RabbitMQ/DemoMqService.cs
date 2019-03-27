using RabbitMQ.Client;
using System;
using Utility.RabbitMQ.Common;

namespace Utility.RabbitMQ
{
    public class DemoMqService : MqServiceBase
    {
        public Action<MessageLevel, string, Exception> OnAction = null;
        public DemoMqService(MqConfig config) : base(config)
        {
            Queues.Add(new QueueInfo()
            {
                ExchangeType = ExchangeType.Direct,
                Name = "login-message",
                RouterKey = "pk",
                OnReceived = OnReceived
            });
        }

        public override string VHost => "/";
        public override string Exchange => "Abs.Refresh";


        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="message"></param>
        public override void OnReceived(MessageBody message)
        {
            try
            {
                Console.WriteLine(message.Content);
            }
            catch (Exception ex)
            {
                OnAction?.Invoke(MessageLevel.Error, ex.Message, ex);
            }
            message.Consumer.Model.BasicAck(message.BasicDeliver.DeliveryTag, true);

        }
    }
}
