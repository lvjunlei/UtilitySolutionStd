#region GuidHelper 文件信息
/***********************************************************
**文 件 名：GuidHelper 
**命名空间：Utility.Helpers 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2017/11/20 10:52:10 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion
using System;
using System.Diagnostics;

namespace Utility.Helpers
{
    /// <summary>
    /// StopwatchHelper
    /// </summary>
    public static class StopwatchHelper
    {
        /// <summary>
        /// 计算执行时间
        /// </summary>
        /// <param name="action">方法</param>
        /// <returns></returns>
        public static TimeSpan Caculate(Action action)
        {
            var sw = new Stopwatch();
            sw.Start();
            action();
            sw.Stop();
            return sw.Elapsed;
        }

        /// <summary>
        /// 计算方法指定执行次数的执行时间
        /// </summary>
        /// <param name="executeTime">执行次数</param>
        /// <param name="action">方法</param>
        /// <returns></returns>
        public static TimeSpan Caculate(int executeTimes, Action action)
        {
            var sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < executeTimes; i++)
            {
                action();
            }
            sw.Stop();
            return sw.Elapsed;
        }
    }
}
