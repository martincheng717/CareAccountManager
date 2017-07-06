using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Gdot.Care.Common.Enum;
namespace Gdot.Care.Common.Model
{
    [ExcludeFromCodeCoverage]
    public class MetricWatcherOption
    {
        public bool ManualStartStop { get; set; }
        public IDictionary<string, object> LogMessage { get; set; }
        public LogOptionEnum LoggingOption { get; set; }

        public MetricWatcherOption()
        {
            LogMessage = new Dictionary<string, object>();
            LoggingOption = LogOptionEnum.FullLog;
        }
    }
}
