namespace Utility.RabbitMQ
{
    /// <summary>
    /// MQConfig 类，用于存放 RabbitMQ 主机配置等信息
    /// </summary>
    public class MqConfig
    {
        /// <summary>
        /// 访问消息队列的用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 访问消息队列的密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 消息队列的主机地址
        /// </summary>
        public string HostIp { get; set; }

        /// <summary>
        /// 消息队列的主机开放的端口
        /// </summary>
        public int Port { get; set; }
    }
}
