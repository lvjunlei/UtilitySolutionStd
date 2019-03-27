#region ZipHelper 文件信息
/***********************************************************
**文 件 名：ZipHelper 
**命名空间：Utility.Helpers 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2019-03-12 15:43:41 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion

using System.IO.Compression;

namespace Utility.Helpers
{
    /// <summary>
    /// ZipHelper
    /// </summary>
    public class ZipHelper
    {
        /// <summary>
        /// 打包 ZIP 文件
        /// </summary>
        /// <param name="sourcePath">要打包的文件路径</param>
        /// <param name="zipPath">打包后的 ZIP 文件路径</param>
        public static void Zip(string sourcePath, string zipPath)
        {
            ZipFile.CreateFromDirectory(sourcePath, zipPath);
        }

        /// <summary>
        /// 解压缩 ZIP 文件
        /// </summary>
        /// <param name="zipPath">要解压的 ZIP 文件路径</param>
        /// <param name="exactPath">解压后的 ZIP 文件路径</param>
        public static void UnZip(string zipPath, string exactPath)
        {
            ZipFile.ExtractToDirectory(zipPath, exactPath);
        }
    }
}
