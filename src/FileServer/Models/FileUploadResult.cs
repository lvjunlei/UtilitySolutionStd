using System;

namespace FileServer.Models
{
    /// <summary>
    /// 文件上传结果信息
    /// </summary>
    public class FileUploadResult
    {
        /// <summary>
        /// 文件原始名称
        /// </summary>
        public string OriginalName { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; }

        /// <summary>
        /// 可访问的文件路径
        /// </summary>
        public string VisitUrl { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public long FileSize { get; }

        /// <summary>
        /// 文件保存时间
        /// </summary>
        public DateTime SaveTime { get; }

        /// <summary>
        /// 初始化文件上传结果信息
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="originalName">文件原始名称</param>
        /// <param name="fileSize">文件大小</param>
        public FileUploadResult(string fileName, string originalName, long fileSize)
        {
            FileName = fileName;
            FileSize = fileSize;
            OriginalName = originalName;
            SaveTime = DateTime.Now;
        }
    }
}