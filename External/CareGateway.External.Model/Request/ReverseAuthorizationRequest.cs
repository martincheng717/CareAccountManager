﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareGateway.External.Model.Request
{
    public class ReverseAuthorizationRequest
    {
        public string AccountIdentifier { get; set; }
        public int AuthorizedTransactionKey { get; set; }
    }
}
