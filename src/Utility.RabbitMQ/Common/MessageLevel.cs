namespace Utility.RabbitMQ.Common
{
    /// <summary>
    /// 消息等级
    /// </summary>
    public enum MessageLevel
    {
        /// <summary>
        /// Trace
        /// </summary>
        Trace = 0,

        /// <summary>
        /// Debug
        /// </summary>
        Debug = 1,

        /// <summary>
        /// Information
        /// </summary>
        Information = 2,

        /// <summary>
        /// Warning
        /// </summary>
        Warning = 3,

        /// <summary>
        /// Error
        /// </summary>
        Error = 4,

        /// <summary>
        /// Critical
        /// </summary>
        Critical = 5,

        /// <summary>
        /// None
        /// </summary>
        None = 6
    }
}
