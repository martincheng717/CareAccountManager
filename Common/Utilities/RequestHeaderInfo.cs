using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;
using Gdot.Care.Common.Exceptions;
using Gdot.Care.Common.Interface;
using Gdot.Care.Common.Logging;

namespace Gdot.Care.Common.Utilities
{
    [ExcludeFromCodeCoverage]
    public class RequestHeaderInfo: IRequestHeaderInfo
    {
        public string GetSysComponentKey()
        {
            return GetHeaderValue("X-SysComponentKey");
        }

        public string GetCorrelationId()
        {
            return GetHeaderValue("X-CorrelationId");
        }

        public string GetSessionId()
        {
            return GetHeaderValue("X-SessionId");
        }

        public string GetSysUserKey()
        {
            return GetHeaderValue("X-SysUserKey");
        }

        public string GetClientIpAddress()
        {
            return HttpContext.Current.Request.UserHostAddress;
        }
        private static string GetHeaderValue(string name)
        {
            try
            {
                if (HttpContext.Current != null)
                {
                    return HttpContext.Current?.Request.Headers.GetValues(name)?.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var dict = new Dictionary<string, object>
                {
                    {name, "not found"},
                };
                throw new GdErrorException("unable to read name",
                    new LogObject($"RequestHeaderInfo_{name}", dict), ex);
            }
            return "";
        }

    }
}
