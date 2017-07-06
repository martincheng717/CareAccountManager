using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareGateway.External.Model.Response
{
    public class RiskResponseBase
    {
        public int ResponseCode { get; set; }

        public string ResponseDescription { get; set; }

        public List<ResponseDetails> ResponseDetails { get; set; }
    }

    public class ResponseDetails
    {
        public int ResponseDetailCode { get; set; }

        public string ResponseDetailDescription { get; set; }
    }
}
