using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Gdot.Care.Common.Enum;
using Gdot.Care.Common.Interface;
using Gdot.Care.Common.Logging;
using Gdot.Care.Common.Model;

namespace Gdot.Care.Common.Utilities
{

    [ExcludeFromCodeCoverage]
    public class MetricWatcher : IDisposable
    {
        public MetricWatcherOption Options { get; set; }
        private Stopwatch _stopwatch;
        private readonly string _eventType;
        private static readonly ILogger Log = Logging.Log.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public MetricWatcher(string eventType, MetricWatcherOption options = null)
        {
            var config = ConfigManager.Instance.Configuration;
            Options = options ?? new MetricWatcherOption();
            _eventType = eventType;
            if (!Options.ManualStartStop)
            {
                Start();
            }
        }

        public void Start()
        {
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
        }

        public void Stop(bool isError = false)
        {
            if (_stopwatch != null)
            {
                _stopwatch.Stop();
                Options.LogMessage.Add("DurationInMs", _stopwatch.ElapsedMilliseconds);
                GC.SuppressFinalize(this);
                _stopwatch = null;
                if ((Options.LoggingOption == LogOptionEnum.NoLog) ||
                    (Options.LoggingOption == LogOptionEnum.SuccessLog && isError))
                {
                    return;
                }
                if (isError)
                {
                    Log.Error(new LogObject(_eventType, Options.LogMessage));
                }
                else
                {
                    Log.Info(new LogObject(_eventType, Options.LogMessage));
                }
            }
        }
        public void Dispose()
        {
            Stop();
        }

        ~MetricWatcher()
        {
            Stop();
        }
    }
}
