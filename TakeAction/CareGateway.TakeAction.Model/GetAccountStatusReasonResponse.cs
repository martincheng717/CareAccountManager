using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareGateway.TakeAction.Model
{
    public class GetAccountStatusReasonResponse
    {
        public string CurrentStatus { get; set; }
        public string CurrentStatusReason { get; set; }
        public List<string> TargetStatuses { get; set; }
        public int? ReasonKey { get; set; }
        public string StatusReason { get; set; }
    }
}
