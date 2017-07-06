using System.ComponentModel;

namespace Gdot.Care.Common.Enum
{
    public enum CommonEventType
    {
        [Description("ApiClient")]
        ApiClient,
        WcfClient,
        SqlDatabaseEx,
        MetricWatcher,
        ApiSevice
    }
}
