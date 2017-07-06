using System.ComponentModel.DataAnnotations;

namespace CareGateway.Sfdc.Model
{
    public class UpdateOFACStatusRequest
    {
        [Required]
        public string AccountIdentifier { get; set; }

        [Required]
        public bool IsOfacMatch { get; set; }

        [Required]
        public string CaseNumber { get; set; }
    }
}
