using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CareGateway.External.Model.Request
{
    public class RequestBase
    {/// <summary>
     ///     Required : Container for request header details
     /// </summary>
        [JsonProperty("requestHeader", Order = -2)]
        //[Required]
        public RequestHeader Header { get; set; }
    }
}
