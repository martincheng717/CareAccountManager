using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareGateway.External.Model.Data
{

    public class Transaction
    {
        public TransactionSummary Summary { get; set; }

        public TransactionDetail Detail { get; set; }
    }
}
