#region Encryptor 文件信息
/***********************************************************
**文 件 名：Encryptor 
**命名空间：Utility.Security 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2019-03-28 09:40:00 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion

using Utility.Extensions;

namespace Utility.Security
{
    /// <summary>
    /// 默认数据加密器
    /// </summary>
    public class DefualtEncryptor : IEncryptor
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="data">原始数据</param>
        /// <returns>已加密数据</returns>
        public string Encrypt(string data)
        {
            return data.EncryptBase64();
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="data">已加密数据</param>
        /// <returns>原始数据</returns>
        public string Decrypt(string data)
        {
            return data.DecryptBase64();
        }
    }
}
