using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using Gdot.Care.Common.Enum;
using System.Collections.Generic;

namespace Gdot.Care.Common.Interface
{
    public interface IApiClient
    {
        HttpClient Client { get; set; }
        Task<TResponse> GetAsync<TResponse>(Uri uri, LogOptionEnum logOption = LogOptionEnum.FullLog, IDictionary<string, object> logMessage = null);
        Task<TResponse> GetAsync<TResponse>(Uri uri, MediaTypeFormatter mediaTypeFormatter, LogOptionEnum logOption = LogOptionEnum.FullLog, IDictionary<string,object> logMessage=null);
        Task<TResponse> PostAsync<TResponse, TRequest>(Uri uri, TRequest data, LogOptionEnum logOption = LogOptionEnum.FullLog, IDictionary<string, object> logMessage = null);
        Task<TResponse> PostAsync<TResponse, TRequest>(Uri uri, TRequest data, MediaTypeFormatter mediaTypeFormatter, LogOptionEnum logOption = LogOptionEnum.FullLog, IDictionary<string, object> logMessage = null);
        Task<TResponse> PutAsync<TResponse, TRequest>(Uri uri, TRequest data, LogOptionEnum logOption = LogOptionEnum.FullLog, IDictionary<string, object> logMessage = null);
        Task<TResponse> PutAsync<TResponse, TRequest>(Uri uri, TRequest data, MediaTypeFormatter mediaTypeFormatter, LogOptionEnum logOption = LogOptionEnum.FullLog, IDictionary<string, object> logMessage = null);

    }
}
