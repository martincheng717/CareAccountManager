using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareGateway.External.Model.Response
{
    public class GetSSNBySSNTokenResponse: ResponseBase
    {
        public string SSN { get; set; }
    }
}
