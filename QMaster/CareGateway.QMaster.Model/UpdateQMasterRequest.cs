using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareGateway.QMaster.Model
{
    public class UpdateQMasterRequest
    {
        [Required]
        public int QMasterKey { get; set; }
        [Required]
        public string GDCaseNo { get; set; }

        public string AgentFullName { get; set; }
    }
}
