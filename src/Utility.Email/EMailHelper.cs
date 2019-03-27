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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;

namespace Utility.Email
{
    /// <summary>
    /// 基于MailKit的邮件帮助类
    /// 使用前请先调用 Init 初始化方法
    /// </summary>
    public static class EMailHelper
    {
        #region 属性

        /// <summary>
        /// 邮件服务器Host
        /// </summary>
        public static string Host { get; private set; }

        /// <summary>
        /// 邮件服务器Port
        /// </summary>
        public static int Port { get; private set; }

        /// <summary>
        /// 邮件服务器是否是ssl
        /// </summary>
        public static bool UseSsl { get; private set; }

        /// <summary>
        /// 发送邮件的账号友善名称
        /// </summary>
        public static string UserName { get; private set; }

        /// <summary>
        /// 发送邮件的账号地址
        /// </summary>
        public static string UserAccount { get; private set; }

        /// <summary>
        /// 发现邮件所需的账号密码
        /// </summary>
        public static string Password { get; private set; }

        /// <summary>
        /// 是否已初始化
        /// </summary>
        private static bool _isInit;

        #endregion

        #region 初始化

        /// <summary>
        /// 初始化EMailHelper
        /// </summary>
        /// <param name="smtpHost">邮件服务器Host</param>
        /// <param name="smtpPort">邮件服务器Port</param>
        /// <param name="useSsl">邮件服务器是否是ssl</param>
        /// <param name="userName">发送邮件的账号友善名称</param>
        /// <param name="userAccount">发送邮件的账号地址</param>
        /// <param name="password">发现邮件所需的账号密码</param>
        public static void Init(string smtpHost,
            int smtpPort,
            bool useSsl,
            string userName,
            string userAccount,
            string password)
        {
            Host = smtpHost;
            Port = smtpPort;
            UseSsl = useSsl;
            UserName = userName;
            UserAccount = userAccount;
            Password = password;
            _isInit = true;
        }

        #endregion

        #region 发送邮件

        /// <summary>
        /// 发送电子邮件，默认发送方为<see cref="UserAccount"/>
        /// </summary>
        /// <param name="subject">邮件主题</param>
        /// <param name="content">邮件内容主题</param>
        /// <param name="toAddress">接收方信息</param>
        /// <param name="files">附件(文件路径集合)</param>
        /// <returns></returns>
        public static async Task SendEMailAsync(
            string subject,
            string content,
            List<string> toAddress,
            params string[] files)
        {
            var tos = new List<MailboxAddress>();
            foreach (var address in toAddress)
            {
                tos.Add(new MailboxAddress(address));
            }
            var attachs = new List<AttachmentInfo>();
            if (files != null)
            {
                foreach (var file in files)
                {
                    var att = new AttachmentInfo
                    {
                        FileName = Path.GetFileName(file),
                        Stream = File.OpenRead(file),
                        ContentTransferEncoding = ContentEncoding.Base64
                    };
                    attachs.Add(att);
                }

            }
            await SendEMailAsync(subject, content, tos, attachments: attachs);
        }

        /// <summary>
        /// 发送电子邮件，默认发送方为<see cref="UserAccount"/>
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
            await SendEMailAsync(subject, content, new[] { new MailboxAddress(UserName, UserAccount) }, toAddress, textFormat, attachments, dispose).ConfigureAwait(false);
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
            await SendEMailAsync(subject, content, new[] { fromAddress }, toAddress, textFormat, attachments, dispose).ConfigureAwait(false);
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
            if (!_isInit)
            {
                throw new Exception("EMailHelper帮助类未经过初始化，请先调用 Init 方法。");
            }
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
                await client.AuthenticateAsync(UserAccount, Password).ConfigureAwait(false);
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

        #endregion

        #region 公共方法

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

        #endregion
    }
}
