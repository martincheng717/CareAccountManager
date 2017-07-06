using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareGateway.TakeAction.Model
{
    public class GetAllTransTypeResponse
    {
        public List<TransType> CreditTransType { get; set; }

        public List<TransType> DebitTransType { get; set; }
    }
}
