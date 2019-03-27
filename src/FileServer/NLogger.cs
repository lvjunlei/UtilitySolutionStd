using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileServer
{
    /// <summary>
    /// 
    /// </summary>
    public class NLogger : Microsoft.Extensions.Logging.ILogger
    {
        private static readonly Logger log = LogManager.GetLogger("");
        
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="state"></param>
        /// <returns></returns>
        public IDisposable BeginScope<TState>(TState state)
        {
            return null; 
        }
        
        public bool IsEnabled(Microsoft.Extensions.Logging.LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(Microsoft.Extensions.Logging.LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            switch (logLevel)
            {
                case Microsoft.Extensions.Logging.LogLevel.Trace:

                    break;
                case Microsoft.Extensions.Logging.LogLevel.Debug:
                    break;
                case Microsoft.Extensions.Logging.LogLevel.Information:
                    break;
                case Microsoft.Extensions.Logging.LogLevel.Warning:
                    break;
                case Microsoft.Extensions.Logging.LogLevel.Error:
                    break;
                case Microsoft.Extensions.Logging.LogLevel.Critical:
                    break;
                case Microsoft.Extensions.Logging.LogLevel.None:
                    break;
            }
        }
    }
}
