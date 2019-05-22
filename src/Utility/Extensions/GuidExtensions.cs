using System;

namespace Utility.Extensions
{
    /// <summary>
    /// GuidExtensions
    /// </summary>
    public static class GuidExtensions
    {
        /// <summary>
        /// 转换为Oracle数据库格式的 Guid 主键
        /// </summary>
        /// <param name="guid">C# Guid类型</param>
        /// <returns>Oracle数据库格式的 Guid 主键</returns>
        public static string ToOracle(this Guid guid)
        {
            return BitConverter.ToString(guid.ToByteArray()).Replace("-", "");
        }

        /// <summary>
        /// 转换为Guid格式
        /// </summary>
        /// <param name="guid">guid字符串</param>
        /// <returns>Guid格式</returns>
        public static Guid ToGuid(this string guid)
        {
            if (string.IsNullOrEmpty(guid))
            {
                throw new ArgumentNullException(nameof(guid));
            }
            var bytes = new byte[guid.Length / 2];
            for (var i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(guid.Substring(i * 2, 2), 16);
            }
            return new Guid(bytes);
        }
    }
}
