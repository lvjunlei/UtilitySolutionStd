using RabbitMQ.Client.Events;

namespace Utility.RabbitMQ
{
    /// <summary>
    /// 消息体对象 MessageBody，用于解析和传递消息到业务系统中
    /// </summary>
    public class MessageBody
    {
        /// <summary>
        /// 消费者
        /// </summary>
        public EventingBasicConsumer Consumer { get; set; }

        /// <summary>
        /// BasicDeliver
        /// </summary>
        public BasicDeliverEventArgs BasicDeliver { get; set; }

        /// <summary>
        /// 成功标识
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        ///  0成功
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string Content { get; set; }
    }
}
