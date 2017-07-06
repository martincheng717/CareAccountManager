using System;
using System.Diagnostics.CodeAnalysis;
using Gdot.Care.Common.Logging;
namespace Gdot.Care.Common.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class NotFoundException: GdErrorException
    {
        public NotFoundException(string msg) : base(msg)
        {
        }
        public NotFoundException(string msg, LogObject logData) : base(msg, logData)
        {
        }
        public NotFoundException(string msg, LogObject logData, Exception ex) : base(msg, logData, ex)
        {
        }
    }
}
