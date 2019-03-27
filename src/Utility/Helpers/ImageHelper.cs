#region ImageHelper 文件信息
/***********************************************************
**文 件 名：ImageHelper 
**命名空间：Utility.Helpers 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2019-03-12 15:47:21 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion

using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Utility.Helpers
{
    /// <summary>
    /// ImageHelper
    /// </summary>
    public class ImageHelper
    {
        /// <summary>
        /// 图片转字节数组
        /// </summary>
        /// <param name="path">图片路径</param>
        /// <returns>byte array result</returns>
        public static Byte[] ImageToByteArray(string path)
        {
            using (var fs = File.OpenRead(path))
            {
                int filelength = 0;
                filelength = (int)fs.Length;
                Byte[] imagebyte = new Byte[filelength];
                fs.Read(imagebyte, 0, filelength);
                fs.Close();
                return imagebyte;
            }
        }

        /// <summary>
        /// 图片文件 转 base64字符串
        /// </summary>
        /// <param name="path">图片路径</param>
        /// <returns>base64 string result</returns>
        public static string ImageToBase64(string path)
        {
            var byteArr = ImageToByteArray(path);
            return Convert.ToBase64String(byteArr);
        }

        /// <summary>
        /// base64字符串 转 图片
        /// </summary>
        /// <param name="base64">base64 字符串</param>
        /// <param name="path">store path</param>
        public static void Base64ToImage(string base64, string path)
        {
            string filepath = Path.GetDirectoryName(path);
            if (!Directory.Exists(filepath))
            {
                if (filepath != null) Directory.CreateDirectory(filepath);
            }
            var match = Regex.Match(base64, "data:image/png;base64,([\\w\\W]*)$");
            if (match.Success)
            {
                base64 = match.Groups[1].Value;
            }
            var photoBytes = Convert.FromBase64String(base64);
            File.WriteAllBytes(path, photoBytes);
        }

        /// <summary>
        /// 检查图片是否是 jpg
        /// </summary>
        /// <param name="imageBase64">图片 base64 字符串</param>
        /// <returns>true is jpg  false not jpg</returns>
        public static bool IsJPG(string imageBase64)
        {
            var img = Convert.FromBase64String(imageBase64);
            var jpgStr = $"{img[0].ToString()}{img[1].ToString()}";
            return jpgStr.Equals($"{(int)ImageFormat.JPG}");
        }

        /// <summary>
        /// 检查图片是否是 png
        /// </summary>
        /// <param name="imageBase64">图片 base64 字符串</param>
        /// <returns>true is png  false not png</returns>
        public static bool IsPNG(string imageBase64)
        {
            var img = Convert.FromBase64String(imageBase64);
            var jpgStr = $"{img[0].ToString()}{img[1].ToString()}";
            return jpgStr.Equals($"{(int)ImageFormat.PNG}");
        }

        /// <summary>
        /// 检查图片是否是 GIF
        /// </summary>
        /// <param name="imageBase64">图片 base64 字符串</param>
        /// <returns>true is gif  false not gif</returns>
        public static bool IsGIF(string imageBase64)
        {
            var img = Convert.FromBase64String(imageBase64);
            var jpgStr = $"{img[0].ToString()}{img[1].ToString()}";
            return jpgStr.Equals($"{(int)ImageFormat.GIF}");
        }

        private enum ImageFormat
        {
            JPG = 255216,
            GIF = 7173,
            PNG = 13780,
        }
    }
}
