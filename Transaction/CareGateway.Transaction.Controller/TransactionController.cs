using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using CareGateway.Transaction.Logic;
using CareGateway.Transaction.Model;
using Gdot.Care.Common.Api;

namespace CareGateway.Transaction.Controller
{

    [RoutePrefix("api/transaction")]
    public class TransactionController : ResourceController
    {

        public ITransaction<AllTransactionHistoryResponse, AllTransactionHistoryRequest> GetAllTransactionHistoryManager { get; set; }
        public ITransaction<AuthorizationReversalRequest> ReverseAuthorizationManager { get; set; }
      
        [Route("history")]
        [HttpGet]
        [ApiLog]
        public async Task<IHttpActionResult> GetAllTransactionHistory([FromUri] AllTransactionHistoryRequest request)
        {
            var response = await GetAllTransactionHistoryManager.Execute(request);
            return CreateResponse(response);
        }

        [Route("AuthorizationReversal")]
        [HttpPost]
        [ApiLog]
        public async Task<IHttpActionResult> AuthorizationReversal([FromBody] AuthorizationReversalRequest request)
        {
            await ReverseAuthorizationManager.Execute(request);
            return Ok();
        }

        
    }
}
