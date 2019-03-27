#region Message 文件信息
/***********************************************************
**文 件 名：Message 
**命名空间：Utility.Events 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2019-03-14 13:23:20 
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
    public class MessageEvent : Event, IMessage
    {
        /// <summary>
        /// 消息名称
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 消息数据
        /// </summary>
        public object Data { get; }

        /// <summary>
        /// 回调名称
        /// </summary>
        public string Callback { get; }

        /// <summary>
        /// 初始化消息
        /// </summary>
        /// <param name="name">消息名称</param>
        /// <param name="data">消息数据</param>
        /// <param name="callback">回调名称</param>
        public MessageEvent(string name, object data, string callback = default(string))
        {
            Name = name;
            Data = data;
            Callback = callback;
        }
    }
}
