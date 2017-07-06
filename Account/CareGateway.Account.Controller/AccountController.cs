using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using CareGateway.Account.Logic;
using CareGateway.Account.Model;
using Gdot.Care.Common.Api;
using Gdot.Care.Common.Enum;

namespace CareGateway.Account.Controller
{
    [RoutePrefix("api/account")]
    public class AccountController: ResourceController
    {

        public IAccount<AccountSummaryResponse, string> GetAccountSummaryManager { get; set; }
        public IAccount<CustomerDetailResponse, string> GetCustomerDetailManager{ get; set; }
        public IAccount<AccountDetailResponse, string> GetAccountDetailManager { get; set; }
        public IAccount<List<AccountSearchInfo>, AccountSearchRequest> AccountSearchManager { get; set; }
        public IAccount<GetFullSSNResponse, string> GetFullSSNManager { get; set; }
        public IAccount<LogViewSensitiveDataRequest> LogViewSensitiveDataManager { get; set; }
        public IAccount<MonthlyStatementDateResponse,string> GetMonthlyStatementDateManager { get; set; }

        [Route("accountsummary")]
        [HttpGet]
        [ApiLog]
        public async Task<IHttpActionResult> GetAccountSummary([FromUri] string accountIdentifier)
        {
            var response = await GetAccountSummaryManager.Execute(accountIdentifier);
            return CreateResponse(response);
        }

        [Route("customerdetail")]
        [HttpGet]
        [ApiLog]
        public async Task<IHttpActionResult> GetCustomerDetail([FromUri] string accountIdentifier)
        {
            var response = await GetCustomerDetailManager.Execute(accountIdentifier);
            return CreateResponse(response);
        }
        [Route("accountdetail")]
        [HttpGet]
        [ApiLog]
        public async Task<IHttpActionResult> GetAccountDetail([FromUri] string accountIdentifier)
        {
            var response = await GetAccountDetailManager.Execute(accountIdentifier);
            return CreateResponse(response);
        }

        [Route("search")]
        [ApiLog]
        public async Task<IHttpActionResult> Search([FromBody] AccountSearchRequest request)
        {
            var response = await AccountSearchManager.Execute(request);
            return CreateResponse(response);
        }

        [Route("fullssn")]
        [ApiLog]
        public async Task<IHttpActionResult> GetFullSSN([FromUri] string ssnToken)
        {
            var response = await GetFullSSNManager.Execute(ssnToken);
            return CreateResponse(response);
        }

        [Route("logviewsensitivedata")]
        [ApiLog(LogOption = ApiLogOption.LogRequest)]
        public async Task<IHttpActionResult> LogViewSensitiveData([FromBody] LogViewSensitiveDataRequest request)
        {
             await LogViewSensitiveDataManager.Execute(request);
            return Ok();
        }

        [Route("monthlystatementdate")]
        [HttpGet]
        [ApiLog]
        public async Task<IHttpActionResult> GetMonthlyStatementDate([FromUri] string accountIdentifier)
        {
            var response = await GetMonthlyStatementDateManager.Execute(accountIdentifier);
            return CreateResponse(response);
        }

    }
}
