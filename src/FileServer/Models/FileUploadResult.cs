using System;

namespace FileServer.Models
{
    /// <summary>
    /// �ļ��ϴ������Ϣ
    /// </summary>
    public class FileUploadResult
    {
        /// <summary>
        /// �ļ�ԭʼ����
        /// </summary>
        public string OriginalName { get; set; }

        /// <summary>
        /// �ļ�����
        /// </summary>
        public string FileName { get; }

        /// <summary>
        /// �ɷ��ʵ��ļ�·��
        /// </summary>
        public string VisitUrl { get; set; }

        /// <summary>
        /// ��Ϣ
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// �ļ���С
        /// </summary>
        public long FileSize { get; }

        /// <summary>
        /// �ļ�����ʱ��
        /// </summary>
        public DateTime SaveTime { get; }

        /// <summary>
        /// ��ʼ���ļ��ϴ������Ϣ
        /// </summary>
        /// <param name="fileName">�ļ�����</param>
        /// <param name="originalName">�ļ�ԭʼ����</param>
        /// <param name="fileSize">�ļ���С</param>
        public FileUploadResult(string fileName, string originalName, long fileSize)
        {
            FileName = fileName;
            FileSize = fileSize;
            OriginalName = originalName;
            SaveTime = DateTime.Now;
        }
    }
}