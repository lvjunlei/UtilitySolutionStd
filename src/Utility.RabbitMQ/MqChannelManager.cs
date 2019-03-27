using System.Collections.Generic;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Utility.RabbitMQ
{
    /// <summary>
    /// RabbitMQ 通道管理类，用于创建通道，
    /// 只有一个公共方法 CreateReceiveChannel，传入相关参数，创建一个 MQChannel 对象
    /// </summary>
    public class MqChannelManager
    {
        public MqConnection MqConn { get; set; }

        public MqChannelManager(MqConnection conn)
        {
            MqConn = conn;
        }

        /// <summary>
        /// 创建消息通道
        /// </summary>
        /// <param name="exchangeType"></param>
        /// <param name="exchange"></param>
        /// <param name="queue"></param>
        /// <param name="routekey"></param>
        /// <returns></returns>
        public MqChannel CreateReceiveChannel(string exchangeType, string exchange, string queue, string routekey)
        {
            var model = CreateModel(exchangeType, exchange, queue, routekey);
            model.BasicQos(0, 1, false);
            var consumer = CreateConsumer(model, queue);
            var channel = new MqChannel(exchangeType, exchange, queue, routekey)
            {
                Connection = MqConn.Connection,
                Consumer = consumer
            };
            consumer.Received += channel.Receive;
            return channel;
        }

        /// <summary>
        ///  创建一个通道，包含交换机/队列/路由，并建立绑定关系
        /// </summary>
        /// <param name="type">交换机类型</param>
        /// <param name="exchange">交换机名称</param>
        /// <param name="queue">队列名称</param>
        /// <param name="routeKey">路由名称</param>
        /// <returns></returns>
        private IModel CreateModel(string type, string exchange, string queue, string routeKey, IDictionary<string, object> arguments = null)
        {
            type = string.IsNullOrEmpty(type) ? "default" : type;
            var model = MqConn.Connection.CreateModel();
            model.BasicQos(0, 1, false);
            model.QueueDeclare(queue, true, false, false, arguments);
            model.QueueBind(queue, exchange, routeKey);
            return model;
        }

        /// <summary>
        ///  接收消息到队列中
        /// </summary>
        /// <param name="model">消息通道</param>
        /// <param name="queue">队列名称</param>
        /// <param name="callback">订阅消息的回调事件</param>
        /// <returns></returns>
        private EventingBasicConsumer CreateConsumer(IModel model, string queue)
        {
            var consumer = new EventingBasicConsumer(model);
            model.BasicConsume(queue, false, consumer);

            return consumer;
        }
    }
}
