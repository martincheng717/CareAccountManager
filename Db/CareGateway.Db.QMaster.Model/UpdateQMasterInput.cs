using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareGateway.Db.QMaster.Model
{
    public class UpdateQMasterInput
    {
        public int QMasterKey { get; set; }
        public string CaseID { get; set; }
        public string ChangeBy { get; set; }
    }
}
