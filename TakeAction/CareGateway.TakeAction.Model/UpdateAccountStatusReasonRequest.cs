using CareGateway.External.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareGateway.TakeAction.Model
{
    public class UpdateAccountStatusReasonRequest
    {
        public string AccountIdentifier { get; set; }
        public string Status { get; set; }
        public string ReasonKey { get; set; }
    }
}
