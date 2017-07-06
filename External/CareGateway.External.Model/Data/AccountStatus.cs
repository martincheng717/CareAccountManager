using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareGateway.External.Model.Enum;
using Newtonsoft.Json;

namespace CareGateway.External.Model.Data
{
    public class AccountStatus
    {
        [JsonProperty("OFAC")]
        public Status Ofac { get; set; }
        [JsonProperty("KYC")]
        public Status Kyc { get; set; }
    }
}
