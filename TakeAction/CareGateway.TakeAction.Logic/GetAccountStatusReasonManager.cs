using CareGateway.External.Client.Interfaces;
using CareGateway.External.Model.Request;
using CareGateway.TakeAction.Model;
using Gdot.Care.Common.Exceptions;
using Gdot.Care.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareGateway.External.Model.Response;

namespace CareGateway.TakeAction.Logic
{
    public class GetAccountStatusReasonManager : ITakeAction<GetAccountStatusReasonResponse, GetAccountStatusReasonRequest>
    {
        public ICRMCoreService CRMCoreService { get; set; }
        public async Task<GetAccountStatusReasonResponse> Execute(GetAccountStatusReasonRequest request)
        {
            var logObject = new Dictionary<string, object> { { "AccountIdentifier", request.AccountIdentifier } };
            try
            {
                var response = new GetAccountStatusReasonResponse();
                var rsp = await CRMCoreService.GetStatusTransition(new GetStatusTransitionRequest { AccountIdentifier = request.AccountIdentifier });
                if (rsp == null)
                {
                    throw new NotFoundException("No record found",
                        new LogObject("GetAccountStatusReason", logObject));
                }
                MappingResponse(response, rsp);
                return response;
            }
            catch (GdErrorException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new GdErrorException(
                    "Error when executing GetAccountStatusReason",
                    new LogObject("GetAccountStatusReasonManager", logObject), ex);
            }
        }

        private static void MappingResponse(GetAccountStatusReasonResponse response, GetStatusTransitionResponse rsp)
        {
            response.CurrentStatus = rsp.CurrentStatus;
            response.CurrentStatusReason = rsp.CurrentStatusReason;
            response.ReasonKey = rsp.ReasonKey;
            response.StatusReason = rsp.StatusReason;
            response.TargetStatuses = rsp.TargetStatuses;
        }
    }
}
