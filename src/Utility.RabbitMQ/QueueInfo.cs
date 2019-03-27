using System;
using Utility.RabbitMQ.Common;

namespace Utility.RabbitMQ
{
    /// <summary>
    /// RabbitMQ 队列信息
    /// </summary>
    public class QueueInfo
    {
        /// <summary>
        ///  队列名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///  路由名称
        /// </summary>
        public string RouterKey { get; set; }

        /// <summary>
        ///  交换机类型
        /// </summary>
        public string ExchangeType { get; set; }

        /// <summary>
        ///  接受消息委托
        /// </summary>
        public Action<MessageBody> OnReceived { get; set; }

        /// <summary>
        /// 输出信息到客户端
        /// </summary>
        public Action<MqChannel, MessageLevel, string> OnAction { get; set; }
    }
}
