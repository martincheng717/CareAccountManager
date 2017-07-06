using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareGateway.External.Model.Response
{

    [ExcludeFromCodeCoverage]
    public class ResponseBase
    {
        public ResponseBase()
        {
            ResponseHeader = new ResponseHeader();
        }
        public ResponseHeader ResponseHeader { get; set; }

    }
}
