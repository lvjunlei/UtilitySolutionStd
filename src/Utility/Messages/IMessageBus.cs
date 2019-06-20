#region IMessageBus 文件信息
/***********************************************************
**文 件 名：IMessageBus 
**命名空间：Utility.Messages 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2019/6/17 16:34:49 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明：版权所有，盗版必究
************************************************************/
#endregion

using System.Threading.Tasks;

namespace Utility.Messages
{
    /// <summary>
    /// IMessageBus
    /// </summary>
    public interface IMessageBus
    {
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <typeparam name="TMesage">Message类型</typeparam>
        /// <param name="msg">消息</param>
        Task PublishAsync<TMesage>(TMesage msg) where TMesage : IMessage;
    }
}
