#region ILogAdapter 文件信息
/***********************************************************
**文 件 名：ILogAdapter 
**命名空间：Utility.Log 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2019-01-29 16:11:07 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion


using System;

namespace Utility.Logs
{
    /// <summary>
    /// ILogAdapter
    /// </summary>
    public interface ILogAdapter
    {
        /// <summary>
        /// Info 日志信息
        /// </summary>
        /// <param name="msg">日志消息</param>
        void Info(object msg, string account = null, string appName = null, string moduleName = null, object parameter = null);

        /// <summary>
        /// Debug 日志信息
        /// </summary>
        /// <param name="msg">日志消息</param>
        void Debug(object msg, string account = null, string appName = null, string moduleName = null, object parameter = null);

        /// <summary>
        /// Warn 日志信息
        /// </summary>
        /// <param name="msg">日志消息</param>
        void Warn(object msg, string account = null, string appName = null, string moduleName = null, object parameter = null);

        /// <summary>
        /// Error 日志信息
        /// </summary>
        /// <param name="msg">日志消息</param>
        /// <param name="ex">异常信息</param>
        void Error(object msg, Exception ex = null, string account = null, string appName = null, string moduleName = null, object parameter = null);

        /// <summary>
        /// Fatal 日志信息
        /// </summary>
        /// <param name="msg">日志消息</param>
        /// <param name="ex">异常信息</param>
        void Fatal(object msg, Exception ex = null, string account = null, string appName = null, string moduleName = null, object parameter = null);

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
        void Log(string level, object msg, Exception ex, string account = null, string appName = null, string moduleName = null, object parameter = null);
    }
}
