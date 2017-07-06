using CareGateway.TakeAction.Logic;
using CareGateway.TakeAction.Model;
using Gdot.Care.Common.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace CareGateway.TakeAction.Controller
{
    [RoutePrefix("api/takeaction")]
    public class TakeActionController : ResourceController
    {
        public ITakeAction<GetAccountStatusReasonResponse, GetAccountStatusReasonRequest> GetAccountStatusReasonManager { get; set; }
        public ITakeAction<GetCloseAccountOptionsResponse, GetCloseAccountOptionsRequest> GetCloseAccountOptionsManager { get; set; }
        public ITakeAction<UpdateAccountStatusReasonRequest> UpdateAccountStatusReasonManager { get; set; }
        public ITakeAction<CloseAccountResponse, CloseAccountRequest> CloseAccountManager { get; set; }
        public ITakeAction<SendEmailTriggerReqeust> SendEmailTriggerManager { get; set; }
        public ITakeAction<GetAllTransTypeResponse, GetAllTransTypeRequest> GetAllTransTypeManager { get; set; }

        [Route("accountstatusreason")]
        [HttpGet]
        [ApiLog]
        public async Task<IHttpActionResult> AccountStatusReason([FromUri] string accountIdentifier)
        {
            var response = await GetAccountStatusReasonManager.Execute(new GetAccountStatusReasonRequest { AccountIdentifier = accountIdentifier });
            return CreateResponse(response);
        }

        [Route("CloseAccountOptions")]
        [HttpGet]
        [ApiLog]
        public async Task<IHttpActionResult> CloseAccountOptions([FromUri] string accountIdentifier)
        {
            var response = await GetCloseAccountOptionsManager.Execute(new GetCloseAccountOptionsRequest { AccountIdentifier = accountIdentifier});
            return CreateResponse(response);
        }

        [Route("updateaccountstatusreason")]
        [HttpPost]
        [ApiLog]
        public async Task<IHttpActionResult> UpdateAccountStatusReason([FromBody] UpdateAccountStatusReasonRequest request)
        {
            await UpdateAccountStatusReasonManager.Execute(request);
            return Ok();
        }


        [Route("CloseAccount")]
        [HttpPost]
        [ApiLog]
        public async Task<IHttpActionResult> CloseAccount([FromBody] CloseAccountRequest request)
        {
            var response = await CloseAccountManager.Execute(request);
            return CreateResponse(response);
        }

        [Route("sendemail")]
        [HttpPost]
        [ApiLog]
        public async Task<IHttpActionResult> SendEmail([FromBody] SendEmailTriggerReqeust request)
        {
             await SendEmailTriggerManager.Execute(request);
            return Ok();
        }

        /// <summary>
        /// Get TransType according to AccountIdentifier
        /// </summary>
        /// <param name="request">AccountIdentifier</param>
        /// <returns>
        /// OK: 200
        /// BadRequest: 40000
        /// ExternalError: 42200
        /// NotFound: 40400
        /// InternalServerError: 50000
        /// </returns>
        [Route("adjustBalance/transferType")]
        [HttpPost]
        [ApiLog]
        public async Task<IHttpActionResult> GetAllTransType([FromBody] GetAllTransTypeRequest request)
        {
            var response = await GetAllTransTypeManager.Execute(request);
            return CreateResponse(response);
        }
    }
}
