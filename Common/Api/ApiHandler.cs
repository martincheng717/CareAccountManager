using Gdot.Care.Common.Api.ErrorHandler;
using Gdot.Care.Common.Exceptions;
using Gdot.Care.Common.Interface;
using Gdot.Care.Common.Model;
using Gdot.Care.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Gdot.Care.Common.Enum;
using Gdot.Care.Common.Extension;
using Gdot.Care.Common.Logging;
using Newtonsoft.Json;
using System.Web;
using Newtonsoft.Json.Serialization;

namespace Gdot.Care.Common.Api
{
    [ExcludeFromCodeCoverage]
    public class ApiHandler : DelegatingHandler
    {
        private static readonly JsonMediaTypeFormatter JsonFormatter = new JsonMediaTypeFormatter
        {
            SerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            }
        };
        private static readonly ILogger Log = Logging.Log.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly string[] _excludeLogHeaders = { "x-original-url", "x-arr-ssl", "x-arr-log-id" };
        
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {

            //extract request headers
            var loggers = new Dictionary<string, object>();

            var headers = HttpContext.Current.Request.Headers;
            foreach (var req in headers.AllKeys)
            {
                //only extract any request headers start with "x-" for logging
                if (req.StartsWith(Constants.LoggingIdentifier, StringComparison.CurrentCultureIgnoreCase) && !_excludeLogHeaders.Contains(req.ToLower()))
                {
                    loggers.Add(req.Remove(0, 2), headers[req]);
                }
            }

            string correlationIdValue;
            if (loggers.ContainsKey(Constants.CorellationKey))
            {
                correlationIdValue = loggers[Constants.CorellationKey].ToString();
            }
            else
            {
                correlationIdValue = Guid.NewGuid().ToString().Replace("-", "");
                loggers.Add(Constants.CorellationKey, correlationIdValue);
                if (!headers.AllKeys.Contains(Constants.CorellationKeyHeader))
                {
                    headers.Add(Constants.CorellationKeyHeader, correlationIdValue);
                }
            }

            loggers.Add(Constants.Route, request.RequestUri.AbsoluteUri);
            HttpResponseMessage response;
            Logging.Log.LogProperty.Value = loggers;
          

            if (request.RequestUri.AbsolutePath.Contains("/api/"))
            {   
                ApiLogAttribute.LogParam.Value = new Dictionary<string, object>();
                // only do metric on /api/ route
                using (new MetricWatcher(Constants.MetricApi, new MetricWatcherOption() {  LogMessage= ApiLogAttribute.LogParam.Value }))
                {
                   response = await base.SendAsync(request, cancellationToken); 
                }
            }
            else
            {
                response = await base.SendAsync(request, cancellationToken);
            }
            return await ResponseMessage(response, cancellationToken, correlationIdValue);
        }

        private static async Task<HttpResponseMessage> ResponseMessage(HttpResponseMessage response, CancellationToken cancellationToken, string errorId)
        {
            if (!response.IsSuccessStatusCode && response.Content != null)
            {
                Exception ex;
                try
                {
                    ex = await response.Content.ReadAsAsync<Exception>(cancellationToken);
                }
                catch (Exception)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    ex = new Exception(content);
                }
                var errorCode = ErrorCodeEnum.None;
                var errorName = "";
                var eventType = "";
                // Add WaveErrorException
                //todo
                var gdException = ex as GdErrorException;
                if (gdException != null)
                {
                    errorCode = gdException.ErrorCode;
                    errorName = gdException.ErrorName;
                    if (gdException.LogData != null)
                    {
                        eventType = gdException.LogData.EventType;
                    }
                    Log.Error(gdException.LogData, gdException);
                }
                else
                {
                    Log.Error(new LogObject(CommonEventType.ApiSevice.ToString(), "An exception has occurred."), ex);
                }

                var error = new ApiErrorResponse
                {
                    HttpStatusCode = response.StatusCode,
                    ErrorId = errorId,
                    ErrorMessage = ex.GetExceptionMessages(),
                    Date = DateTime.Now,
                    ErrorCode = errorCode,
                    ErrorName = errorName,
                };
                // For SalesForce Error
                var innerException = gdException as ExternalErrorException;
                while (innerException != null && string.IsNullOrEmpty(innerException.ResponseCode))
                {
                    innerException = innerException.InnerException as ExternalErrorException;
                }
                if (innerException != null)
                {
                    error.ErrorCode = innerException.ErrorCode;
                    error.ErrorName = innerException.ErrorName;
                    if (innerException.LogData != null)
                    {
                        error.EventType = innerException.LogData.EventType;
                    }
                    error.ExternalResponseCode = innerException.ResponseCode;
                    error.ExternalResponseText = innerException.ResponseText;
                    // mapping error response
                    ApiErrorMapping.MapErrorResponse(error);
                }

                response.Content = new ObjectContent<ApiErrorResponse>(
                    error,
                    JsonFormatter, "application/json");
            }
            return response;
        }
    }
}