using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Threading.Tasks;
using Gdot.Care.Common.Enum;
using Gdot.Care.Common.Extension;
using Gdot.Care.Common.Interface;
using Gdot.Care.Common.Logging;
using Gdot.Care.Common.Model;
using Gdot.Care.Common.Utilities;

namespace Gdot.Care.Common.Wcf
{
    [ExcludeFromCodeCoverage]
    public class WcfClient : IWcfClient
    {
        public async Task<T> Execute<T>(Func<Task<T>> func, IDictionary<string, object> logData = null)
        {
            T retval;
            var isError = false;
            logData = logData ?? new Dictionary<string, object>();
            var metric = new MetricWatcher(Constants.MetricWcfClient,
                new MetricWatcherOption
                {
                    ManualStartStop = true,
                    LoggingOption = LogOptionEnum.FullLog,
                    LogMessage = logData
                });
            try
            {
                metric.Start();
                retval = await func();
            }
            catch (Exception ex)
            {
                isError = true;
                logData.Add("WCFException", ex);
                throw;
            }
            finally
            {
                metric.Stop(isError);
            }
            return retval;
        }
    }
}
