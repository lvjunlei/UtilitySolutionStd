using Newtonsoft.Json;
using System.IO;
using System.Security.Cryptography;

namespace Utility.Helpers
{
    /// <summary>
    /// RSA 帮助类
    /// </summary>
    public static class RsaHelper
    {
        /// <summary>
        /// 从本地文件中读取用来签发 Token 的 RSA Key
        /// </summary>
        /// <param name="filePath">存放密钥的文件夹路径</param>
        /// <param name="isPrivate">是否是私钥</param>
        /// <param name="keyParameters">秘钥</param>
        /// <returns></returns>
        public static bool TryGetKeyParameters(string filePath, bool isPrivate, out RSAParameters keyParameters)
        {
            var fileName = isPrivate ? "key.json" : "key.public.json";
            keyParameters = default;
            if (!Directory.Exists(filePath))
            {
                return false;
            }
            var file = Path.Combine(filePath, fileName);
            if (!File.Exists(file))
            {
                return false;
            }
            keyParameters = JsonConvert.DeserializeObject<RSAParameters>(File.ReadAllText(file));
            return true;
        }

        /// <summary>
        /// 生成并保存 RSA 公钥与私钥
        /// </summary>
        /// <param name="filePath">存放密钥的文件夹路径</param>
        /// <returns>私钥</returns>
        public static RSAParameters CreateAndSaveKey(string filePath)
        {
            RSAParameters publicKeys, privateKeys;
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                try
                {
                    privateKeys = rsa.ExportParameters(true);
                    publicKeys = rsa.ExportParameters(false);
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
            File.WriteAllText(Path.Combine(filePath, "key.json"), JsonConvert.SerializeObject(privateKeys));
            File.WriteAllText(Path.Combine(filePath, "key.public.json"), JsonConvert.SerializeObject(publicKeys));
            return privateKeys;
        }
    }
}
