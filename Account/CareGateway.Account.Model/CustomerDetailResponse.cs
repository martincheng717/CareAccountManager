using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareGateway.External.Model;
using CareGateway.External.Model.Data;

namespace CareGateway.Account.Model
{
    public class CustomerDetailResponse
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Last4SSN { get; set; }

        public string SSNToken { get; set; }

        public string DOB { get; set; }

        public Address Address { get; set; }

        public string AccountExternalID { get; set; }

        public DateTime? CreateDate { get; set; }
    }
}
