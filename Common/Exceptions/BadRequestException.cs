using System;
using System.Diagnostics.CodeAnalysis;
using Gdot.Care.Common.Logging;

namespace Gdot.Care.Common.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class BadRequestException: GdErrorException
    {
        public BadRequestException(string msg) : base(msg)
        {
        }
        public BadRequestException(string msg, LogObject logData) : base(msg, logData)
        {
        }
        public BadRequestException(string msg, LogObject logData, Exception ex) : base(msg, logData, ex)
        {
        }
    }
}
