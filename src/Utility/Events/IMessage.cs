#region IMessage 文件信息
/***********************************************************
**文 件 名：IMessage 
**命名空间：Utility.Events 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2019-03-14 13:17:10 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion


namespace Utility.Events
{
    /// <summary>
    /// 消息事件
    /// </summary>
    public interface IMessage : IEvent
    {
        /// <summary>
        /// 消息主题
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 消息数据
        /// </summary>
        object Data { get; }

        /// <summary>
        /// 回掉名称
        /// </summary>
        string Callback { get; }
    }
}
