using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareGateway.External.Model.Enum;

namespace CareGateway.External.Model.Data
{
    public class Account
    {
        public string Identifier { get; set; }
        public long AccountKey { get; set; }

        public Guid AccountId { get; set; }

        public State State { get; set; }
        public Balance Balance { get; set; }

        public string CurrencyCode { get; set; }

        //[DataMember]
        //[JsonProperty("status")]
        public AccountStatus Status { get; set; } // TODO: kstropus: Remove this

        public Stage Stage { get; set; }

        public string Cure { get; set; }


        public string[] ReasonCodes { get; set; }

        public int EventCounter { get; set; }

        public Stage? DBStage { get; set; }

        public string Bin { get; set; }

        public string Pod { get; set; }

        public int[] AccountStatusReasonKeys { get; set; }
        public string CreateDate { get; set; }

    }
}
