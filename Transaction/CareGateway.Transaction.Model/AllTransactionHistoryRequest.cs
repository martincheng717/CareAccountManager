using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareGateway.Transaction.Model
{
    public class AllTransactionHistoryRequest
    {
        [Required(AllowEmptyStrings = false)]
        public string AccountIdentifier { get; set; }
    
        public DateTime? CycleDate { get; set; }
    }
}
