using CareGateway.Sfdc.Logic.CaseClientProxy.Model;
using CareGateway.Sfdc.Model.Enum;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareGateway.Sfdc.Logic.CaseClientProxy
{
    [ExcludeFromCodeCoverage]
    public class GetForceClientRequest
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string LoginAccount { get; set; }
        public string PlainTextPassword { get; set; }
        public string Domain { get; set; }
        public ClientEnum Assignment { get; set; }

    }
}
