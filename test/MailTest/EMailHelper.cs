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

using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailTest
{
    /// <summary>
    /// 基于MailKit的邮件帮助类
    /// </summary>
    public static class EMailHelper
    {
        /// <summary>
        /// 邮件服务器Host
        /// </summary>
        public static string Host { get; set; }

        /// <summary>
        /// 邮件服务器Port
        /// </summary>
        public static int Port { get; set; }

        /// <summary>
        /// 邮件服务器是否是ssl
        /// </summary>
        public static bool UseSsl { get; set; }

        /// <summary>
        /// 发送邮件的账号友善名称
        /// </summary>
        public static string UserName { get; set; }

        /// <summary>
        /// 发送邮件的账号地址
        /// </summary>
        public static string UserAddress { get; set; }

        /// <summary>
        /// 发现邮件所需的账号密码
        /// </summary>
        public static string Password { get; set; }

        /// <summary>
        /// 发送电子邮件，默认发送方为<see cref="UserAddress"/>
        /// </summary>
        /// <param name="subject">邮件主题</param>
        /// <param name="content">邮件内容主题</param>
        /// <param name="toAddress">接收方信息</param>
        /// <param name="textFormat">内容主题模式，默认TextFormat.Text</param>
        /// <param name="attachments">附件</param>
        /// <param name="dispose">是否自动释放附件所用Stream</param>
        /// <returns></returns>
        public static async Task SendEMailAsync(string subject,
            string content,
            IEnumerable<MailboxAddress> toAddress,
            TextFormat textFormat = TextFormat.Text,
            IEnumerable<AttachmentInfo> attachments = null,
            bool dispose = true)
        {
            await SendEMailAsync(subject, content, new MailboxAddress[] { new MailboxAddress(UserName, UserAddress) }, toAddress, textFormat, attachments, dispose).ConfigureAwait(false);
        }

        /// <summary>
        /// 发送电子邮件
        /// </summary>
        /// <param name="subject">邮件主题</param>
        /// <param name="content">邮件内容主题</param>
        /// <param name="fromAddress">发送方信息</param>
        /// <param name="toAddress">接收方信息</param>
        /// <param name="textFormat">内容主题模式，默认TextFormat.Text</param>
        /// <param name="attachments">附件</param>
        /// <param name="dispose">是否自动释放附件所用Stream</param>
        /// <returns></returns>
        public static async Task SendEMailAsync(string subject,
            string content,
            MailboxAddress fromAddress,
            IEnumerable<MailboxAddress> toAddress,
            TextFormat textFormat = TextFormat.Text,
            IEnumerable<AttachmentInfo> attachments = null,
            bool dispose = true)
        {
            await SendEMailAsync(subject, content, new MailboxAddress[] { fromAddress }, toAddress, textFormat, attachments, dispose).ConfigureAwait(false);
        }

        /// <summary>
        /// 发送电子邮件
        /// </summary>
        /// <param name="subject">邮件主题</param>
        /// <param name="content">邮件内容主题</param>
        /// <param name="fromAddress">发送方信息</param>
        /// <param name="toAddress">接收方信息</param>
        /// <param name="textFormat">内容主题模式，默认TextFormat.Text</param>
        /// <param name="attachments">附件</param>
        /// <param name="dispose">是否自动释放附件所用Stream</param>
        /// <returns></returns>
        public static async Task SendEMailAsync(string subject,
            string content,
            IEnumerable<MailboxAddress> fromAddress,
            IEnumerable<MailboxAddress> toAddress,
            TextFormat textFormat = TextFormat.Text,
            IEnumerable<AttachmentInfo> attachments = null,
            bool dispose = true)
        {
            var message = new MimeMessage();
            message.From.AddRange(fromAddress);
            message.To.AddRange(toAddress);
            message.Subject = subject;
            var body = new TextPart(textFormat)
            {
                Text = content
            };
            MimeEntity entity = body;
            if (attachments != null)
            {
                var mult = new Multipart("mixed")
                {
                    body
                };
                foreach (var att in attachments)
                {
                    if (att.Stream != null)
                    {
                        var attachment = string.IsNullOrWhiteSpace(att.ContentType) ? new MimePart() : new MimePart(att.ContentType);
                        attachment.Content = new MimeContent(att.Stream);
                        attachment.ContentDisposition = new ContentDisposition(ContentDisposition.Attachment);
                        attachment.ContentTransferEncoding = att.ContentTransferEncoding;
                        attachment.FileName = ConvertHeaderToBase64(att.FileName, Encoding.UTF8);//解决附件中文名问题
                        mult.Add(attachment);
                    }
                }
                entity = mult;
            }
            message.Body = entity;
            message.Date = DateTime.Now;
            using (var client = new SmtpClient())
            {
                //创建连接
                await client.ConnectAsync(Host, Port, UseSsl).ConfigureAwait(false);
                await client.AuthenticateAsync(UserAddress, Password).ConfigureAwait(false);
                await client.SendAsync(message).ConfigureAwait(false);
                await client.DisconnectAsync(true).ConfigureAwait(false);
                if (dispose && attachments != null)
                {
                    foreach (var att in attachments)
                    {
                        att.Dispose();
                    }
                }
            }
        }

        /// <summary>
        /// ConvertToBase64
        /// </summary>
        /// <param name="inputStr"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        private static string ConvertToBase64(string inputStr, Encoding encoding)
        {
            return Convert.ToBase64String(encoding.GetBytes(inputStr));
        }

        /// <summary>
        /// ConvertHeaderToBase64
        /// </summary>
        /// <param name="inputStr"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        private static string ConvertHeaderToBase64(string inputStr, Encoding encoding)
        {
            var encode = !string.IsNullOrEmpty(inputStr) && inputStr.Any(c => c > 127);
            if (encode)
            {
                return "=?" + encoding.WebName + "?B?" + ConvertToBase64(inputStr, encoding) + "?=";
            }
            return inputStr;
        }
    }

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
        private Stream stream;

        /// <summary>
        /// 文件数据流，获取数据时优先采用此部分
        /// </summary>
        public Stream Stream
        {
            get
            {
                if (stream == null && Data != null)
                {
                    stream = new MemoryStream(Data);
                }
                return stream;
            }
            set { stream = value; }
        }

        /// <summary>
        /// 释放Stream
        /// </summary>
        public void Dispose()
        {
            if (stream != null)
            {
                stream.Dispose();
            }
        }
    }
}
