﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareGateway.External.Model.Request
{
    public class GetAllTransactionHistoryReqeust
    {
        public string AccountIdentifier { get; set; }
        public string CycleDate { get; set; }
    }
}