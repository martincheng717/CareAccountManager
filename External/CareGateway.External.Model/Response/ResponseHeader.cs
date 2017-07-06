using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareGateway.External.Model.Response
{

    [ExcludeFromCodeCoverage]
    public class ResponseHeader
    {

        public string ResponseId { get; set; }
        public string StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public string SubStatusCode { get; set; }
        public string SubStatusMessage { get; set; }
    }
}
