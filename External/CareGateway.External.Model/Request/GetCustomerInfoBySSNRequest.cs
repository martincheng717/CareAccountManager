using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CareGateway.External.Model.Request
{
    public class GetCustomerInfoBySSNRequest
    {
        [JsonProperty("ssn")]
        public string Ssn { get; set; }
    }
}
