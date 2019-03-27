#region Mp3FileMerge 文件信息
/***********************************************************
**文 件 名：Mp3FileMerge 
**命名空间：Utility.Audio 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2018/2/26 14:53:48 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion

using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Utility.Audio
{
    /// <summary>
    /// 拼接 MP3 格式文件
    /// </summary>
    public class Mp3FileMerge
    {
        /// <summary>
        /// 拼接 MP3 格式音频文件
        /// </summary>
        /// <param name="outputFile">要合成的文件名称</param>
        /// <param name="sourceFiles">源文件名称集合</param>
        public static void Merge(string outputFile, IEnumerable<string> sourceFiles)
        {
            using (var sw = new StreamWriter(outputFile, false, Encoding.GetEncoding(1252)))
            {
                var bys = new List<byte>();
                foreach (var file in sourceFiles)
                {
                    var length = new FileInfo(file).Length;
                    var bytes = new byte[length];
                    using (var fs = new FileStream(file, FileMode.Open))
                    {
                        fs.Read(bytes, 0, (int)length);
                        bys.AddRange(bytes);
                    }
                }
                sw.Write(Encoding.GetEncoding(1252).GetString(bys.ToArray()));
            }
        }
    }
}
