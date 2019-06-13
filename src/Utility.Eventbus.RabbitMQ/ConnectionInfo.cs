#region ConnectionInfo 文件信息
/***********************************************************
**文 件 名：ConnectionInfo 
**命名空间：Utility.Eventbus.RabbitMQ 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2019-03-31 19:39:32 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion

using RabbitMQ.Client;
using System;

namespace Utility.Eventbus.RabbitMQ
{
    /// <summary>
    /// ConnectionInfo
    /// </summary>
    public class ConnectionInfo
    {
        /// <summary>
        /// 连接信息
        /// </summary>
        public IConnection Connection { get; set; }

        /// <summary>
        /// 连接创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 队列名称
        /// </summary>
        public string QueueName { get; set; }

        /// <summary>
        /// 队列信息
        /// </summary>
        public QueueInfo QueueInfo { get; set; }
    }
}
