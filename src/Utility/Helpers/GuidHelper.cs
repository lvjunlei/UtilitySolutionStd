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
using System.Threading;

namespace Utility.Helpers
{
    /// <summary>
    /// 可生成连续GUID的帮助类
    /// </summary>
    public class GuidHelper
    {
        /// <summary>
        /// 生成有序的 GUID
        /// </summary>
        /// <param name="databaseType">数据库类型</param>
        /// <returns></returns>
        public static Guid NewGuid(SequentialGuidDatabaseType databaseType = SequentialGuidDatabaseType.SqlServer)
        {
            return SequentialGuidGenerator.Instance.Create(databaseType);
        }

        #region UuidCreateSequential

        /// <summary>
        /// 生成有序的 GUID
        /// </summary>
        /// <returns></returns>
        public static Guid CreateSequentialGuid()
        {
            const int rpcSOk = 0;
            var result = UuidCreateSequential(out Guid guid);

            if (result != rpcSOk)
            {
                throw new System.ComponentModel.Win32Exception(System.Runtime.InteropServices.Marshal.GetLastWin32Error());
            }
            else
            {
                var guidBytes = guid.ToByteArray();
                Array.Reverse(guidBytes, 0, 4);
                Array.Reverse(guidBytes, 4, 2);
                Array.Reverse(guidBytes, 6, 2);

                var timestamp = DateTime.UtcNow.Ticks / 10000L;
                var timestampBytes = BitConverter.GetBytes(timestamp);

                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(timestampBytes);
                }
                Buffer.BlockCopy(timestampBytes, 2, guidBytes, 10, 6);
                return new Guid(guidBytes);
            }

        }

        [System.Runtime.InteropServices.DllImport("rpcrt4.dll", SetLastError = true)]
        private static extern int UuidCreateSequential(out Guid guid);

        #endregion

        #region EntityFramework Core

        private static long _counter = DateTime.UtcNow.Ticks;

        /// <summary>
        /// 获取下一个有序 GUID
        /// 推荐使用
        /// </summary>
        /// <returns>有序 GUID</returns>
        public static Guid Next()
        {
            var guidBytes = Guid.NewGuid().ToByteArray();
            var counterBytes = BitConverter.GetBytes(Interlocked.Increment(ref _counter));

            if (!BitConverter.IsLittleEndian)
            {
                Array.Reverse(counterBytes);
            }

            guidBytes[08] = counterBytes[1];
            guidBytes[09] = counterBytes[0];
            guidBytes[10] = counterBytes[7];
            guidBytes[11] = counterBytes[6];
            guidBytes[12] = counterBytes[5];
            guidBytes[13] = counterBytes[4];
            guidBytes[14] = counterBytes[3];
            guidBytes[15] = counterBytes[2];

            return new Guid(guidBytes);
        }

        #endregion
    }
}
