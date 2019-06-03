using System;

namespace Utility.Extensions
{
    /// <summary>
    /// DateTime 扩展方法
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// 计算机基准时间
        /// </summary>
        private static readonly DateTime dt1970 = new DateTime(1970, 1, 1);

        /// <summary>
        /// 获取指定时间自1970-01-01以来的Milliseconds
        /// </summary>
        /// <param name="dt">指定时间</param>
        /// <returns>Milliseconds</returns>
        public static double TotalMilliseconds(this DateTime dt)
        {
            return (dt - dt1970).TotalMilliseconds;
        }

        /// <summary>
        /// 获取时间戳标识的时间
        /// </summary>
        /// <param name="timeStamp">时间戳</param>
        /// <returns>时间</returns>
        public static DateTime ToDateTime(this double timeStamp)
        {
            return dt1970.AddMilliseconds(timeStamp);
        }
    }
}
