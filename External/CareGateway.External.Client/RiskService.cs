using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CareGateway.External.Client.Interfaces;
using CareGateway.External.Model.Request;
using CareGateway.External.Model.Response;
using Gdot.Care.Common.Api;
using Gdot.Care.Common.Enum;
using Gdot.Care.Common.Exceptions;
using Gdot.Care.Common.Interface;
using Gdot.Care.Common.Logging;
using Gdot.Care.Common.Utilities;

namespace CareGateway.External.Client
{
    [ExcludeFromCodeCoverage]
    public class RiskService : IRiskService
    {
        private readonly IApiClient _apiClient;
        private static readonly ILogger Log = Gdot.Care.Common.Logging.Log.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public RiskService(IApiClient client)
        {
            _apiClient = client ?? new ApiClient();
        }

        public async Task UpdateOFACStatus(UpdateOFACStatusRequest request)
        {
            var endpointUrl = $"{ConfigManager.Instance.GetApiEndpoint("RiskService")}/ofac/update";
            var logDic = new Dictionary<string, object>
            {
                { "AccountIdentifier", request.AccountIdentifier },
                { "IsOfacMatch", request.IsOfacMatch },
                { "CaseNumber", request.CaseNumber }
            };

            try
            {
                var handler = new HttpClientHandler
                {
                    UseDefaultCredentials = true,
                    PreAuthenticate = true
                };
                
                _apiClient.Client = new HttpClient(handler);

                var response = await _apiClient.PostAsync<RiskResponseBase, UpdateOFACStatusRequest>(
                    new Uri(endpointUrl), request, LogOptionEnum.FullLog, logDic);

                if (response == null)
                {
                    throw new ExternalErrorException($"Error when calling UpdateOFACStatus method from RiskService return null", new LogObject($"RiskService_UpdateOFACStatus", logDic));
                }

                if (response.ResponseCode != 0)
                {
                    throw new ExternalErrorException($"Error when calling UpdateOFACStatus method from RiskService with message: { response.ResponseDescription } and details: { response.ResponseDetails?[0].ResponseDetailDescription }", new LogObject($"RiskService_UpdateOFACStatus", logDic));
                }
            }
            catch (Exception ex)
            {
                Log.Info(new LogObject("UpdateOFACStatus_RiskService", logDic));

                throw new ExternalErrorException("Error when calling UpdateOFACStatus route from RiskService", new LogObject("UpdateOFACStatus_RiskService", logDic), ex);
            }
        }
    }
}
