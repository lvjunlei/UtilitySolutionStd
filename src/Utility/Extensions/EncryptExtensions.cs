#region EncryptExtensions 文件信息
/***********************************************************
**文 件 名：EncryptExtensions 
**命名空间：Utility.Extensions 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2019-03-27 13:44:00 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion

using System;
using System.Security.Cryptography;
using System.Text;

namespace Utility.Extensions
{
    /// <summary>
    /// 加解密扩展方法
    /// </summary>
    public static class EncryptExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        private static byte[] Keys = { 0x41, 0x72, 0x65, 0x79, 0x6F, 0x75, 0x6D, 0x79, 0x53, 0x6E, 0x6F, 0x77, 0x6D, 0x61, 0x6E, 0x3F };

        #region Base64

        /// <summary> 
        /// 将字符串使用base64算法编码 
        /// </summary>
        /// <param name="encodingName">编码类型（编码名称）</param>
        /// <param name="inputStr">待加密的字符串</param>
        /// <returns>加密后的字符串</returns> 
        public static string EncryptBase64(this string inputStr, string encodingName = "UTF-8")
        {
            if (inputStr.IsNullOrEmpty())
                return null;

            var bytes = Encoding.GetEncoding(encodingName).GetBytes(inputStr);
            return Convert.ToBase64String(bytes);
        }

        /// <summary> 
        /// 将字符串使用base64算法解码
        /// </summary> 
        /// <param name="encodingName">编码类型</param> 
        /// <param name="base64String">已用base64算法加密的字符串</param> 
        /// <returns>解密后的字符串</returns> 
        public static string DecryptBase64(this string base64String, string encodingName = "UTF-8")
        {
            if (base64String.IsNullOrEmpty())
                return null;

            var bytes = Convert.FromBase64String(base64String);
            return Encoding.GetEncoding(encodingName).GetString(bytes);
        }

        #endregion

        #region Md5

        /// <summary>
        /// 将字符串使用MD5算法加密 16位  
        /// </summary>
        /// <param name="inputStr">要加密的字符串</param>
        /// <returns>密文</returns>
        public static string Encrypt16Md5(this string inputStr)
        {
            if (inputStr.IsNullOrEmpty())
                return inputStr;

            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.Default.GetBytes(inputStr));
                return BitConverter.ToString(result).Replace("-", "");
            }
        }

        /// <summary>
        /// 将字符串使用MD5算法加密 32位
        /// </summary>
        /// <param name="inputStr">要加密的字符串</param>
        /// <returns></returns>
        public static string Encrypt32Md5(this string inputStr)
        {
            if (inputStr.IsNullOrEmpty())
                return inputStr;

            using (var md5 = MD5.Create())
            {
                var sb = new StringBuilder();
                var bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(inputStr));
                for (var i = 0; i < bytes.Length; i++)
                {
                    sb.Append(bytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        /// <summary>
        /// 将字符串使用MD5算法加密 64位
        /// </summary>
        /// <param name="inputStr">要加密的字符串</param>
        /// <returns></returns>
        public static string Encrypt64Md5(this string inputStr)
        {
            if (inputStr.IsNullOrEmpty())
                return inputStr;

            using (var md5 = MD5.Create())
            {
                var bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(inputStr));
                return Convert.ToBase64String(bytes);
            }
        }

        #endregion

        #region  AES

        /// <summary>
        /// AES 加密
        /// </summary>
        /// <param name="encryptString">要加密的字符串</param>
        /// <param name="encryptKey">秘钥</param>
        /// <returns>密文</returns>
        public static string EncryptAes(this string encryptString, string encryptKey)
        {
            if (encryptString.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(encryptString));
            }

            if (encryptKey.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(encryptKey));
            }

            encryptKey = GetSubString(encryptKey, 0, 32, "");
            encryptKey = encryptKey.PadRight(32, ' ');

            RijndaelManaged rijndaelProvider = new RijndaelManaged();
            rijndaelProvider.Key = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 32));
            rijndaelProvider.IV = Keys;
            ICryptoTransform rijndaelEncrypt = rijndaelProvider.CreateEncryptor();

            byte[] inputData = Encoding.UTF8.GetBytes(encryptString);
            byte[] encryptedData = rijndaelEncrypt.TransformFinalBlock(inputData, 0, inputData.Length);

            return Convert.ToBase64String(encryptedData);
        }

        /// <summary>
        /// AES 解密
        /// </summary>
        /// <param name="decryptString">加密的字符串</param>
        /// <param name="decryptKey">密钥</param>
        /// <returns>明文</returns>
        public static string DecryptAes(this string decryptString, string decryptKey)
        {
            if (decryptString.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(decryptString));
            }

            if (decryptKey.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(decryptKey));
            }

            decryptKey = GetSubString(decryptKey, 0, 32, "");
            decryptKey = decryptKey.PadRight(32, ' ');

            RijndaelManaged rijndaelProvider = new RijndaelManaged();
            rijndaelProvider.Key = Encoding.UTF8.GetBytes(decryptKey);
            rijndaelProvider.IV = Keys;
            ICryptoTransform rijndaelDecrypt = rijndaelProvider.CreateDecryptor();

            byte[] inputData = Convert.FromBase64String(decryptString);
            byte[] decryptedData = rijndaelDecrypt.TransformFinalBlock(inputData, 0, inputData.Length);

            return Encoding.UTF8.GetString(decryptedData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_SrcString"></param>
        /// <param name="p_StartIndex"></param>
        /// <param name="p_Length"></param>
        /// <param name="p_TailString"></param>
        /// <returns></returns>
        private static string GetSubString(string p_SrcString, int p_StartIndex, int p_Length, string p_TailString)
        {
            string myResult = p_SrcString;

            byte[] bComments = Encoding.UTF8.GetBytes(p_SrcString);
            foreach (char c in Encoding.UTF8.GetChars(bComments))
            {    //当是日文或韩文时(注:中文的范围:\u4e00 - \u9fa5, 日文在\u0800 - \u4e00, 韩文为\xAC00-\xD7A3)
                if ((c > '\u0800' && c < '\u4e00') || (c > '\xAC00' && c < '\xD7A3'))
                {
                    //if (System.Text.RegularExpressions.Regex.IsMatch(p_SrcString, "[\u0800-\u4e00]+") || System.Text.RegularExpressions.Regex.IsMatch(p_SrcString, "[\xAC00-\xD7A3]+"))
                    //当截取的起始位置超出字段串长度时
                    if (p_StartIndex >= p_SrcString.Length)
                        return "";
                    else
                        return p_SrcString.Substring(p_StartIndex,
                                                       ((p_Length + p_StartIndex) > p_SrcString.Length) ? (p_SrcString.Length - p_StartIndex) : p_Length);
                }
            }

            if (p_Length >= 0)
            {
                byte[] bsSrcString = Encoding.Default.GetBytes(p_SrcString);

                //当字符串长度大于起始位置
                if (bsSrcString.Length > p_StartIndex)
                {
                    int p_EndIndex = bsSrcString.Length;

                    //当要截取的长度在字符串的有效长度范围内
                    if (bsSrcString.Length > (p_StartIndex + p_Length))
                    {
                        p_EndIndex = p_Length + p_StartIndex;
                    }
                    else
                    {   //当不在有效范围内时,只取到字符串的结尾

                        p_Length = bsSrcString.Length - p_StartIndex;
                        p_TailString = "";
                    }

                    int nRealLength = p_Length;
                    int[] anResultFlag = new int[p_Length];
                    byte[] bsResult = null;

                    int nFlag = 0;
                    for (int i = p_StartIndex; i < p_EndIndex; i++)
                    {
                        if (bsSrcString[i] > 127)
                        {
                            nFlag++;
                            if (nFlag == 3)
                                nFlag = 1;
                        }
                        else
                            nFlag = 0;

                        anResultFlag[i] = nFlag;
                    }

                    if ((bsSrcString[p_EndIndex - 1] > 127) && (anResultFlag[p_Length - 1] == 1))
                        nRealLength = p_Length + 1;

                    bsResult = new byte[nRealLength];

                    Array.Copy(bsSrcString, p_StartIndex, bsResult, 0, nRealLength);

                    myResult = Encoding.Default.GetString(bsResult);
                    myResult = myResult + p_TailString;
                }
            }

            return myResult;
        }

        #endregion

        #region DES

        /// <summary> 
        /// DES 加密数据 
        /// </summary> 
        /// <param name="encryptString">要加密的字符串</param> 
        /// <param name="encryptKey">加密密钥</param> 
        /// <returns>密文</returns> 
        public static string EncryptDes(this string encryptString, string encryptKey)
        {
            if (encryptString.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(encryptString));
            }

            if (encryptKey.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(encryptKey));
            }

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray;
            inputByteArray = Encoding.Default.GetBytes(encryptString);
            string md5SKey = Encrypt32Md5(encryptKey).Substring(0, 8);
            des.Key = Encoding.ASCII.GetBytes(md5SKey);
            des.IV = Encoding.ASCII.GetBytes(md5SKey);
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }

        /// <summary> 
        /// DES 解密数据 
        /// </summary> 
        /// <param name="decryptString">加密的字符串</param> 
        /// <param name="decryptKey">解密密钥</param> 
        /// <returns>明文</returns> 
        public static string DecryptDes(this string decryptString, string decryptKey)
        {
            if (decryptString.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(decryptString));
            }

            if (decryptKey.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(decryptKey));
            }

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            int len;
            len = decryptString.Length / 2;
            byte[] inputByteArray = new byte[len];
            int x, i;
            for (x = 0; x < len; x++)
            {
                i = Convert.ToInt32(decryptString.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
            string md5SKey = Encrypt32Md5(decryptKey).Substring(0, 8);
            des.Key = Encoding.ASCII.GetBytes(md5SKey);
            des.IV = Encoding.ASCII.GetBytes(md5SKey);
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.Default.GetString(ms.ToArray());
        }

        #endregion

        #region SHA

        /// <summary>
        /// 将字符串使用SHA1算法加密
        /// </summary>
        /// <param name="inputStr">明文</param>
        /// <returns></returns>
        public static string EncryptSha1(this string inputStr)
        {
            if (inputStr.IsNullOrEmpty())
                return null;

            using (var sha1 = SHA1.Create())
            {
                var buffer = Encoding.UTF8.GetBytes(inputStr);
                var byteArr = sha1.ComputeHash(buffer);
                return BitConverter.ToString(byteArr).Replace("-", "").ToLower();
            }
        }

        /// <summary>
        /// 将字符串使用SHA256算法加密
        /// </summary>
        /// <param name="inputStr">明文</param>
        /// <returns></returns>
        public static string EncryptSha256(this string inputStr)
        {
            if (inputStr.IsNullOrEmpty())
                return null;

            using (var sha256 = SHA256.Create())
            {
                var buffer = Encoding.UTF8.GetBytes(inputStr);
                var byteArr = sha256.ComputeHash(buffer);
                return BitConverter.ToString(byteArr).Replace("-", "").ToLower();
            }
        }

        /// <summary>
        /// 将字符串使用SHA384算法加密
        /// </summary>
        /// <param name="inputStr">明文</param>
        /// <returns></returns>
        public static string EncryptSha384(this string inputStr)
        {
            if (inputStr.IsNullOrEmpty())
                return null;

            using (var sha384 = SHA384.Create())
            {
                var buffer = Encoding.UTF8.GetBytes(inputStr);
                var byteArr = sha384.ComputeHash(buffer);
                return BitConverter.ToString(byteArr).Replace("-", "").ToLower();
            }
        }

        /// <summary>
        /// 将字符串使用SHA512算法加密
        /// </summary>
        /// <param name="inputStr">明文</param>
        /// <returns></returns>
        public static string EncryptSha512(this string inputStr)
        {
            if (inputStr.IsNullOrEmpty())
                return null;

            using (var sha512 = SHA512.Create())
            {
                var buffer = Encoding.UTF8.GetBytes(inputStr);
                var byteArr = sha512.ComputeHash(buffer);
                return BitConverter.ToString(byteArr).Replace("-", "").ToLower();
            }
        }

        #endregion
    }
}
