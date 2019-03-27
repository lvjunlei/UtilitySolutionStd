#region OperationResult 文件信息
/***********************************************************
**文 件 名：OperationResult 
**命名空间：Abs.IServices 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2017/12/19 11:18:02 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion


using System;

namespace Utility.Data
{
    /// <summary>
    /// 操作结果
    /// </summary>
    public class OperateResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 操作消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 附加信息
        /// </summary>
        public object Append { get; set; }

        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime SendTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="success">是否成功</param>
        /// <param name="message">操作消息</param>
        /// <param name="append">附加信息</param>
        public OperateResult(bool success, string message, object append = null)
        {
            IsSuccess = success;
            Message = message;
            Append = append;
            SendTime = DateTime.Now;
        }

        /// <summary>
        /// 获取操作成功的结果实例
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="append">附加信息</param>
        /// <returns></returns>
        public static OperateResult CreateSuccesResult(string message, object append = null)
        {
            return new OperateResult(true, message, append);
        }

        /// <summary>
        /// 获取操作失败的结果实例
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="append">附加信息</param>
        /// <returns></returns>
        public static OperateResult CreateFailureResult(string message, object append = null)
        {
            return new OperateResult(false, message, append);
        }
    }
}
