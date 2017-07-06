using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CareGateway.Partner.Model
{
    public class CaseActivityRequest
    {
        [Required(AllowEmptyStrings = false)]
        public string PartnerCaseNo { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string CaseNo { get; set; }
        public int? PartnerCaseType { get; set; }

        [Required(AllowEmptyStrings = false)]
        public int ParentCaseStatus { get; set; }
    }
}
