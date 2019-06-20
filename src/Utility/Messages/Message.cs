#region Message 文件信息
/***********************************************************
**文 件 名：Message 
**命名空间：Utility.Messages 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2019/6/17 16:33:03 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明：版权所有，盗版必究
************************************************************/
#endregion

using System;

namespace Utility.Messages
{
    /// <summary>
    /// 
    /// </summary>
    public class Message : IMessage
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid MessageId { get; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime MessageCreatedTime { get; }

        public Message()
        {
            MessageId = Guid.NewGuid();
            MessageCreatedTime = DateTime.Now;
        }
    }
}
