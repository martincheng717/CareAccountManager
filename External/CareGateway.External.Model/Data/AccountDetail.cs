using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareGateway.External.Model.Data
{
    public class AccountDetail
    {
        public string AccountExternalID { get; set; }

        public string Association { get; set; }

        public string Processor { get; set; }

        public string CardExternalID { get; set; }

        public string State { get; set; }

        public string Stage { get; set; }

        public string Reason { get; set; }

        public string Cure { get; set; }

        public string AccountNumber { get; set; }
    }
}
