using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareGateway.External.Model.Data;

namespace CareGateway.Account.Model
{
    public class AccountSearchInfo
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Last4SSN { get; set; }

        public string AccountState { get; set; }

        public string AccountIdentifier { get; set; }
        public string AccountNumber { get; set; }
    }
}
