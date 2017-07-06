﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CareGateway.External.Model.Request
{
    public class RequestHeader
    {
        /// <summary>
        ///     Required : Required UUID generated by the caller
        /// </summary>
        [JsonProperty("requestId")]
        public string RequestId { get; set; }

        /// <summary>
        ///     Optional : Contains request options to use during request execution
        /// </summary>
        [JsonIgnore]
        public Dictionary<string, string> RequestOptions { get; set; }
    }
}
