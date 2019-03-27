using System.Collections.Generic;

namespace Utility.RabbitMQ
{
    /// <summary>
    /// 契约接口 IService，此接口包含创建通道、开启/停止订阅，
    /// 一个服务可能承载多个通道，所以还需要包含通道列表
    /// </summary>
    public interface IMqService
    {
        /// <summary>
        ///  创建通道
        /// </summary>
        /// <param name="queue">队列名称</param>
        /// <param name="routeKey">路由名称</param>
        /// <param name="exchangeType">交换机类型</param>
        /// <returns></returns>
        MqChannel CreateChannel(string queue, string routeKey, string exchangeType);

        /// <summary>
        ///  开启订阅
        /// </summary>
        void Start();

        /// <summary>
        ///  停止订阅
        /// </summary>
        void Stop();

        /// <summary>
        ///  通道列表
        /// </summary>
        List<MqChannel> Channels { get; set; }

        /// <summary>
        ///  消息队列中定义的虚拟机
        /// </summary>
        string VHost { get; }

        /// <summary>
        ///  消息队列中定义的交换机
        /// </summary>
        string Exchange { get; }
    }
}
