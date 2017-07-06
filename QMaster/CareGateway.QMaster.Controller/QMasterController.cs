using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CareGateway.QMaster.Logic;
using CareGateway.QMaster.Model;
using Gdot.Care.Common.Api;

namespace CareGateway.QMaster.Controller
{
    [Route("api/qmaster")]
    public class QMasterController:ResourceController
    {

        public IQMasterManager<CallTransferResponse, CallTransferRequest> CallTransferManager { get; set; }
        public IQMasterManager<QMasterInfoResponse, int> GetQMasterManager { get; set; }
        public IQMasterManager<UpdateQMasterRequest> UpdateQMasterManager { get; set; }
        
        [ApiLog]
        public async Task<IHttpActionResult> Post([FromBody] CallTransferRequest request)
        {
            return CreateResponse(await CallTransferManager.Execute(request));
        }
        
        [ApiLog]
        public async Task<IHttpActionResult> Get([FromUri] int qMasterKey)
        {
            return CreateResponse(await GetQMasterManager.Execute(qMasterKey));

        }

        [ApiLog]
        public async Task<IHttpActionResult> Put([FromBody] UpdateQMasterRequest request)
        {
            await UpdateQMasterManager.Execute(request);
            return Ok();
        }
    }
}
