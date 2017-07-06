using System;
using System.Net;
using Gdot.Care.Common.Enum;
using System.Diagnostics.CodeAnalysis;

namespace Gdot.Care.Common.Api.ErrorHandler
{
    [ExcludeFromCodeCoverage]
    public class ApiErrorResponse
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public string ErrorId { get; set; }
        public ErrorCodeEnum ErrorCode { get; set; }
        public string ErrorName { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime Date { get; set; }
        public string ExternalResponseCode { get; set; }
        public string ExternalResponseText { get; internal set; }
        public string EndPointConfigurationName { get; internal set; }
        public string OperationName { get; internal set; }
        public string EventType { get; set; }
    }
}
