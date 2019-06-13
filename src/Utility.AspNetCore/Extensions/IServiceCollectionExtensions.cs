

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Cryptography;
using Utility.DynamicWebApi;
using Utility.Helpers;
using Utility.Security;

namespace Utility.Extensions
{
    /// <summary>
    /// IServiceCollectionExtensions
    /// </summary>
    public static partial class IServiceCollectionExtensions
    {
        #region DynamicWebApi

        /// <summary>
        /// 添加动态　WebAPI 到容器
        /// </summary>
        /// <param name="services"></param>
        /// <param name="options">configuration</param>
        /// <returns></returns>
        public static IServiceCollection AddDynamicWebApi(this IServiceCollection services, DynamicWebApiOptions options)
        {
            if (options == null)
            {
                throw new ArgumentException(nameof(options));
            }

            options.Valid();

            AppConsts.DefaultAreaName = options.DefaultAreaName;
            AppConsts.DefaultHttpVerb = options.DefaultHttpVerb;
            AppConsts.DefaultApiPreFix = options.DefaultApiPrefix;
            AppConsts.ControllerPostfixes = options.RemoveControllerPostfixes;
            AppConsts.ActionPostfixes = options.RemoveActionPostfixes;
            AppConsts.FormBodyBindingIgnoredTypes = options.FormBodyBindingIgnoredTypes;

            var partManager = services.TryGetSingletonInstance<ApplicationPartManager>();

            if (partManager == null)
            {
                throw new InvalidOperationException("\"AddDynamicWebApi\" 必须在 \"AddMvc\" 方法之后调用");
            }

            // Add a custom controller checker
            partManager.FeatureProviders.Add(new DynamicWebApiControllerFeatureProvider());

            services.Configure<MvcOptions>(o =>
            {
                // Register Controller Routing Information Converter
                o.Conventions.Add(new DynamicWebApiConvention(services));
            });

            return services;
        }

        /// <summary>
        /// 添加 WebAPI 到容器
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDynamicWebApi(this IServiceCollection services)
        {
            return AddDynamicWebApi(services, new DynamicWebApiOptions());
        }

        #endregion

        #region JWT

        /// <summary>
        /// 添加 JWT 验证
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
            var key = new RsaSecurityKey(keyParameters);
            _tokenOptions.Key = key;
            _tokenOptions.Issuer = "qdcares_net";
            _tokenOptions.SigningCredentials = new SigningCredentials(_tokenOptions.Key, SecurityAlgorithms.RsaSha256Signature);

            services.AddSingleton<JwtIssuerOptions>(_tokenOptions);
            return services;
        }

        #endregion
    }
}
