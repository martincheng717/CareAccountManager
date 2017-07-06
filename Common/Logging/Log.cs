using System;
using Gdot.Care.Common.Interface;
using Gdot.Care.Common.Utilities;
using log4net;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Gdot.Care.Common.Logging
{
    [ExcludeFromCodeCoverage]
    public sealed class Log : ILogger
    {
        private static readonly Lazy<Log> Lazy = new Lazy<Log>(() => new Log());
        private static ILog _log;

        public static AsyncLocal<IDictionary<string,object>> LogProperty = new AsyncLocal<IDictionary<string, object>>();
        public static string Log4NetConfigPath { get; set; }

        public static void Configure()
        {
            var fi = string.IsNullOrEmpty(Log4NetConfigPath)
                ? ConfigManager.Instance.GetLog4NetConfig()
                : ConfigManager.Instance.GetLog4NetConfig(Log4NetConfigPath);
            log4net.Config.XmlConfigurator.ConfigureAndWatch(fi);
        }

        public static ILogger GetLogger(Type type)
        {
            _log = LogManager.GetLogger(type);
            return Lazy.Value;
        }
        public void Info(LogObject logObject)
        {
            _log.Info(logObject);
        }
        public void Warn(LogObject logObject)
        {
            _log.Warn(logObject);
        }
        public void Warn(LogObject logObject, Exception ex)
        {
            _log.Warn(logObject, ex);
        }
        public void Error(LogObject logObject, Exception exception = null)
        {
            _log.Error(logObject, exception);
        }
    }
}
