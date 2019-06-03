using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using Utility.Helpers;
using Utility.Security;

namespace Utility.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddJwt(this IServiceCollection services, out JwtIssuerOptions _tokenOptions)
        {
            _tokenOptions = new JwtIssuerOptions();
            var keyDir = PlatformServices.Default.Application.ApplicationBasePath;
            if (!RsaHelper.TryGetKeyParameters(keyDir, true, out RSAParameters keyParameters))
            {
                keyParameters = RsaHelper.CreateAndSaveKey(keyDir);
            }
            var key=new RsaSecurityKey(keyParameters);
            _tokenOptions.Key = key;
            _tokenOptions.Issuer = "qdcares_net";
            _tokenOptions.SigningCredentials = new SigningCredentials(_tokenOptions.Key, SecurityAlgorithms.RsaSha256Signature);

            services.AddSingleton<JwtIssuerOptions>(_tokenOptions);
            return services;
        }
    }
}
