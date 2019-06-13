#region QueueInfo 文件信息
/***********************************************************
**文 件 名：QueueInfo 
**命名空间：Utility.Eventbus.RabbitMQ 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2019-03-31 19:38:00 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion


namespace Utility.Eventbus.RabbitMQ
{
    /// <summary>
    /// 
    /// </summary>
    public class QueueInfo
    {
        /// <summary>
        /// 持久化
        /// </summary>
        public bool Durable { get; set; }

        /// <summary>
        /// Exclusive
        /// </summary>
        public bool Exclusive { get; set; }

        /// <summary>
        /// 自动删除
        /// </summary>
        public bool AutoDelete { get; set; }
    }
}
