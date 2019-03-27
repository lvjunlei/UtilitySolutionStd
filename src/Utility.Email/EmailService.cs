#region EmailService 文件信息
/***********************************************************
**文 件 名：EmailService 
**命名空间：Utility.Email 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2019-03-08 17:12:03 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Utility.Email
{
    public class EmailService : IEmailService
    {
        /// <summary>
        /// 初始化EMailHelper
        /// </summary>
        /// <param name="smtpHost">邮件服务器Host</param>
        /// <param name="smtpPort">邮件服务器Port</param>
        /// <param name="useSsl">邮件服务器是否是ssl</param>
        /// <param name="userName">发送邮件的账号友善名称</param>
        /// <param name="userAccount">发送邮件的账号地址</param>
        /// <param name="password">发现邮件所需的账号密码</param>
        public EmailService(string smtpHost,
            int smtpPort,
            bool useSsl,
            string userName,
            string userAccount,
            string password)
        {
            EMailHelper.Init(smtpHost, smtpPort, useSsl, userName, userAccount, password);
        }

        /// <summary>
        /// 发送电子邮件，默认发送方为
        /// </summary>
        /// <param name="subject">邮件主题</param>
        /// <param name="content">邮件内容主题</param>
        /// <param name="toAddress">接收方信息</param>
        /// <param name="files">附件(文件路径集合)</param>
        /// <returns></returns>
        public async Task SendEMailAsync(string subject, string content, List<string> toAddress, params string[] files)
        {
            await EMailHelper.SendEMailAsync(subject, content, toAddress, files);
        }
    }
}
