using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Utility.Data;

namespace Utility.Security
{
    /// <summary>
    /// Jwt服务接口
    /// </summary>
    public interface IJwtService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        ClaimsIdentity CreateClaimIdentity(IUser user);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        /// <param name="refreshToken"></param>
        /// <param name="claimsIdentity"></param>
        /// <returns></returns>
        Task<string> CreateEncodedToken(string account, string refreshToken, ClaimsIdentity claimsIdentity);

        /// <summary>
        /// 生成 Token 信息
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <param name="audience">Token 接受者</param>
        /// <param name="expireTime">过期时间</param>
        /// <returns></returns>
        string CreateEncodedJwt(IUser user, string audience, DateTime expireTime);
    }
}
