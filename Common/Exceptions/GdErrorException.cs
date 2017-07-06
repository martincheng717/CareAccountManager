using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using Gdot.Care.Common.Enum;
using Gdot.Care.Common.Logging;

namespace Gdot.Care.Common.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class GdErrorException: Exception
    {
        public ErrorCodeEnum ErrorCode { get; set; }
        public LogObject LogData { get; set; }
        public string ErrorName { get; set; }


        public GdErrorException(string msg) : base(msg)
        {
        }
        public GdErrorException(string msg, LogObject logData) : base(msg)
        {
            LogData = logData;
        }
        public GdErrorException(string msg, LogObject logData, Exception ex) : base(msg, ex)
        {
            LogData = logData;
        }
        public GdErrorException(string msg, LogObject logData, ErrorCodeEnum errorCode) : base(msg)
        {
            LogData = logData;
            ErrorCode = errorCode;
        }
        public GdErrorException(string msg, LogObject logData, ErrorCodeEnum errorCode, Exception ex) : base(msg, ex)
        {
            LogData = logData;
            ErrorCode = errorCode;
        }
        public GdErrorException(string msg, LogObject logData, ErrorCodeEnum errorCode,string errorName,
            Exception ex) : base(msg, ex)
        {
            LogData = logData;
            ErrorCode = errorCode;
            ErrorName = errorName;
        }
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("ErrorCode", ErrorCode);
            info.AddValue("LogData", LogData);
            info.AddValue("ErrorName", ErrorName);
        }
    }
}
