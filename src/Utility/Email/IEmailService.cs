#region IEmailService 文件信息
/***********************************************************
**文 件 名：IEmailService 
**命名空间：Utility.Email 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2019-03-08 16:56:59 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion


using System.Collections.Generic;
using System.Threading.Tasks;

namespace Utility.Email
{
    /// <summary>
    /// Email服务接口
    /// </summary>
    public interface IEmailService
    {
        #region 发送邮件

        /// <summary>
        /// 发送电子邮件，默认发送方为
        /// </summary>
        /// <param name="subject">邮件主题</param>
        /// <param name="content">邮件内容主题</param>
        /// <param name="toAddress">接收方信息</param>
        /// <param name="files">附件(文件路径集合)</param>
        /// <returns></returns>
        Task SendEMailAsync(
           string subject,
           string content,
           List<string> toAddress,
           params string[] files);

        #endregion
    }
}
