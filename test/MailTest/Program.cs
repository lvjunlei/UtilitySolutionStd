using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace MailTest
{
    class Program
    {
        static void Main(string[] args)
        {
            EmailTest();


            Console.ReadLine();
        }

        private static async void EmailTest()
        {
            EMailHelper.Host = "smtp.aliyun.com";
            EMailHelper.Port = 465;
            EMailHelper.UseSsl = true;
            EMailHelper.UserName = "lvjunlei";
            EMailHelper.Password = "suifeng5211314";
            EMailHelper.UserAddress = "lvjunlei@aliyun.com";
            var subject = "2019-02-20 工作日报";
            var content = "各位领导好，附件是 2019-02-20 工作日报 请查收";
            var attachs = new List<AttachmentInfo>();
            //从指定文件夹内读取要发送的附件
            foreach (var file in Directory.GetFiles("Attach"))
            {
                var att = new AttachmentInfo
                {
                    FileName = Path.GetFileName(file),
                    //Data = File.ReadAllBytes(file)
                    Stream = File.OpenRead(file),//Data和Stream两种方式任意用一种即可
                    ContentTransferEncoding = ContentEncoding.Base64
                };
                attachs.Add(att);
            }
            await EMailHelper.SendEMailAsync(subject, content, new MailboxAddress[] {
                new MailboxAddress("lvjunlei_job@foxmail.com")
            }, attachments: attachs);
        }

        private void MailTest()
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("LvJunlei", "lvjunlei@aliyun.com"));
            message.To.Add(new MailboxAddress("穆浩泽", "1609337156@qq.com"));
            message.Subject = "2019-02-19 青岛出差工作日志";

            message.Body = new TextPart("plain")
            {
                Text = @"领导，你好：附件是本次出差青岛 2019-02-19 号的工作日志，请查收！"
            };

            //message.

            using (var client = new SmtpClient())
            {
                //client.SslProtocols = new System.Security.Authentication.SslProtocols();
                // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect("smtp.aliyun.com", 465, true);

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate("lvjunlei@aliyun.com", "suifeng5211314");

                client.Send(message);

                client.Disconnect(true);
            }
        }
    }

    public class Dog
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        public string State { get; set; }
    }
}
