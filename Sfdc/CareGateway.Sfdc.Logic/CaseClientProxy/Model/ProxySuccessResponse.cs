using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareGateway.Sfdc.Logic.CaseClientProxy
{
    [ExcludeFromCodeCoverage]
    public class ProxySuccessResponse
    {
        public object Errors;
        public string Id;
        public bool Success;
    }
}
