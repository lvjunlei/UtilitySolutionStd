#region WaveFileMerge 文件信息
/***********************************************************
**文 件 名：WaveFileMerge 
**命名空间：Utility.Audio 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2018/2/26 14:50:21 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion

using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Utility.Audio
{
    /// <summary>
    /// Wave文件拼接
    /// </summary>
    public class WaveFileMerge
    {
        private int _length;
        private short _channels;
        private int _samplerate;
        private int _dataLength;
        private short _bitsPerSample;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spath"></param>
        private void WaveHeaderSourceFile(string spath)
        {
            using (var fs = new FileStream(spath, FileMode.Open, FileAccess.Read))
            {
                using (var br = new BinaryReader(fs))
                {
                    _length = (int)fs.Length - 8;
                    fs.Position = 22;
                    _channels = br.ReadInt16();
                    fs.Position = 24;
                    _samplerate = br.ReadInt32();
                    fs.Position = 34;

                    _bitsPerSample = br.ReadInt16();
                    _dataLength = (int)fs.Length - 44;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sPath"></param>
        private void WaveHeaderOutputFile(string sPath)
        {
            using (var fs = new FileStream(sPath, FileMode.Create, FileAccess.Write))
            {
                using (var bw = new BinaryWriter(fs))
                {
                    fs.Position = 0;
                    bw.Write(new[] { 'R', 'I', 'F', 'F' });

                    bw.Write(_length);

                    bw.Write(new[] { 'W', 'A', 'V', 'E', 'f', 'm', 't', ' ' });

                    bw.Write(16);

                    bw.Write((short)1);
                    bw.Write(_channels);

                    bw.Write(_samplerate);

                    bw.Write(_samplerate * (_bitsPerSample * _channels / 8));

                    bw.Write((short)(_bitsPerSample * _channels / 8));

                    bw.Write(_bitsPerSample);

                    bw.Write(new[] { 'd', 'a', 't', 'a' });
                    bw.Write(_dataLength);
                }
            }
        }

        /// <summary>
        /// 合成Wave格式音频文件
        /// </summary>
        /// <param name="outputFile">要合成的文件名称</param>
        /// <param name="sourceFiles">源文件名称集合</param>
        public static void Merge(string outputFile, IEnumerable<string> sourceFiles)
        {
            var waIn = new WaveFileMerge();
            var waOut = new WaveFileMerge
            {
                _dataLength = 0,
                _length = 0
            };

            //聚合文件头
            var enumerable = sourceFiles as string[] ?? sourceFiles.ToArray();
            foreach (var path in enumerable)
            {
                if (!File.Exists(path))
                {
                    continue;
                }
                waIn.WaveHeaderSourceFile(path);
                waOut._dataLength += waIn._dataLength;
                waOut._length += waIn._length;
            }

            //重构文件头
            waOut._bitsPerSample = waIn._bitsPerSample;
            waOut._channels = waIn._channels;
            waOut._samplerate = waIn._samplerate;
            waOut.WaveHeaderOutputFile(outputFile);

            foreach (var path in enumerable)
            {
                if (!File.Exists(path))
                {
                    continue;
                }
                using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    var arrfile = new byte[fs.Length - 44];
                    fs.Position = 44;
                    fs.Read(arrfile, 0, arrfile.Length);

                    using (var fo = new FileStream(outputFile, FileMode.Append, FileAccess.Write))
                    {
                        using (var bw = new BinaryWriter(fo))
                        {
                            bw.Write(arrfile);
                        }
                    }
                }
            }
        }
    }
}
