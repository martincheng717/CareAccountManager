using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareGateway.External.Model.Response
{
    [ExcludeFromCodeCoverage]
    public class GetClsAccountOptsResponse: ResponseBase
    {
        public List<string> CloseAccountOptions { get; set; }
    }
}
