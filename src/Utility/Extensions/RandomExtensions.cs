﻿#region RandomExtensions 文件信息
/***********************************************************
**文 件 名：RandomExtensions 
**命名空间：Utility.Extensions 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2016-12-07 21:49:06 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion

using System;

namespace Utility.Extensions
{
    /// <summary>
    /// 随机数Random扩展
    /// </summary>
    public static class RandomExtensions
    {
        /// <summary>
        /// 随机布尔值
        /// </summary>
        /// <param name="random"></param>
        /// <returns>随机布尔值</returns>
        public static bool NextBoolean(this Random random)
        {
            if (random == null)
            {
                throw new ArgumentNullException(nameof(random));
            }
            return random.NextDouble() > 0.5;
        }

        /// <summary>
        /// 指定枚举类型的随机枚举值
        /// </summary>
        /// <param name="random"></param>
        /// <returns>指定枚举类型的随机枚举值</returns>
        public static T NextEnum<T>(this Random random) where T : struct
        {
            var type = typeof(T);
            if (!type.IsEnum)
            {
                throw new InvalidOperationException();
            }
            var array = Enum.GetValues(type);
            var index = random.Next(array.GetLowerBound(0), array.GetUpperBound(0) + 1);
            return (T)array.GetValue(index);
        }

        /// <summary>
        /// 随机数填充的指定长度的数组
        /// </summary>
        /// <param name="random"></param>
        /// <param name="length">数组长度</param>
        /// <returns>随机数填充的指定长度的数组</returns>
        public static byte[] NextBytes(this Random random, int length)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }
            var data = new byte[length];
            random.NextBytes(data);
            return data;
        }

        /// <summary>
        /// 数组中的随机元素
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="random"></param>
        /// <param name="items">元素数组</param>
        /// <returns>元素数组中的某个随机项</returns>
        public static T NextItem<T>(this Random random, T[] items)
        {
            return items[random.Next(0, items.Length)];
        }

        /// <summary>
        /// 指定时间段内的随机时间值
        /// </summary>
        /// <param name="random"></param>
        /// <param name="minValue">时间范围的最小值</param>
        /// <param name="maxValue">时间范围的最大值</param>
        /// <returns>指定时间段内的随机时间值</returns>
        public static DateTime NextDateTime(this Random random, DateTime minValue, DateTime maxValue)
        {
            var ticks = minValue.Ticks + (long)((maxValue.Ticks - minValue.Ticks) * random.NextDouble());
            return new DateTime(ticks);
        }

        /// <summary>
        /// 随机时间值
        /// </summary>
        /// <param name="random"></param>
        /// <returns>随机时间值</returns>
        public static DateTime NextDateTime(this Random random)
        {
            return NextDateTime(random, DateTime.MinValue, DateTime.MaxValue);
        }

        /// <summary>
        /// 获取指定的长度的随机数字字符串
        /// </summary>
        /// <param name="random"></param>
        /// <param name="length">要获取随机数长度</param>
        /// <returns>指定长度的随机数字符串</returns>
        public static string GetRandomNumberString(this Random random, int length)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }
            char[] pattern = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            var result = "";
            var n = pattern.Length;
            for (var i = 0; i < length; i++)
            {
                var rnd = random.Next(0, n);
                result += pattern[rnd];
            }
            return result;
        }

        /// <summary>
        /// 获取指定的长度的随机字母字符串
        /// </summary>
        /// <param name="random"></param>
        /// <param name="length">要获取随机数长度</param>
        /// <returns>指定长度的随机字母组成字符串</returns>
        public static string GetRandomLetterString(this Random random, int length)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }
            char[] pattern =
            {
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L',
                'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
            };
            var result = "";
            var n = pattern.Length;
            for (var i = 0; i < length; i++)
            {
                var rnd = random.Next(0, n);
                result += pattern[rnd];
            }
            return result;
        }

        /// <summary>
        /// 获取指定的长度的随机字母和数字字符串
        /// </summary>
        /// <param name="random"></param>
        /// <param name="length">要获取随机数长度</param>
        /// <returns>指定长度的随机字母和数字组成字符串</returns>
        public static string GetRandomLetterAndNumberString(this Random random, int length)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }
            char[] pattern =
            {
                '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P',
                'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
            };
            var result = "";
            var n = pattern.Length;
            for (var i = 0; i < length; i++)
            {
                var rnd = random.Next(0, n);
                result += pattern[rnd];
            }
            return result;
        }
    }
}
