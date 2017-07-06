using CareGateway.Account.Model;
using Gdot.Care.Common.Exceptions;
using Gdot.Care.Common.Interface;
using Gdot.Care.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CareGateway.Account.Logic
{
    public class LogViewSensitiveManager : IAccount<LogViewSensitiveDataRequest>
    {
        private static readonly ILogger Log = Gdot.Care.Common.Logging.Log.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public IRequestHeaderInfo RequestHeaderInfo { get; set; }
        public Task Execute(LogViewSensitiveDataRequest request)
        {
            if (String.IsNullOrEmpty(RequestHeaderInfo.GetUserName()))
            {
                throw new BadRequestException("Invalid parameter header.UserName");
            }
            var UserName = RequestHeaderInfo.GetUserName();
            var IpAddress = RequestHeaderInfo.GetClientIpAddress();
            return Task.Run(() =>
            {
                Log.Info(new LogObject("LogViewSensitiveManager",
                    new Dictionary<string, object> {
                        { "ReferenceType", request.ReferenceType },
                        { "ReferenceValue", request.ReferenceValue},
                        { "ViewEvent", request.ViewEvent},
                        { "FullName", request.FullName},
                        { "UserName", UserName},
                        { "IpAddress", IpAddress}
                    }));
            });
        }
    }
}
