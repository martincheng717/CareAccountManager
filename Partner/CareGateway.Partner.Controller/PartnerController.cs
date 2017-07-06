using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using CareGateway.Partner.Logic;
using CareGateway.Partner.Model;
using Gdot.Care.Common.Api;

namespace CareGateway.Partner.Controller
{
    public class PartnerController:ResourceController
    {

        public IPartner<List<CaseActivityRequest>> CaseActivityManager { get; set; }


        [ApiLog]
        public async Task<IHttpActionResult> Post([FromBody] List<CaseActivityRequest> request)
        {
            await CaseActivityManager.Execute(request);
            return Ok();
        }
    }
}
