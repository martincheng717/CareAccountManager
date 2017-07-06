using System;
using System.Threading.Tasks;
using Gdot.Care.Common.Logging;

namespace Gdot.Care.Common.Interface
{
    public interface ILogger
    {
        void Info(LogObject logObject);
        void Warn(LogObject logObject);
        void Warn(LogObject logObject, Exception ex);
        void Error(LogObject logObject, Exception exception = null);
    }
}
