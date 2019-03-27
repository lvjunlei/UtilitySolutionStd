#region MailHelper 文件信息
/***********************************************************
**文 件 名：MailHelper 
**命名空间：MailTest 
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


using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;

namespace MailTest
{
    /// <summary>
    /// 邮件帮助类
    /// </summary>
    public class MailHelper
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="subject">邮件主题</param>
        /// <param name="body">邮件内容</param>
        /// <param name="to">接收人</param>
        /// <param name="cc">抄送人</param>
        /// <param name="files">邮件附件</param>
        /// <returns>发送结果</returns>
        public bool SendMail(string subject,
            string body,
            IEnumerable<string> to,
            IEnumerable<string> cc,
            params string[] files)
        {
            if (files != null)
            {
                var fss = new List<FileStream>();
                foreach (var file in files)
                {
                    if (!File.Exists(file))
                    {
                        continue;
                    }

                    //fss.Add(File.op);
                }
            }
            return false;
        }
    }
}
