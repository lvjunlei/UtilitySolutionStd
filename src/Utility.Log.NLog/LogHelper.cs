using NLog;
using System;

namespace Utility.Logs
{
    /// <summary>
    /// LogHelper
    /// </summary>
    public static class LogHelper
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        #region Error

        /// <summary>
        /// 记录 error 日志信息
        /// </summary>
        /// <param name="msg">日志信息</param>
        public static void Error(string msg)
        {
            Log.Error($" # {msg}");
        }

        /// <summary>
        /// 记录 error 日志信息
        /// </summary>
        /// <param name="exp">异常信息</param>
        public static void Error(Exception exp)
        {
            Log.Error(exp);
        }

        /// <summary>
        /// 记录 error 日志信息
        /// </summary>
        /// <param name="msg">日志信息</param>
        /// <param name="exp">异常信息</param>
        public static void Error(string msg, Exception exp)
        {
            Log.Error(exp, $" # {msg}");
        }

        #endregion

        #region Debug

        /// <summary>
        /// 记录 debug 日志信息
        /// </summary>
        /// <param name="msg">日志信息</param>
        public static void Debug(object msg)
        {
            Log.Debug($" # {msg}");
        }

        /// <summary>
        /// 记录 debug 日志信息
        /// </summary>
        /// <param name="msg">日志信息</param>
        /// <param name="exp">异常信息</param>
        public static void Debug(object msg, Exception exp)
        {
            Log.Debug(exp, $" # {msg}");
        }

        #endregion

        #region Info

        /// <summary>
        /// 记录 info 日志信息
        /// </summary>
        /// <param name="msg">日志信息</param>
        public static void Info(string msg)
        {
            Log.Info($" # {msg}");
        }

        /// <summary>
        /// 记录 info 日志信息
        /// </summary>
        /// <param name="msg">日志信息</param>
        /// <param name="exp">异常信息</param>
        public static void Info(object msg, Exception exp)
        {
            Log.Info(exp, $" # {msg}");
        }

        #endregion

        #region warn

        /// <summary>
        /// 记录 warn 日志信息
        /// </summary>
        /// <param name="msg">日志信息</param>
        public static void Warn(string msg)
        {
            Log.Warn($" # {msg}");
        }

        /// <summary>
        /// 记录 warn 日志信息
        /// </summary>
        /// <param name="msg">日志信息</param>
        /// <param name="exp">异常信息</param>
        public static void Warn(string msg, Exception exp)
        {
            Log.Warn(exp, $" # {msg}");
        }

        #endregion

        #region Fatal

        /// <summary>
        /// 记录Fatal日志
        /// </summary>
        /// <param name="msg">日志内容</param>
        public static void Fatal(string msg)
        {
            Log.Fatal(msg);
        }

        /// <summary>
        /// 记录Fatal日志
        /// </summary>
        /// <param name="msg">日志内容</param>
        /// <param name="exp"></param>
        public static void Fatal(string msg, Exception exp)
        {
            Log.Fatal(exp, msg);
        }

        #endregion

        #region WriteDb

        /// <summary>
        /// 把日志信息写入数据库
        /// </summary>
        /// <param name="level">日志级别</param>
        /// <param name="appName">应用名称</param>
        /// <param name="moduleName">模块名称</param>
        /// <param name="account">用户账号</param>
        /// <param name="clientIp">客户端IP</param>
        /// <param name="parameter"></param>
        public static void WriteDb(LogLevel level, string appName, string moduleName, string account, string clientIp, string parameter)
        {

        }

        #endregion
    }
}
