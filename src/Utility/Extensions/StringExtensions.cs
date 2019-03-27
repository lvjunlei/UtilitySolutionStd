#region 文件信息和作者

/*************************************************************************************
      * CLR 版本:   4.0.30319.42000
      * 类 名 称:   StringExtensions
      * 机器名称:   LVJUNLEI-PC
      * 命名空间:   Utility.Extensions
      * 文 件 名:   StringExtensions
      * 创建时间:   16.5.5 10:51:32
      * 作   者:    LvJunlei

  * 创建年份:       2016
      * 修改时间:
      * 修 改 人:
*************************************************************************************
 * Copyright @ 凯亚开发部 2016. All rights reserved.
*************************************************************************************/
#endregion

using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace Utility.Extensions
{
    /// <summary>
    /// String常用扩展
    /// </summary>
    public static class StringExtensions
    {
        #region 常用扩展

        /// <summary>
        /// 测试字符串是否是 Null 或者 Empty
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// 测试字符串是否是 Null 或者 WhiteSpace
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// 如果字符串为NULL 则返回string.Empty，否则，返回原字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string NullToEmpty(this string str)
        {
            return str ?? string.Empty;
        }

        #endregion
        
        #region Format

        /// <summary>
        /// 格式化字符串
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string FormatWith(this string format, params object[] args)
        {
            return string.Format(format, args);
        }

        /// <summary>
        /// 反转字符串
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string Reverse(this string s)
        {
            var input = s.ToCharArray();
            var output = new char[input.Length];
            for (var i = 0; i < input.Length; i++)
            {
                output[input.Length - 1 - i] = input[i];
            }

            return output.ToString();
        }

        /// <summary>
        /// 将指定字符串中的格式项替换为指定数组中相应对象的字符串表示形式。
        /// </summary>
        /// <param name="s">复合格式字符串。</param>
        /// <param name="objs">一个对象数组，其中包含零个或多个要设置格式的对象。</param>
        /// <returns></returns>
        public static string Format(this string s, params object[] objs)
        {
            return string.Format(s, objs);
        }

        #endregion

        #region Regex

        /// <summary>
        /// 字符串是否匹配
        /// </summary>
        /// <param name="s"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static bool IsMatch(this string s, string pattern)
        {
            if (string.IsNullOrEmpty(s))
            {
                return false;
            }

            return Regex.IsMatch(s, pattern);
        }

        /// <summary>
        /// 字符串是否匹配
        /// </summary>
        /// <param name="s"></param>
        /// <param name="pattern"></param>
        /// <param name="regexOptions"></param>
        /// <returns></returns>
        public static bool IsMatch(this string s, string pattern, RegexOptions regexOptions)
        {
            if (string.IsNullOrEmpty(s))
            {
                return false;
            }

            return Regex.IsMatch(s, "", regexOptions);
        }

        /// <summary>
        /// 获取匹配的字符串
        /// </summary>
        /// <param name="s"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static string Match(this string s, string pattern)
        {
            if (string.IsNullOrEmpty(s))
            {
                return "";
            }
            return Regex.Match(s, pattern).Value;
        }

        /// <summary>
        /// 快速验证一个字符串是否符合指定的正则表达式。
        /// </summary>
        /// <param name="value">需验证的字符串。</param>
        /// <param name="express">正则表达式的内容。</param>
        /// <returns>是否合法的bool值。</returns>
        public static bool QuickValidate(this string value, string express)
        {
            var myRegex = new Regex(express);
            return value.Length != 0 && myRegex.IsMatch(value);
        }

        /// <summary>
        /// 普通的域名
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsCommonDomain(this string value)
        {
            return value.ToLower().QuickValidate("^(www.)?(\\w+\\.){1,3}(org|org.cn|gov.cn|com|cn|net|cc)$");
        }

        /// <summary>
        /// 检查一个字符串是否是纯数字构成的，一般用于查询字符串参数的有效性验证。
        /// </summary>
        /// <param name="value">需验证的字符串。</param>
        /// <returns>是否合法的bool值。</returns>
        public static bool IsNumeric(this string value)
        {
            return value.QuickValidate("^[-]?[1-9]*[0-9]*$");
        }

        /// <summary>
        /// 检查一个字符串是否是纯字母和数字构成的，一般用于查询字符串参数的有效性验证。
        /// </summary>
        /// <param name="value">需验证的字符串。</param>
        /// <returns>是否合法的bool值。</returns>
        public static bool IsLetterOrNumber(this string value)
        {
            return value.QuickValidate("^[a-zA-Z0-9_]*$");
        }

        /// <summary>
        /// 判断是否是数字，包括小数和整数。
        /// </summary>
        /// <param name="value">需验证的字符串。</param>
        /// <returns>是否合法的bool值。</returns>
        public static bool IsNumber(this string value)
        {
            return value.QuickValidate("^(0|([1-9]+[0-9]*))(.[0-9]+)?$");
        }

        /// <summary>
        /// 判断一个字符串是否为邮编
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsZipCode(this string value)
        {
            return value.QuickValidate("^([0-9]{6})$");
        }

        /// <summary>
        /// 判断一个字符串是否为邮件
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsEmail(this string value)
        {
            var regex = new Regex(@"^\w+([-+.]\w+)*@(\w+([-.]\w+)*\.)+([a-zA-Z]+)+$", RegexOptions.IgnoreCase);
            return regex.Match(value).Success;
        }

        /// <summary>
        /// 是否邮箱
        /// </summary>
        /// <param name="value">邮箱地址</param>
        /// <param name="isRestrict">是否按严格模式验证</param>
        /// <returns></returns>
        public static bool IsEmail(this string value, bool isRestrict)
        {
            if (value.IsNullOrEmpty())
            {
                return false;
            }
            var pattern = isRestrict
                ? @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$"
                : @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$";

            return value.IsMatch(pattern, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 是否存在邮箱
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="isRestrict">是否按严格模式验证</param>
        /// <returns></returns>
        public static bool HasEmail(this string value, bool isRestrict = false)
        {
            if (value.IsNullOrEmpty())
            {
                return false;
            }
            var pattern = isRestrict
                ? @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$"
                : @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$";
            return value.IsMatch(pattern, RegexOptions.IgnoreCase);
        }

        #region IsPhoneNumber(是否合法的手机号码)
        /// <summary>
        /// 是否合法的手机号码
        /// </summary>
        /// <param name="value">手机号码</param>
        /// <returns></returns>
        public static bool IsPhoneNumber(this string value)
        {
            if (value.IsNullOrEmpty())
            {
                return false;
            }
            return value.IsMatch(@"^(0|86|17951)?(13[0-9]|15[012356789]|18[0-9]|14[57]|17[678])[0-9]{8}$");
        }
        #endregion

        #region IsMobileNumber(是否手机号码)
        /// <summary>
        /// 是否手机号码
        /// </summary>
        /// <param name="value">手机号码</param>
        /// <param name="isRestrict">是否按严格模式验证</param>
        /// <returns></returns>
        public static bool IsMobileNumberSimple(this string value, bool isRestrict = false)
        {
            if (value.IsNullOrEmpty())
            {
                return false;
            }
            var pattern = isRestrict ? @"^[1][3-8]\d{9}$" : @"^[1]\d{10}$";
            return value.IsMatch(pattern);
        }
        /// <summary>
        /// 是否手机号码
        /// </summary>
        /// <param name="value">手机号码</param>
        /// <returns></returns>
        public static bool IsMobileNumber(string value)
        {
            if (value.IsNullOrEmpty())
            {
                return false;
            }
            value = value.Trim().Replace("^", "").Replace("$", "");
            /**
             * 手机号码: 
             * 13[0-9], 14[5,7], 15[0, 1, 2, 3, 5, 6, 7, 8, 9], 17[6, 7, 8], 18[0-9], 170[0-9]
             * 移动号段: 134,135,136,137,138,139,150,151,152,157,158,159,182,183,184,187,188,147,178,1705
             * 联通号段: 130,131,132,155,156,185,186,145,176,1709
             * 电信号段: 133,153,180,181,189,177,1700
             */
            return value.IsMatch(@"^1(3[0-9]|4[57]|5[0-35-9]|8[0-9]|70)\d{8}$");
        }

        /// <summary>
        /// 是否存在手机号码
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="isRestrict">是否按严格模式验证</param>
        /// <returns></returns>
        public static bool HasMobileNumberSimple(string value, bool isRestrict = false)
        {
            if (value.IsNullOrEmpty())
            {
                return false;
            }
            var pattern = isRestrict ? @"[1][3-8]\d{9}" : @"[1]\d{10}";
            return value.IsMatch(pattern);
        }
        #endregion

        #region IsChinaMobilePhone(是否中国移动号码)
        /// <summary>
        /// 是否中国移动号码
        /// </summary>
        /// <param name="value">手机号码</param>
        /// <returns></returns>
        public static bool IsChinaMobilePhone(this string value)
        {
            if (value.IsNullOrEmpty())
            {
                return false;
            }
            /**
             * 中国移动：China Mobile
             * 134,135,136,137,138,139,150,151,152,157,158,159,182,183,184,187,188,147,178,1705
             */
            return value.IsMatch(@"(^1(3[4-9]|4[7]|5[0-27-9]|7[8]|8[2-478])\d{8}$)|(^1705\d{7}$)");
        }
        #endregion

        #region IsChinaUnicomPhone(是否中国联通号码)
        /// <summary>
        /// 是否中国联通号码
        /// </summary>
        /// <param name="value">手机号码</param>
        /// <returns></returns>
        public static bool IsChinaUnicomPhone(this string value)
        {
            if (value.IsNullOrEmpty())
            {
                return false;
            }
            /**
             * 中国联通：China Unicom
             * 130,131,132,155,156,185,186,145,176,1709
             */
            return value.IsMatch(@"(^1(3[0-2]|4[5]|5[56]|7[6]|8[56])\d{8}$)|(^1709\d{7}$)");
        }
        #endregion

        #region IsChinaTelecomPhone(是否中国电信号码)
        /// <summary>
        /// 是否中国电信号码
        /// </summary>
        /// <param name="value">手机号码</param>
        /// <returns></returns>
        public static bool IsChinaTelecomPhone(string value)
        {
            if (value.IsNullOrEmpty())
            {
                return false;
            }
            /**
             * 中国电信：China Telecom
             * 133,153,180,181,189,177,1700
             */
            return value.IsMatch(@"(^1(33|53|77|8[019])\d{8}$)|(^1700\d{7}$)");
        }
        #endregion

        /// <summary>
        /// 判断一个字符串是否为ID格式
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsIdCard(this string value)
        {
            if (value.IsNullOrEmpty() || value.Length < 15)
            {
                return false;
            }
            Regex regex;
            string[] strArray;
            if ((value.Length != 15) && (value.Length != 0x12))
            {
                return false;
            }
            if (value.Length == 15)
            {
                regex = new Regex(@"^(\d{6})(\d{2})(\d{2})(\d{2})(\d{3})$");
                if (!regex.Match(value).Success)
                {
                    return false;
                }
                strArray = regex.Split(value);
                try
                {
                    DateTime dt;
                    return DateTime.TryParse("20" + strArray[2] + "-" + strArray[3] + "-" + strArray[4], out dt);
                }
                catch
                {
                    return false;
                }
            }
            regex = new Regex(@"^(\d{6})(\d{4})(\d{2})(\d{2})(\d{3})([0-9Xx])$");
            if (!regex.Match(value).Success)
            {
                return false;
            }
            strArray = regex.Split(value);
            try
            {
                DateTime dt;
                return DateTime.TryParse(strArray[2] + "-" + strArray[3] + "-" + strArray[4], out dt);
            }
            catch
            {
                return false;
            }
        }

        #region IsBase64String(是否Base64编码)
        /// <summary>
        /// 是否Base64编码
        /// </summary>
        /// <param name="value">Base64字符串</param>
        /// <returns></returns>
        public static bool IsBase64String(this string value)
        {
            return value.IsMatch(@"[A-Za-z0-9\+\/\=]");
        }
        #endregion

        #region IsDate(是否日期)
        /// <summary>
        /// 是否日期
        /// </summary>
        /// <param name="value">日期字符串</param>
        /// <param name="isRegex">是否正则验证</param>
        /// <returns></returns>
        public static bool IsDate(this string value, bool isRegex = false)
        {
            if (value.IsNullOrEmpty())
            {
                return false;
            }
            if (isRegex)
            {
                //考虑到4年一度的366天，还有特殊的2月的日期
                return
                    value.IsMatch(
                        @"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-)) (20|21|22|23|[0-1]?\d):[0-5]?\d:[0-5]?\d$");
            }
            DateTime minValue;
            return DateTime.TryParse(value, out minValue);
        }

        /// <summary>
        /// 是否日期
        /// </summary>
        /// <param name="value">日期字符串</param>
        /// <param name="format">格式化字符串</param>
        /// <returns></returns>
        public static bool IsDate(this string value, string format)
        {
            return IsDate(value, format, null, System.Globalization.DateTimeStyles.None);
        }

        /// <summary>
        /// 是否日期
        /// </summary>
        /// <param name="value">日期字符串</param>
        /// <param name="format">格式化字符串</param>
        /// <param name="provider">格式化提供者</param>
        /// <param name="styles">日期格式</param>
        /// <returns></returns>
        public static bool IsDate(this string value, string format, IFormatProvider provider, System.Globalization.DateTimeStyles styles)
        {
            if (value.IsNullOrEmpty())
            {
                return false;
            }
            return DateTime.TryParseExact(value, format, provider, styles, out DateTime _);
        }
        #endregion

        #region IsDateTime(是否有效时间)
        /// <summary>
        /// 是否大于最小时间
        /// </summary>
        /// <param name="value">时间</param>
        /// <param name="min">最小时间</param>
        /// <returns></returns>
        public static bool IsDateTimeMin(this string value, DateTime min)
        {
            if (value.IsNullOrEmpty())
            {
                return false;
            }
            DateTime dateTime;
            if (DateTime.TryParse(value, out dateTime))
            {
                if (DateTime.Compare(dateTime, min) >= 0)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 是否小于最大时间
        /// </summary>
        /// <param name="value">时间</param>
        /// <param name="max">最大时间</param>
        /// <returns></returns>
        public static bool IsDateTimeMax(this string value, DateTime max)
        {
            if (value.IsNullOrEmpty())
            {
                return false;
            }
            DateTime dateTime;
            if (DateTime.TryParse(value, out dateTime))
            {
                if (DateTime.Compare(max, dateTime) >= 0)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        /// <summary>
        /// 判断是不是纯中文
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsChinese(this string value)
        {
            var regex = new Regex(@"^[\u4E00-\u9FA5\uF900-\uFA2D]+$", RegexOptions.IgnoreCase);
            return regex.Match(value).Success;
        }

        /// <summary>
        /// 判断一个字符串是否为手机号码
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsPhone(this string value)
        {
            var regex = new Regex(@"^(13|15)\d{9}$", RegexOptions.IgnoreCase);
            return regex.Match(value).Success;
        }

        /// <summary>
        /// 判断一个字符串是否为电话号码
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsTelphone(this string value)
        {
            var regex = new Regex(@"^(86)?(-)?(0\d{2,3})?(-)?(\d{7,8})(-)?(\d{3,5})?$", RegexOptions.IgnoreCase);
            return regex.Match(value).Success;
        }

        /// <summary>
        /// 判断一个字符串是否为网址
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsUrl(this string value)
        {
            var regex = new Regex(@"(http://)?([\w-]+\.)*[\w-]+(/[\w- ./?%&=]*)?", RegexOptions.IgnoreCase);
            return regex.Match(value).Success;
        }

        /// <summary>
        /// 判断一个字符串是否为IP地址
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsIp(this string value)
        {
            var regex = new Regex(@"^(((2[0-4]{1}[0-9]{1})|(25[0-5]{1}))|(1[0-9]{2})|([1-9]{1}[0-9]{1})|([0-9]{1})).(((2[0-4]{1}[0-9]{1})|(25[0-5]{1}))|(1[0-9]{2})|([1-9]{1}[0-9]{1})|([0-9]{1})).(((2[0-4]{1}[0-9]{1})|(25[0-5]{1}))|(1[0-9]{2})|([1-9]{1}[0-9]{1})|([0-9]{1})).(((2[0-4]{1}[0-9]{1})|(25[0-5]{1}))|(1[0-9]{2})|([1-9]{1}[0-9]{1})|([0-9]{1}))$", RegexOptions.IgnoreCase);
            return regex.Match(value).Success;
        }

        /// <summary>
        /// 判断一个字符串是否为字母加数字
        /// Regex("[a-zA-Z0-9]?"
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsWordAndNum(this string value)
        {
            var regex = new Regex("[a-zA-Z0-9]?");
            return regex.Match(value).Success;
        }

        /// <summary>
        /// 格式化输出XML字符串
        /// </summary>
        /// <param name="xml">未格式化XML字符串</param>
        /// <returns></returns>
        public static string FormatXml(this string xml)
        {
            var doc = new XmlDocument();
            doc.LoadXml(xml);

            using (var sw = new StringWriter())
            {
                using (var writer = new XmlTextWriter(sw))
                {
                    writer.Indentation = 2;
                    writer.Formatting = System.Xml.Formatting.Indented;
                    doc.WriteContentTo(writer);
                    writer.Close();
                }

                return sw.ToString();
            }
        }

        #endregion

        #region Convert

        /// <summary>
        /// 判断字符串是否是Int
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsInt(this string s)
        {
            int i;
            return int.TryParse(s, out i);
        }

        /// <summary>
        /// 把字符串转换为整形
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int ToInt(this string s)
        {
            return int.Parse(s);
        }

        /// <summary>
        /// 是否是DateTime时间
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsDateTime(this string s)
        {
            DateTime t;
            return DateTime.TryParse(s, out t);
        }

        /// <summary>
        /// 转换为DateTime
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string s)
        {
            return DateTime.Parse(s);
        }

        /// <summary>
        /// 转换指定格式的时间
        /// </summary>
        /// <param name="s"></param>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string s, string format, IFormatProvider provider)
        {
            return DateTime.ParseExact(s, format, provider);
        }

        /// <summary>
        /// 首字母小写
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToCamel(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return s;
            }

            return s[0].ToString().ToLower() + s.Substring(1);
        }

        /// <summary>
        /// 首字母大写
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToPascal(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return s;
            }

            return s[0].ToString().ToUpper() + s.Substring(1);
        }

        /// <summary>
        /// 把string转换为Byte数组
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns>Byte数组</returns>
        public static byte[] ToByteArray(this string s)
        {
            return Encoding.ASCII.GetBytes(s);
        }

        /// <summary>
        /// 把string转换为Byte数组
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns>Byte数组</returns>
        public static byte[] ToUtf8ByteArray(this string s)
        {
            return Encoding.UTF8.GetBytes(s);
        }

        /// <summary>
        /// 把byte数组转换为string
        /// </summary>
        /// <param name="data">byte数组</param>
        /// <returns>string</returns>
        public static string ToString(this byte[] data)
        {
            return Encoding.ASCII.GetString(data);
        }

        #endregion

        #region 隐藏敏感信息

        /// <summary>
        /// 隐藏敏感信息
        /// </summary>
        /// <param name="info">信息实体</param>
        /// <param name="left">左边保留的字符数</param>
        /// <param name="right">右边保留的字符数</param>
        /// <param name="basedOnLeft">当长度异常时，是否显示左边 
        /// <code>true</code>显示左边，<code>false</code>显示右边
        /// </param>
        /// <returns></returns>
        public static string HideSensitiveInfo(this string info, int left, int right, bool basedOnLeft = true)
        {
            if (string.IsNullOrEmpty(info))
            {
                throw new ArgumentNullException(info);
            }
            var sbText = new StringBuilder();
            var hiddenCharCount = info.Length - left - right;
            if (hiddenCharCount > 0)
            {
                string prefix = info.Substring(0, left), suffix = info.Substring(info.Length - right);
                sbText.Append(prefix);
                for (var i = 0; i < hiddenCharCount; i++)
                {
                    sbText.Append("*");
                }
                sbText.Append(suffix);
            }
            else
            {
                if (basedOnLeft)
                {
                    if (info.Length > left && left > 0)
                    {
                        sbText.Append(info.Substring(0, left) + "****");
                    }
                    else
                    {
                        sbText.Append(info.Substring(0, 1) + "****");
                    }
                }
                else
                {
                    if (info.Length > right && right > 0)
                    {
                        sbText.Append("****" + info.Substring(info.Length - right));
                    }
                    else
                    {
                        sbText.Append("****" + info.Substring(info.Length - 1));
                    }
                }
            }
            return sbText.ToString();
        }

        /// <summary>
        /// 隐藏敏感信息
        /// </summary>
        /// <param name="info">信息实体</param>
        /// <param name="left">左边保留的字符数</param>
        /// <param name="right">右边保留的字符数</param>
        /// <param name="basedOnLeft">当长度异常时，是否显示左边</param> 
        /// <code>true</code>显示左边，<code>false</code>显示右边
        /// <returns></returns>
        public static string HideSensitiveInfo1(this string info, int left, int right, bool basedOnLeft = true)
        {
            if (string.IsNullOrEmpty(info))
            {
                throw new ArgumentNullException(info);
            }
            var sbText = new StringBuilder();
            var hiddenCharCount = info.Length - left - right;
            if (hiddenCharCount > 0)
            {
                string prefix = info.Substring(0, left), suffix = info.Substring(info.Length - right);
                sbText.Append(prefix);
                sbText.Append("****");
                sbText.Append(suffix);
            }
            else
            {
                if (basedOnLeft)
                {
                    if (info.Length > left && left > 0)
                    {
                        sbText.Append(info.Substring(0, left) + "****");
                    }
                    else
                    {
                        sbText.Append(info.Substring(0, 1) + "****");
                    }
                }
                else
                {
                    if (info.Length > right && right > 0)
                    {
                        sbText.Append("****" + info.Substring(info.Length - right));
                    }
                    else
                    {
                        sbText.Append("****" + info.Substring(info.Length - 1));
                    }
                }
            }
            return sbText.ToString();
        }

        /// <summary>
        /// 隐藏敏感信息
        /// </summary>
        /// <param name="info">信息</param>
        /// <param name="sublen">信息总长与左子串（或右子串）的比例</param>
        /// <param name="basedOnLeft"/>当长度异常时，是否显示左边，默认true，默认显示左边
        /// <code>true</code>显示左边，<code>false</code>显示右边
        /// <returns></returns>
        public static string HideSensitiveInfo(this string info, int sublen = 3, bool basedOnLeft = true)
        {
            if (string.IsNullOrEmpty(info))
            {
                throw new ArgumentNullException(info);
            }
            if (sublen <= 1)
            {
                sublen = 3;
            }
            var subLength = info.Length / sublen;
            if (subLength > 0 && info.Length > (subLength * 2))
            {
                string prefix = info.Substring(0, subLength), suffix = info.Substring(info.Length - subLength);
                return prefix + "****" + suffix;
            }
            if (basedOnLeft)
            {
                var prefix = subLength > 0 ? info.Substring(0, subLength) : info.Substring(0, 1);
                return prefix + "****";
            }
            var suffixs = subLength > 0 ? info.Substring(info.Length - subLength) : info.Substring(info.Length - 1);
            return "****" + suffixs;
        }

        /// <summary>
        /// 隐藏右键详情
        /// </summary>
        /// <param name="email">邮件地址</param>
        /// <param name="left">邮件头保留字符个数，默认值设置为3</param>
        /// <returns></returns>
        public static string HideEmailDetails(this string email, int left = 3)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException(email);
            }
            if (!Regex.IsMatch(email, @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"))
                return HideSensitiveInfo(email);
            var suffixLen = email.Length - email.LastIndexOf('@');
            return HideSensitiveInfo(email, left, suffixLen, false);
        }

        #endregion

        #region 打开文件或网址

        /// <summary>
        /// 打开文件或网址
        /// </summary>
        /// <param name="s"></param>
        public static void Open(this string s)
        {
            if (s == "")
            {
                return;
            }
            Process.Start(s);
        }

        #endregion

        #region 执行DOS命令

        /// <summary>
        /// 执行DOS命令
        /// </summary>
        /// <param name="cmd">命令</param>
        /// <param name="error">执行命令产生的错误</param>
        /// <returns></returns>
        public static string ExecuteDos(this string cmd, out string error)
        {
            var process = new Process
            {
                StartInfo =
                {
                    FileName = "cmd.exe",
                    UseShellExecute = false,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                }
            };
            process.Start();

            process.StandardInput.WriteLine(cmd);
            process.StandardInput.WriteLine("exit");
            error = process.StandardError.ReadToEnd();
            return process.StandardOutput.ReadToEnd();
        }

        #endregion
    }
}
