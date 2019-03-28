#region LogService 文件信息
/***********************************************************
**文 件 名：LogService 
**命名空间：Utility.Logs 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2019-03-27 17:24:35 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion

using NLog;
using System;

namespace Utility.Logs
{
    /// <summary>
    /// LogService
    /// </summary>
    public class LogService : ILogService
    {
        #region 私有成员

        private readonly Logger Log = LogManager.GetCurrentClassLogger();

        #endregion

        /// <summary>
        /// 记录 Debug 日志
        /// </summary>
        /// <param name="msg">日志内容</param>
        public void Debug(string msg)
        {
            Log.Debug($" | {msg}");
        }

        /// <summary>
        /// 记录 Debug 日志
        /// </summary>
        /// <param name="msg">日志内容</param>
        /// <param name="ex">异常信息</param>
        public void Debug(string msg, Exception ex)
        {
            Log.Debug(ex, $" | {msg}");
        }

        /// <summary>
        /// 记录 Error 日志
        /// </summary>
        /// <param name="msg">日志内容</param>
        public void Error(string msg)
        {
            Log.Error($" | {msg}");
        }

        /// <summary>
        /// 记录 Error 日志
        /// </summary>
        /// <param name="ex">异常信息</param>
        public void Error(Exception ex)
        {
            Log.Error(ex);
        }

        /// <summary>
        /// 记录 Error 日志
        /// </summary>
        /// <param name="msg">日志信息</param>
        /// <param name="ex">异常信息</param>
        public void Error(string msg, Exception ex)
        {
            Log.Error(ex, $" | {msg}");
        }

        /// <summary>
        /// 记录 Fatal 日志
        /// </summary>
        /// <param name="msg">日志信息</param>
        public void Fatal(string msg)
        {
            Log.Fatal($" | {msg}");
        }

        /// <summary>
        /// 记录 Fatal 日志
        /// </summary>
        /// <param name="msg">日志信息</param>
        /// <param name="ex">异常信息</param>
        public void Fatal(string msg, Exception ex)
        {
            Log.Fatal(ex, $" | {msg}");
        }

        /// <summary>
        /// 记录 Info 日志
        /// </summary>
        /// <param name="msg">日志信息</param>
        public void Info(string msg)
        {
            Log.Info($" | {msg}");
        }

        /// <summary>
        /// 记录 Info 日志
        /// </summary>
        /// <param name="msg">日志信息</param>
        /// <param name="ex">异常信息</param>
        public void Info(string msg, Exception ex)
        {
            Log.Info(ex, $" | {msg}");
        }

        /// <summary>
        /// 记录 Warn 日志
        /// </summary>
        /// <param name="msg">日志信息</param>
        public void Warn(string msg)
        {
            Log.Warn($" | {msg}");
        }

        /// <summary>
        /// 记录 Warn 日志
        /// </summary>
        /// <param name="msg">日志信息</param>
        /// <param name="ex">异常信息</param>
        public void Warn(string msg, Exception ex)
        {
            Log.Warn(ex, $" | {msg}");
        }

        /// <summary>
        /// 把日志信息写入数据库
        /// </summary>
        /// <param name="log">日志信息</param>
        public void WriteDb(Log log)
        {
            throw new NotImplementedException();
        }
    }
}
