#region IEncryptor 文件信息
/***********************************************************
**文 件 名：IEncryptor 
**命名空间：Utility.Security 
**内     容： 
**功     能： 
**文件关系： 
**作     者：LvJunlei
**创建日期：2019-03-14 16:09:38 
**版 本 号：V1.0.0.0 
**修改日志： 
**版权说明： 
************************************************************/
#endregion


namespace Utility.Security
{
    /// <summary>
    /// 数据加密器
    /// </summary>
    public interface IEncryptor
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="data">原始数据</param>
        /// <returns>已加密数据</returns>
        string Encrypt(string data);

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="data">已加密数据</param>
        /// <returns>原始数据</returns>
        string Decrypt(string data);
    }
}
