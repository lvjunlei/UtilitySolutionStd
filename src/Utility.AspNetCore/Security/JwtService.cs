using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Utility.Data;
using Utility.Extensions;

namespace Utility.Security
{
    /// <summary>
    /// JwtService
    /// </summary>
    public class JwtService : IJwtService
    {
        private readonly JwtIssuerOptions _tokenOptions;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tokenOptions"></param>
        public JwtService(JwtIssuerOptions tokenOptions)
        {
            _tokenOptions = tokenOptions;
        }

        /// <summary>
        /// 创建CreateClaimIdentity
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ClaimsIdentity CreateClaimIdentity(IUser user)
        {
            var jti = $"{user.Account}{user.RoleName}{DateTime.Now.TotalMilliseconds()}".Encrypt32Md5();
            var claim = new[]
            {
                new Claim(ClaimTypes.Role,user.RoleName??string.Empty),// 角色信息
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),//用户 id
                new Claim("jti",jti,ClaimValueTypes.String) //用来标识用户token
            };
            return new ClaimsIdentity(new GenericIdentity(user.Account, "TokenAuth"), claim);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        /// <param name="refreshToken"></param>
        /// <param name="claimsIdentity"></param>
        /// <returns></returns>
        public Task<string> CreateEncodedToken(string account, string refreshToken, ClaimsIdentity claimsIdentity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 生成 Token 信息
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <param name="audience">Token 接受者</param>
        /// <param name="expireTime">过期时间</param>
        /// <returns></returns>
        public string CreateEncodedJwt(IUser user, string audience, DateTime expireTime)
        {
            var handler = new JwtSecurityTokenHandler();
            var jti = $"{audience}{user.Account}{expireTime.TotalMilliseconds()}".Encrypt32Md5();
            var claim = new[]
            {
                new Claim(ClaimTypes.Role,user.RoleName??string.Empty),// 角色信息
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),//用户 id
                new Claim("jti",jti,ClaimValueTypes.String) //用来标识用户token
            };

            var identity = new ClaimsIdentity(new GenericIdentity(user.Account, "TokenAuth"), claim);

            var token = handler.CreateEncodedJwt(new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor
            {
                Issuer = _tokenOptions.Issuer,
                Audience = _tokenOptions.Audience,
                SigningCredentials = _tokenOptions.SigningCredentials,
                Subject = identity,
                Expires = expireTime
            });

            return token;
        }
    }
}
