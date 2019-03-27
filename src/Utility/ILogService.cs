#region ILogger 文件信息
/***********************************************************
**文 件 名：ILogger 
**命名空间：Utility.Logs 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2017/11/20 9:02:56 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion

using System;

namespace Utility
{
    /// <summary>
    /// 日志接口服务
    /// </summary>
    public interface ILogService
    {
        #region 公共方法

        /// <summary>
        /// 写入数据库
        /// </summary>
        /// <param name="log"></param>
        void WriteDb(Log log);

        /// <summary>
        /// 记录Info日志
        /// </summary>
        /// <param name="msg">日志内容</param>
        void Info(string msg);

        /// <summary>
        /// 记录Info日志
        /// </summary>
        /// <param name="msg">日志内容</param>
        /// <param name="ex">异常信息</param>
        void Info(string msg, Exception ex);

        /// <summary>
        /// 记录InfoWarn日志
        /// </summary>
        /// <param name="msg">日志内容</param>
        void Warn(string msg);

        /// <summary>
        /// 记录InfoWarn日志
        /// </summary>
        /// <param name="msg">日志内容</param>
        /// <param name="ex">异常信息</param>
        void Warn(string msg, Exception ex);

        /// <summary>
        /// 记录Debug日志
        /// </summary>
        /// <param name="msg">日志内容</param>
        void Debug(string msg);

        /// <summary>
        /// 记录Debug日志
        /// </summary>
        /// <param name="msg">日志内容</param>
        /// <param name="ex"></param>
        void Debug(string msg, Exception ex);

        /// <summary>
        /// 记录Error日志
        /// </summary>
        /// <param name="msg">日志内容</param>
        void Error(string msg);

        /// <summary>
        /// 记录Error日志
        /// </summary>
        /// <param name="ex">异常信息</param>
        void Error(Exception ex);

        /// <summary>
        /// 记录Error日志
        /// </summary>
        /// <param name="msg">日志内容</param>
        /// <param name="ex">异常信息</param>
        void Error(string msg, Exception ex);

        /// <summary>
        /// 记录Fatal日志
        /// </summary>
        /// <param name="msg">日志内容</param>
        void Fatal(string msg);

        /// <summary>
        /// 记录Fatal日志
        /// </summary>
        /// <param name="msg">日志内容</param>
        /// <param name="ex"></param>
        void Fatal(string msg, Exception ex);

        #endregion
    }

    /// <summary>
    /// 日志信息
    /// </summary>
    public class Log
    {
        /// <summary>
        /// 用户账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 方法名称
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// 方法描述
        /// </summary>
        public string ActionInfo { get; set; }

        /// <summary>
        /// 调用的参数
        /// </summary>
        public string ActionParameter { get; set; }

        /// <summary>
        /// 方法 执行结果
        /// </summary>
        public string ActionResult { get; set; }

        /// <summary>
        /// 设备IP
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// 设备MAC
        /// </summary>
        public string Mac { get; set; }

        /// <summary>
        /// 调用时间
        /// </summary>
        public DateTime LogTime { get; set; }

        public Log()
        {
            LogTime = DateTime.Now;
        }
    }
}
