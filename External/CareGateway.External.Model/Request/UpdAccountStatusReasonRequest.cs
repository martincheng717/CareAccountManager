using CareGateway.External.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareGateway.External.Model.Request
{
    public class UpdAccountStatusReasonRequest
    {
        public string AccountIdentifier { get; set; }
        public string Status { get; set; }
        public StateReason? AccountStatusReason { get; set; }
    }
}
