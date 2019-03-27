using System.Collections.Generic;

namespace Utility.RabbitMQ
{
    /// <summary>
    /// 抽象类来实现该接口，将实现细节进行封装，方便后面的业务服务继承调用
    /// </summary>
    public abstract class MqServiceBase : IMqService
    {
        internal bool Started = false;

        internal MqServiceBase(MqConfig config)
        {
            Config = config;
        }

        public MqChannel CreateChannel(string queue, string routeKey, string exchangeType)
        {
            var conn = new MqConnection(Config, VHost);
            var cm = new MqChannelManager(conn);
            var channel = cm.CreateReceiveChannel(exchangeType, Exchange, queue, routeKey);
            return channel;
        }

        /// <summary>
        ///  启动订阅
        /// </summary>
        public void Start()
        {
            if (Started)
            {
                return;
            }

            var conn = new MqConnection(Config, VHost);
            var manager = new MqChannelManager(conn);
            foreach (var item in Queues)
            {
                var channel = manager.CreateReceiveChannel(item.ExchangeType, Exchange, item.Name, item.RouterKey);
                channel.OnReceived = item.OnReceived;
                Channels.Add(channel);
            }
            Started = true;
        }

        /// <summary>
        ///  停止订阅
        /// </summary>
        public void Stop()
        {
            foreach (var c in Channels)
            {
                c.Stop();
            }
            Channels.Clear();
            Started = false;
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="message"></param>
        public abstract void OnReceived(MessageBody message);

        /// <summary>
        /// 
        /// </summary>
        public List<MqChannel> Channels { get; set; } = new List<MqChannel>();

        /// <summary>
        ///  消息队列配置
        /// </summary>
        public MqConfig Config { get; set; }

        /// <summary>
        ///  消息队列中定义的虚拟机
        /// </summary>
        public abstract string VHost { get; }

        /// <summary>
        ///  消息队列中定义的交换机
        /// </summary>
        public abstract string Exchange { get; }

        /// <summary>
        ///  定义的队列列表
        /// </summary>
        public List<QueueInfo> Queues { get; } = new List<QueueInfo>();
    }
}
