﻿using System;
using System.Diagnostics.CodeAnalysis;
using Gdot.Care.Common.Logging;

namespace Gdot.Care.Common.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class UserNotAllowedException: GdErrorException
    {
        public UserNotAllowedException(string msg) : base(msg)
        {
        }
        public UserNotAllowedException(string msg, LogObject logData) : base(msg, logData)
        {
        }
        public UserNotAllowedException(string msg, LogObject logData, Exception ex) : base(msg, logData, ex)
        {
        }
    }
}
