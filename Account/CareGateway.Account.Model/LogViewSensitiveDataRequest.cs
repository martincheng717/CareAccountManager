using CareGateway.Account.Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareGateway.Account.Model
{
    public class LogViewSensitiveDataRequest
    {
        [Required]
        public ReferenceType ReferenceType { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string ReferenceValue { get; set; }
        [Required]
        public ViewEventType ViewEvent { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string FullName { get; set; }
    }
}
