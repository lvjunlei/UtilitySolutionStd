using System;

namespace FileServer.Models
{
    /// <summary>
    /// 下载文件信息
    /// </summary>
    public class DownloadFileModel
    {
        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public long FileSize { get; set; }

        /// <summary>
        /// 文件数据
        /// </summary>
        public byte[] FileData { get; set; }

        /// <summary>
        /// 发送文件时间
        /// </summary>
        public DateTime SendTime { get; }

        /// <summary>
        /// 下载文件信息初始化
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="fileSize">文件大小</param>
        /// <param name="fileData">文件数据</param>
        public DownloadFileModel(string fileName,long fileSize,byte[] fileData)
        {
            FileName = fileName;
            FileSize = fileSize;
            FileData = fileData;
            SendTime = DateTime.Now;
        }
    }
}
