using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using CareGateway.External.Client.Interfaces;
using CareGateway.External.Model.Request;
using CareGateway.External.Model.Response;
using Gdot.Care.Common.Api;
using Gdot.Care.Common.Enum;
using Gdot.Care.Common.Exceptions;
using Gdot.Care.Common.Interface;
using Gdot.Care.Common.Logging;
using Gdot.Care.Common.Model.Configuration;
using Gdot.Care.Common.Utilities;
using Newtonsoft.Json;

namespace CareGateway.External.Client
{
    [ExcludeFromCodeCoverage]
    public class PartnerService:IPartnerService
    {
        private readonly IApiClient _apiClient;
        private static readonly ILogger Log = Gdot.Care.Common.Logging.Log.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public PartnerService(IApiClient client)
        {
            _apiClient = client ?? new ApiClient();
        }
        public async Task<ResponseBase> PublishCaseStatus(PartnerCaseActivityRequest request)
        {
            var endpointUrl = $"{ConfigManager.Instance.GetWebConfigBaseURI<PartnerWebApiClientSection>("partnerWebApiClient").BaseUrl?.Value}publishCaseStatus";
            var requestJson = JsonConvert.SerializeObject(request);

            var logDic = new Dictionary<string, object>
            {
                { "RequestBody", requestJson},
            };
            try
            {
                var handler = new HttpClientHandler
                {
                    PreAuthenticate = true,
                    ClientCertificateOptions = ClientCertificateOption.Automatic
                };

                _apiClient.Client = new HttpClient(handler);

                foreach (var header in ConfigManager.Instance.GetPartnerApiHeader())
                {
                    _apiClient.Client.DefaultRequestHeaders.Add(header.Name, header.Value);
                }
                _apiClient.Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                _apiClient.Client.DefaultRequestHeaders.Add(
                    "User-Agent",
                    "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36");

                logDic.Add("DefaultRequestHeaders", _apiClient.Client.DefaultRequestHeaders.ToString());
                var response = await _apiClient.PutAsync<ResponseBase, PartnerCaseActivityRequest>(
                    new Uri(endpointUrl), request, LogOptionEnum.FullLog, logDic);
                HandleResponse(logDic, response);
                return response;
            }
            catch (Exception ex)
            {
                Log.Info(new LogObject("PublishCaseStatus_PartnerService", logDic));
     
                throw new ExternalErrorException(
                    "Error when calling PublishCaseStatus route from PartnerService",
                    new LogObject("PartnerService_PublishCaseStatus",
                        logDic), ex);

            }
        }

        private static void HandleResponse(Dictionary<string, object> logDic, ResponseBase response)
        {
            logDic.Add("ResponseId", response.ResponseHeader?.ResponseId);
            logDic.Add("StatusCode", response.ResponseHeader?.StatusCode);
            logDic.Add("StatusMessage", response.ResponseHeader?.StatusMessage);
            logDic.Add("SubStatusMessage", response.ResponseHeader?.SubStatusMessage);
            logDic.Add("SubStatusCode", response.ResponseHeader?.SubStatusCode);
            Log.Info(new LogObject("CaseActivityManager_PartnerService", logDic));
        }
    }
}
