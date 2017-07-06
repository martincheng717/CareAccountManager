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

namespace CareGateway.TakeAction.Logic
{
    public class GetCloseAccountOptionsManager : ITakeAction<GetCloseAccountOptionsResponse, GetCloseAccountOptionsRequest>
    {
        public ICRMCoreService CRMCoreService { get; set; }
        public async Task<GetCloseAccountOptionsResponse> Execute(GetCloseAccountOptionsRequest request)
        {
            var logObject = new Dictionary<string, object> { { "AccountIdentifier", request.AccountIdentifier} };
            try
            {
                var response = new GetCloseAccountOptionsResponse();
                var rsp = await CRMCoreService.GetCloseAccountOptions(
                    new GetClsAccountOptsRequest { AccountIdentifier = request.AccountIdentifier });
                return new GetCloseAccountOptionsResponse { CloseAccountOptions = rsp?.CloseAccountOptions };
            }
            catch (GdErrorException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new GdErrorException(
                    "Error when executing GetCloseAccountOptions",
                    new LogObject("GetCloseAccountOptions", logObject), ex);
            }
            
        }
    }
}
