#region IMessage 文件信息
/***********************************************************
**文 件 名：IMessage 
**命名空间：Utility.Messages 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2019/6/17 16:31:00 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明：版权所有，盗版必究
************************************************************/
#endregion

using System;

namespace Utility.Messages
{
    /// <summary>
    /// IMessage
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        /// Message ID 
        /// </summary>
        Guid MessageId { get; }

        /// <summary>
        /// Message 创建时间
        /// </summary>
        DateTime MessageCreatedTime { get; }
    }
}
