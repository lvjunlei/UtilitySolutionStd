#region AudioMerge 文件信息
/***********************************************************
**文 件 名：AudioMerge 
**命名空间：Utility.Audio 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2018/2/26 14:49:52 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion

using System.Collections.Generic;

namespace Utility.Audio
{
    /// <summary>
    /// 音频文件拼接
    /// </summary>
    public class AudioMerge
    {
        /// <summary>
        /// 拼接 Wave 格式音频文件
        /// </summary>
        /// <param name="outputFile">要合成的文件名称</param>
        /// <param name="sourceFiles">源文件名称集合</param>
        public static void MergeWave(string outputFile, IEnumerable<string> sourceFiles)
        {
            WaveFileMerge.Merge(outputFile, sourceFiles);
        }

        /// <summary>
        /// 拼接 Mp3 格式音频文件
        /// </summary>
        /// <param name="outputFile">要合成的文件名称</param>
        /// <param name="sourceFiles">源文件名称集合</param>
        public static void MergeMp3(string outputFile, IEnumerable<string> sourceFiles)
        {
            Mp3FileMerge.Merge(outputFile, sourceFiles);
        }
    }
}
