using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareGateway.External.Client.Interfaces;
using CareGateway.External.Model.Data;
using CareGateway.TakeAction.Model;
using Gdot.Care.Common.Exceptions;
using Gdot.Care.Common.Logging;

namespace CareGateway.TakeAction.Logic
{
    public class CloseAccountManager:ITakeAction<CloseAccountResponse, CloseAccountRequest>
    {
        public ICRMCoreService CRMCoreService { get; set; }

        public async Task<CloseAccountResponse> Execute(CloseAccountRequest request)
        {
            var logObject = new Dictionary<string, object>
            {
                { "AccountIdentifier", request.AccountIdentifier },
                { "Option", request.Option.ToString()}

            };
            try
            {
                var response = new CloseAccountResponse();
                var rsp = await CRMCoreService.CloseAccount(new External.Model.Request.CloseAccountRequest()
                {
                    AccountIdentifier = request.AccountIdentifier,
                    Option = request.Option
                });
                if (rsp.Account == null)
                {
                    throw  new NotFoundException("No Account found",
                        new LogObject("CloseAccount", logObject));
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
                    "Error when executing CloseAccount",
                    new LogObject("CloseAccountManager", logObject), ex);
            }
        }

        private static void MappingResponse(CloseAccountResponse response,
            External.Model.Response.CloseAccountResponse coreResponse)
        {
            response.AccountId = coreResponse.Account.AccountId;
            response.AccountKey = coreResponse.Account.AccountKey;
            response.AccountStatusReasonKeys = coreResponse.Account.AccountStatusReasonKeys;
            response.Balance = coreResponse.Account.Balance;
            response.Bin = coreResponse.Account.Bin;
            response.CreateDate = coreResponse.Account.CreateDate;
            response.Cure = coreResponse.Account.Cure;
            response.CurrencyCode = coreResponse.Account.CurrencyCode;
            response.DBStage = coreResponse.Account.DBStage;
            response.EventCounter = coreResponse.Account.EventCounter;
            response.Identifier = coreResponse.Account.Identifier;
            response.Pod = coreResponse.Account.Pod;
            response.ReasonCodes = coreResponse.Account.ReasonCodes;
            response.Stage = coreResponse.Account.Stage;
            response.State = coreResponse.Account.State;
            response.Status = coreResponse.Account.Status;
        }
    }
}
