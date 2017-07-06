using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareGateway.Account.Model;
using CareGateway.External.Client.Interfaces;
using CareGateway.External.Model;
using CareGateway.External.Model.Data;
using Gdot.Care.Common.Exceptions;
using Gdot.Care.Common.Logging;

namespace CareGateway.Account.Logic
{
    public class GetAccountDetailManager : IAccount<AccountDetailResponse, string>
    {
        public ICRMCoreService CRMCoreService { get; set; }

        public async Task<AccountDetailResponse> Execute(string accountIdentifier)
        {
            try
            {
                var response = new AccountDetailResponse();

                var accountDetail = await CRMCoreService.GetAccountDetail(accountIdentifier);

                if (accountDetail == null)
                {
                    throw new NotFoundException("No record found",
                        new LogObject("GetAccountDetailManager",
                            new Dictionary<string, object> {{"AccountIdentifier", accountIdentifier}}));
                }

                MappingResponse(response, accountDetail);

                return response;
            }
            catch (GdErrorException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new GdErrorException(
                    "Error when executing GetAccountDetail",
                    new LogObject("GetAccountDetailManager",
                        new Dictionary<string, object> {{"AccountIdentifier", accountIdentifier}}), ex);
            }
        }

        /// <summary>
        /// MappingResponse
        /// </summary>
        /// <param name="response"></param>
        /// <param name="accountDetail"></param>
        private static void MappingResponse(AccountDetailResponse response, AccountDetail accountDetail)
        {
            response.AccountExternalID = accountDetail.AccountExternalID;
            response.Association = accountDetail.Association;
            response.CardExternalID = accountDetail.CardExternalID;
            response.Cure = accountDetail.Cure;
            response.Processor = accountDetail.Processor;
            response.Reason = accountDetail.Reason;
            response.Stage = accountDetail.Stage;
            response.State = accountDetail.State;
            response.AccountNumber = accountDetail.AccountNumber;
        }
    }
}