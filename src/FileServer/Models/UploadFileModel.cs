using System.ComponentModel.DataAnnotations;

namespace FileServer.Models
{
    /// <summary>
    /// 上传文件休息
    /// </summary>
    public class UploadFileModel
    {
        /// <summary>
        /// 文件名称
        /// </summary>
        [Required]
        //[RegularExpression(@"/\.[^\.]+$/")]
        [StringLength(150, MinimumLength = 3)]
        public string FileName { get; set; }

        /// <summary>
        /// 是否需要重新命名保存（防止文件重名）
        /// </summary>
        public bool IsAutoRename { get; set; }

        /// <summary>
        /// 当文件重名时可否覆盖
        /// </summary>
        public bool CanCover { get; set; }

        /// <summary>
        /// 文件存储路径（相对）
        /// </summary>
        public string SavePath { get; set; }

        /// <summary>
        /// 文件数据
        /// </summary>
        public byte[] FileData { get; set; }

        /// <summary>
        /// 获取文件大小
        /// </summary>
        /// <returns></returns>
        public long GetFileSize()
        {
            if (FileData == null)
            {
                return 0;
            }
            return FileData.Length;
        }
    }
}
