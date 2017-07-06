using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareGateway.Transaction.Model
{
    public class AuthorizationReversalRequest
    {
        [Required(AllowEmptyStrings = false)]
        public string AccountIdentifier { get; set; }

        [Required(AllowEmptyStrings = false)]
        public int AuthorizedTransactionKey { get; set; }
    }
}
