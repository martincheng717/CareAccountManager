using System.Threading.Tasks;
using System.Web.Http;
using CareGateway.Sfdc.Logic;
using CareGateway.Sfdc.Model;
using Gdot.Care.Common.Api;
using Gdot.Care.Common.Enum;

namespace CareGateway.Sfdc.Controller
{
    [RoutePrefix("api/case")]
    public class CaseController: ApiController
    {
        public ICaseManager CaseManager { get; set; }
        // POST api/case
        public async Task<IHttpActionResult> Post([FromBody] CaseEx request)
        {
            return Ok(await CaseManager.Create(request));
        }

        public async Task<IHttpActionResult> Put([FromBody] CaseEx request)
        {
            return Ok(await CaseManager.Update(request));
        }

        [Route("updateOFACStatus")]
        [ApiLog]
        public async Task<IHttpActionResult> UpdateOFACStatus([FromBody] UpdateOFACStatusRequest request)
        {
            await CaseManager.UpdateOFACStatus(request);
            return Ok();
        }
    }
}
