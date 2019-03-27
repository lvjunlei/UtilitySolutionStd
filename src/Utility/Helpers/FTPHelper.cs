#region FTPHelper 文件信息
/***********************************************************
**文 件 名：FTPHelper 
**命名空间：Utility.Helpers 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2017/11/20 10:52:10 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion

using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Utility.Helpers
{
    /// <summary>
    /// FtpHelper
    /// </summary>
    public class FtpHelper
    {
        /// <summary>
        /// ftp upload file
        /// </summary>
        /// <param name="filePath">file path</param>
        /// <param name="ftpUrl">ftp address</param>
        /// <param name="ftpUser">ftp user</param>
        /// <param name="ftpPwd">ftp password</param>
        public static void Upload(string filePath, string ftpUrl, string ftpUser, string ftpPwd)
        {
            FtpWebRequest request;
            request = WebRequest.Create(new Uri(ftpUrl)) as FtpWebRequest;
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.UseBinary = true;
            request.UsePassive = true;
            request.KeepAlive = true;
            request.Credentials = new NetworkCredential(ftpUser, ftpPwd);
            using (var inputStream = File.OpenRead(filePath))
            using (var outputStream = request.GetRequestStream())
            {
                var buffer = new byte[10240];
                int readBytesCount;
                while ((readBytesCount = inputStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    outputStream.Write(buffer, 0, readBytesCount);
                }
            }
        }
        /// <summary>
        /// ftp upload file async
        /// </summary>
        /// <param name="filePath">file path</param>
        /// <param name="ftpUrl">ftp url</param>
        /// <param name="ftpUser">ftp user</param>
        /// <param name="ftpPwd">ftp password</param>
        public static async Task UploadAsync(string filePath, string ftpUrl, string ftpUser, string ftpPwd)
        {
            FtpWebRequest request;
            request = WebRequest.Create(new Uri(ftpUrl)) as FtpWebRequest;
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.UseBinary = true;
            request.UsePassive = true;
            request.KeepAlive = true;
            request.Credentials = new NetworkCredential(ftpUser, ftpPwd);
            var inputStream = File.OpenRead(filePath);
            var outputStream = await request.GetRequestStreamAsync();
            var buffer = new byte[10240];
            int readBytesCount;
            while ((readBytesCount = await inputStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                await outputStream.WriteAsync(buffer, 0, readBytesCount);
            }
        }

        /// <summary>
        /// ftp download file
        /// </summary>
        /// <param name="filePath">file path</param>
        /// <param name="ftpUrl">ftp url</param>
        /// <param name="ftpUser">ftp user</param>
        /// <param name="ftpPwd">ftp password</param>
        public static void Download(string filePath, string ftpUrl, string ftpUser, string ftpPwd)
        {
            FtpWebRequest request;
            request = WebRequest.Create(new Uri(ftpUrl)) as FtpWebRequest;
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.UseBinary = true;
            request.UsePassive = true;
            request.KeepAlive = true;
            request.Credentials = new NetworkCredential(ftpUser, ftpPwd);
            using (Stream ftpStream = request.GetResponse().GetResponseStream())
            using (Stream fileStream = File.Create(filePath))
            {
                byte[] buffer = new byte[10240];
                int read;
                while ((read = ftpStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    fileStream.Write(buffer, 0, read);
                }
            }
        }


        /// <summary>
        /// ftp download file async
        /// </summary>
        /// <param name="filePath">file path</param>
        /// <param name="ftpUrl">ftp url</param>
        /// <param name="ftpUser">ftp user</param>
        /// <param name="ftpPwd">ftp password</param>
        public static async Task DownloadAsync(string filePath, string ftpUrl, string ftpUser, string ftpPwd)
        {
            FtpWebRequest request;
            request = WebRequest.Create(new Uri(ftpUrl)) as FtpWebRequest;
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.UseBinary = true;
            request.UsePassive = true;
            request.KeepAlive = true;
            request.Credentials = new NetworkCredential(ftpUser, ftpPwd);
            Stream ftpStream = (await request.GetResponseAsync()).GetResponseStream();
            Stream fileStream = File.Create(filePath);
            byte[] buffer = new byte[10240];
            int read;
            while ((read = await ftpStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                await fileStream.WriteAsync(buffer, 0, read);
                int position = (int)fileStream.Position;
            }
        }
    }
}
