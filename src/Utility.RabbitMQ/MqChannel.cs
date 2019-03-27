using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Framing;
using System;

namespace Utility.RabbitMQ
{
    /// <summary>
    /// 通道类，用于订阅、发布消息，同时提供一个关闭通道连接的方法 Stop
    /// </summary>
    public class MqChannel
    {
        /// <summary>
        /// ExchangeType
        /// </summary>
        public string ExchangeType { get; set; }

        /// <summary>
        /// Exchange
        /// </summary>
        public string Exchange { get; set; }

        /// <summary>
        /// QueueName
        /// </summary>
        public string Queue { get; set; }

        /// <summary>
        /// Routingkey
        /// </summary>
        public string Routingkey { get; set; }

        /// <summary>
        /// 连接
        /// </summary>
        public IConnection Connection { get; set; }

        /// <summary>
        /// 消费者
        /// </summary>
        public EventingBasicConsumer Consumer { get; set; }

        /// <summary>
        ///  外部订阅消费者通知委托
        /// </summary>
        public Action<MessageBody> OnReceived { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exchangeType"></param>
        /// <param name="exchange"></param>
        /// <param name="queue"></param>
        /// <param name="routekey"></param>
        public MqChannel(string exchangeType, string exchange, string queue, string routekey)
        {
            ExchangeType = exchangeType;
            Exchange = exchange;
            Queue = queue;
            Routingkey = routekey;
        }

        /// <summary>
        ///  向当前队列发送消息
        /// </summary>
        /// <param name="content"></param>
        public void Publish(string content)
        {
            var body = MqConnection.Utf8.GetBytes(content);
            IBasicProperties prop = new BasicProperties
            {
                DeliveryMode = 1
            };
            Consumer.Model.BasicPublish(Exchange, Routingkey, false, prop, body);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Receive(object sender, BasicDeliverEventArgs e)
        {
            var body = new MessageBody();
            try
            {
                var content = MqConnection.Utf8.GetString(e.Body);
                body.Content = content;
                body.Consumer = (EventingBasicConsumer)sender;
                body.BasicDeliver = e;
            }
            catch (Exception ex)
            {
                body.Content = $"订阅-出错{ex.Message}";
                body.Success = false;
                body.Code = 500;
            }
            OnReceived?.Invoke(body);
        }

        /// <summary>
        ///  设置消息处理完成标志
        /// </summary>
        /// <param name="consumer"></param>
        /// <param name="deliveryTag"></param>
        /// <param name="multiple"></param>
        public void SetBasicAck(EventingBasicConsumer consumer, ulong deliveryTag, bool multiple)
        {
            consumer.Model.BasicAck(deliveryTag, multiple);
        }

        /// <summary>
        ///  关闭消息队列的连接
        /// </summary>
        public void Stop()
        {
            if (Connection != null && Connection.IsOpen)
            {
                Connection.Close();
                Connection.Dispose();
            }
        }
    }
}
