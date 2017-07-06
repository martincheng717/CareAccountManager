using System;
using Gdot.Care.Common.Logging;
using System.Diagnostics.CodeAnalysis;

namespace Gdot.Care.Common.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class ExternalErrorException : GdErrorException
    {
        public string ResponseCode { get; set; }
        public string ResponseText { get; set; }
        public ExternalErrorException(string msg) : base(msg)
        {
        }
        public ExternalErrorException(string msg, LogObject logData) : base(msg, logData)
        {
        }
        public ExternalErrorException(string msg, LogObject logData, Exception ex) : base(msg, logData, ex)
        {
        }
    }
}
