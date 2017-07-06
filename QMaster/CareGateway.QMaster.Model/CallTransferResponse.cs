using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gdot.Care.Common.Model;
using Newtonsoft.Json;

namespace CareGateway.QMaster.Model
{
    public class CallTransferResponse
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public string StatusCode { get; set; }
        /// <summary>
        ///     Required : return qMasterKey to WAVE
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public string SessionId { get; set; }
    }
}
