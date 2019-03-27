﻿#region LogAdapter 文件信息
/***********************************************************
**文 件 名：LogAdapter 
**命名空间：Utility.Log 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2019-01-29 16:10:43 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion


using NLog;
using System;
using System.Linq;

namespace Utility.Logs
{
    /// <summary>
    /// LogAdapter
    /// </summary>
    public class LogAdapter : ILogAdapter
    {
        private readonly Logger logger = LogManager.GetLogger("");

        /// <summary>
        /// Info 日志信息
        /// </summary>
        /// <param name="msg">日志消息</param>
        public void Info(object msg, string account = null, string appName = null, string moduleName = null, object parameter = null)
        {
            Log("Info", msg, null, account, appName, moduleName, parameter);
        }

        /// <summary>
        /// Debug 日志信息
        /// </summary>
        /// <param name="msg">日志消息</param>
        public void Debug(object msg, string account = null, string appName = null, string moduleName = null, object parameter = null)
        {
            Log("Debug", msg, null, account, appName, moduleName, parameter);
        }

        /// <summary>
        /// Warn 日志信息
        /// </summary>
        /// <param name="msg">日志消息</param>
        public void Warn(object msg, string account = null, string appName = null, string moduleName = null, object parameter = null)
        {
            Log("Warn", msg, null, account, appName, moduleName, parameter);
        }

        /// <summary>
        /// Error 日志信息
        /// </summary>
        /// <param name="msg">日志消息</param>
        /// <param name="ex">异常信息</param>
        public void Error(object msg, Exception ex = null, string account = null, string appName = null, string moduleName = null, object parameter = null)
        {
            Log("Error", msg, ex, account, appName, moduleName, parameter);
        }

        /// <summary>
        /// Fatal 日志信息
        /// </summary>
        /// <param name="msg">日志消息</param>
        /// <param name="ex">异常信息</param>
        public void Fatal(object msg, Exception ex = null, string account = null, string appName = null, string moduleName = null, object parameter = null)
        {
            Log("Fatal", msg, ex, account, appName, moduleName, parameter);
        }

        /// <summary>
        /// 自定义格式日志
        /// </summary>
        /// <param name="level">日志级别</param>
        /// <param name="msg">日志消息</param>
        /// <param name="ex">异常信息</param>
        /// <param name="account">用户账号</param>
        /// <param name="appName">应用名称</param>
        /// <param name="moduleName">模块名称</param>
        /// <param name="parameter">参数名称</param>
        public void Log(string level, object msg, Exception ex, string account = null, string appName = null, string moduleName = null, object parameter = null)
        {
            LogLevel logLevel;
            switch (level)
            {
                case "Info":
                    logLevel = LogLevel.Info;
                    break;

                case "Debug":
                    logLevel = LogLevel.Debug;
                    break;

                case "Warn":
                    logLevel = LogLevel.Warn;
                    break;

                case "Error":
                    logLevel = LogLevel.Error;
                    break;

                case "Fatal":
                    logLevel = LogLevel.Fatal;
                    break;

                default:
                    logLevel = LogLevel.Info;
                    break;
            }
            var ei = new LogEventInfo(logLevel, "", msg?.ToString());
            ei.Properties["account"] = account;
            ei.Properties["appName"] = appName;
            ei.Properties["moduleName"] = moduleName;
            ei.Properties["parameter"] = parameter;
            logger.Log(ei);
        }

        public string GetIp()
        {
            return System.Net.NetworkInformation.NetworkInterface
                .GetAllNetworkInterfaces()
                .Select(p => p.GetIPProperties())
                .SelectMany(p => p.UnicastAddresses)
                .Where(p => p.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork
                && !System.Net.IPAddress.IsLoopback(p.Address))
                .FirstOrDefault()
                ?.Address
                .ToString();
        }
    }
}
