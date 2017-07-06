using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareGateway.External.Model.Data
{
    public class TransactionSummary
    {
        public string TransactionIdentifier { get; set; }
        public string TransactionDate { get; set; }

        public string TransactionDescription { get; set; }

        public string TransactionStatus { get; set; }

        public bool? IsCredit { get; set; }

        public string TransType { get; set; }

        public decimal? TransactionAmount { get; set; }

        public decimal? AvailableBalance { get; set; }

        public bool? IsReversible { get; set; }

        public long? AuthorizedTransactionKey { get; set; }
    }
}
