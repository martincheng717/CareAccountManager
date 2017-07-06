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
    public class GetAccountSummaryManager:IAccount<AccountSummaryResponse,string>
    {

        public ICRMCoreService CRMCoreService { get; set; }
        public async Task<AccountSummaryResponse> Execute(string accountIdentifier)
        {
            try
            {
                var response = new AccountSummaryResponse();
                //call to get routing number
                var accountSummary= await CRMCoreService.GetAccountSummary(accountIdentifier);

                if (accountSummary == null)
                {
                    throw new NotFoundException("No record found",
                        new LogObject("GetAccountSummaryManager",
                            new Dictionary<string, object> { { "AccountIdentifier", accountIdentifier } }));
                }
                MappingResponse(response, accountSummary);
                return response;
            }
            catch (GdErrorException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new GdErrorException(
                                    "Error when executing GetAccountSummary",
                                    new LogObject("GetAccountSummaryManager",
                                        new Dictionary<string, object> { { "AccountIdentifier", accountIdentifier } }), ex);
            }
        }

        /// <summary>
        /// MappingResponse
        /// </summary>
        /// <param name="response"></param>
        /// <param name="accountSummary"></param>
        private static void MappingResponse(AccountSummaryResponse response, AccountInfo accountSummary)
        {
            response.AccountStage = accountSummary.AccountStage;
            response.AccountState = accountSummary.AccountState;
            response.AvailableBalance = accountSummary.AvailableBalance;
            response.DOB = accountSummary.DOB;
            response.FirstName = accountSummary.FirstName;
            response.Last4SSN = accountSummary.Last4SSN;
            response.LastName = accountSummary.LastName;
            response.CareReason = accountSummary.CareReason;
            response.SSNToken = accountSummary.SSNToken;
        }
    }
}
