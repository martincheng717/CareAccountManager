using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareGateway.Account.Model
{
    public class AccountSummaryResponse
    {
        
        public decimal AvailableBalance { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Last4SSN { get; set; }

        public string DOB { get; set; }

        public string AccountState { get; set; }

        public string AccountStage { get; set; }
        public string CareReason { get; set; }
        public string SSNToken { get; set; }
    }
}
