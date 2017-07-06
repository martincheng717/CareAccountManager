using CareGateway.Account.Model;
using CareGateway.External.Client.Interfaces;
using CareGateway.External.Model.Request;
using Gdot.Care.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gdot.Care.Common.Logging;

namespace CareGateway.Account.Logic
{
    public class GetFullSSNManager : IAccount<GetFullSSNResponse, string>
    {
        public ICRMCoreService CRMCoreService { get; set; }
        public async Task<GetFullSSNResponse> Execute(string ssnToken)
        {
            try
            {
                var response = new GetFullSSNResponse();
                var rsp = await CRMCoreService.GetSSNBySSNToken(new GetSSNBySSNTokenRequest { SSNToken = ssnToken });
                if (rsp == null)
                {
                    throw new NotFoundException("No record found",
                        new LogObject("GetFullSSNManager",
                           new Dictionary<string, object> { { "SSNTOken", ssnToken } }));
                }
                return new GetFullSSNResponse { SSN = rsp.SSN };
            }
            catch (GdErrorException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new GdErrorException("Error when executing GetFullSSN",
                                       new LogObject("GetFullSSNManager",
                                       new Dictionary<string, object> { { "SSNToken", ssnToken} }), ex);
            }

        }
    }
}
