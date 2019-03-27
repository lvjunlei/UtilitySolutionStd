using NLog;
using System;

namespace NLogTest
{
    /// <summary>
    /// LogHelper
    /// </summary>
    public static class LogHelper
    {
        private static readonly Logger log = LogManager.GetLogger("");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="exp"></param>
        public static void Error(object msg, Exception exp = null)
        {
            if (exp == null)
            {
                log.Error("#" + msg);
            }
            else
            {
                log.Error("#" + msg + "  " + exp.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="exp"></param>
        public static void Debug(object msg, Exception exp = null)
        {
            if (exp == null)
            {
                log.Debug("#" + msg);
            }
            else
            {
                log.Debug("#" + msg + "  " + exp.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="exp"></param>
        public static void Info(object msg, Exception exp = null)
        {
            if (exp == null)
            {
                log.Info("#" + msg);
            }
            else
            {
                log.Info("#" + msg + "  " + exp.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="exp"></param>
        public static void Warn(object msg, Exception exp = null)
        {
            if (exp == null)
            {
                log.Warn("#" + msg);
            }
            else
            {
                log.Warn("#" + msg + "  " + exp.ToString());
            }
        }

        /// <summary>
        /// 把日志信息写入数据库
        /// </summary>
        /// <param name="level">日志级别</param>
        /// <param name="appName">应用名称</param>
        /// <param name="moduleName">模块名称</param>
        /// <param name="account">用户账号</param>
        /// <param name="clientIp">客户端IP</param>
        /// <param name="parameter"></param>
        public static void Database(LogLevel level, string appName, string moduleName, string account, string clientIp, string parameter)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="message"></param>
        public static void ToRecord(this Exception ex, string message)
        {
            Error(message, ex);
        }
    }
}
