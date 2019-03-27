#region ByteExtensions 文件信息
/***********************************************************
**文 件 名：ByteExtensions 
**命名空间：Utility.Extensions 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2016-12-05 15:19:51 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Utility.Extensions
{
    /// <summary>
    /// ByteExtensions
    /// </summary>
    public static class ByteExtensions
    {
        /// <summary>
        /// 获取byte数组的UTF-8字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ToUtf8String(this byte[] bytes)
        {
            return System.Text.Encoding.UTF8.GetString(bytes);
        }

        /// <summary>
        /// 将对象序列化为byte[]
        /// 使用IFormatter的Serialize序列化
        /// </summary>
        /// <param name="obj">需要序列化的对象</param>
        /// <returns>序列化获取的二进制流</returns>
        public static byte[] SerializeToBytes(this object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
            byte[] buff;
            using (var ms = new MemoryStream())
            {
                IFormatter iFormatter = new BinaryFormatter();
                iFormatter.Serialize(ms, obj);
                buff = ms.GetBuffer();
                ms.Close();
            }
            return buff;
        }


        /// <summary>
        /// 将对象转为二进制文件，并保存到指定的文件中
        /// </summary>
        /// <param name="obj">待存的对象</param>
        /// <param name="name">文件路径</param>
        /// <returns></returns>
        public static bool SaveBinaryFile(this object obj, string name)
        {
            using (var flstr = new FileStream(name, FileMode.Create))
            {
                using (var binaryWriter = new BinaryWriter(flstr))
                {
                    var buff = SerializeToBytes(obj);
                    binaryWriter.Write(buff);
                    binaryWriter.Close();
                }
                flstr.Close();
            }
            return true;
        }

        /// <summary>
        /// 将byte[]反序列化为对象
        /// 使用IFormatter的Deserialize发序列化
        /// </summary>
        /// <param name="bytes">传入的byte[]</param>
        /// <returns></returns>
        public static object DeserializeToObject(this byte[] bytes)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }
            using (var ms = new MemoryStream())
            {
                IFormatter iFormatter = new BinaryFormatter();
                var obj = iFormatter.Deserialize(ms);
                ms.Close();
                return obj;
            }
        }


        /// <summary>
        /// 将对象序列化为byte[]
        /// 使用Marshal的StructureToPtr序列化
        /// </summary>
        /// <param name="obj">需序列化的对象</param>
        /// <returns>序列化后的byte[]</returns>
        public static byte[] MarshalObjectToBytes(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
            var buff = new byte[Marshal.SizeOf(obj)];
            var ptr = Marshal.UnsafeAddrOfPinnedArrayElement(buff, 0);
            Marshal.StructureToPtr(obj, ptr, true);
            return buff;
        }

        /// <summary>
        /// 将byte[]序列化为对象
        /// </summary>
        /// <param name="bytes">被转换的二进制流</param>
        /// <param name="type">转换成的类名</param>
        /// <returns></returns>
        public static object MarshalBytesToObject(this byte[] bytes, Type type)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            var ptr = Marshal.UnsafeAddrOfPinnedArrayElement(bytes, 0);
            return Marshal.PtrToStructure(ptr, type);
        }
        
        /// <summary>
        /// 将byte[]转换为文件并保存到指定的地址
        /// </summary>
        /// <param name="bytes">需反序列化的byte[]</param>
        /// <param name="savePath">文件保存的路径</param>
        /// <returns>是否成功</returns>
        public static bool SaveToFile(this byte[] bytes, string savePath)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }
            if (savePath == null)
            {
                throw new ArgumentNullException(nameof(savePath));
            }
            if (File.Exists(savePath))
            {
                var fileName = savePath.Substring(savePath.LastIndexOf("\\", StringComparison.Ordinal) + 1);
                throw new ArgumentNullException($"文件 {fileName} 已存在");
            }
            using (var fs = new FileStream(savePath, FileMode.CreateNew))
            {
                var bw = new BinaryWriter(fs);
                bw.Write(bytes, 0, bytes.Length);
                bw.Close();
                fs.Close();
            }
            return true;
        }

        /// <summary>
        /// 将文件序列化为二进制流
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>序列化后的二进制流</returns>
        public static byte[] ReadToBytes(this string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException(filePath);
            }

            byte[] byteData;
            using (var file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                byteData = new byte[file.Length];
                file.Read(byteData, 0, byteData.Length);
                file.Close();
            }
            return byteData;
        }
    }
}
