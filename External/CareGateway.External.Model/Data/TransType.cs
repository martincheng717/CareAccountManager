using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareGateway.External.Model.Data
{
    public class TransType
    {
        public int GDTransCode { get; set; }

        public string TransCodeCreditDebit { get; set; }

        public string TransCodeDescription { get; set; }

        public string GDTransactionClass { get; set; }
    }
}
