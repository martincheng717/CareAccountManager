using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CareGateway.External.Model.Request
{
    public class PartnerCaseActivityRequest: RequestBase
    {
        [JsonProperty("partnerCaseId")]
        public string PartnerCaseId { get; set; }

        [JsonProperty("caseId")]
        public string CaseId { get; set; }

        [JsonProperty("caseType")]
        public string CaseType { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
