using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;

namespace Gdot.Care.Common.Exceptions
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class ApiRequestException : HttpRequestException
    {
        public HttpStatusCode StatusCode
        {
            get;
            set;
        }
        public ApiRequestException(HttpStatusCode statusCode, string msg, Exception ex) : base(msg, ex)
        {
            StatusCode = statusCode;
        }

    }
}