using Microsoft.AspNetCore.Http;
using System.Linq;

namespace FileServer.Extensions
{
    /// <summary>
    /// HttpContextExtension
    /// </summary>
    public static class HttpContextExtension
    {
        /// <summary>
        /// GetClientIp
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetClientIp(this HttpContext context)
        {
            var ip = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (string.IsNullOrEmpty(ip))
            {
                ip = context.Connection.RemoteIpAddress.ToString();
            }
            return ip;
        }
    }
}
