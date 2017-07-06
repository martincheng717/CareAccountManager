using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareGateway.Transaction.Model
{
    public class AllTransactionHistoryResponse
    {
        public AllTransactionHistoryResponse()
        {
            DefaultCycleDay = 28;
        }
        public int DefaultCycleDay { get; set; }

        public List<Data.Transaction> Transactions { get; set; }
        public DateTime? ActivationDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
