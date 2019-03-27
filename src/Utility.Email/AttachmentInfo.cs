#region MailHelper 文件信息
/***********************************************************
**文 件 名：MailHelper 
**命名空间：Utility.Email 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2019-02-20 15:09:20 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion

using System;
using System.IO;
using MimeKit;

namespace Utility.Email
{
    /// <summary>
    /// 附件信息
    /// </summary>
    public class AttachmentInfo : IDisposable
    {
        /// <summary>
        /// 附件类型，比如application/pdf
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 文件传输编码方式，默认ContentEncoding.Default
        /// </summary>
        public ContentEncoding ContentTransferEncoding { get; set; } = ContentEncoding.Default;

        /// <summary>
        /// 文件数组
        /// </summary>
        public byte[] Data { get; set; }

        /// <summary>
        /// 文件流
        /// </summary>
        private Stream _stream;

        /// <summary>
        /// 文件数据流，获取数据时优先采用此部分
        /// </summary>
        public Stream Stream
        {
            get
            {
                if (_stream == null && Data != null)
                {
                    _stream = new MemoryStream(Data);
                }
                return _stream;
            }
            set => _stream = value;
        }

        /// <summary>
        /// 释放Stream
        /// </summary>
        public void Dispose()
        {
            _stream?.Dispose();
        }
    }
}
