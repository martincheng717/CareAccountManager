using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareGateway.External.Model.Enum;

namespace CareGateway.External.Model.Request
{
    public class CloseAccountRequest
    {
        public string AccountIdentifier { get; set; }
        public CloseAccountOptionEnum? Option { get; set; }
    }
}
