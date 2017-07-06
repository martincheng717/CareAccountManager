using System;
using System.Diagnostics.CodeAnalysis;
using Gdot.Care.Common.Logging;

namespace Gdot.Care.Common.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class GdValidateException : GdErrorException
    {
        public GdValidateException(string msg) : base(msg)
        {
        }
        public GdValidateException(string msg, LogObject logData) : base(msg, logData)
        {
        }
        public GdValidateException(string msg, LogObject logData, Exception ex) : base(msg, logData, ex)
        {
        }
    }
}
