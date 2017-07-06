using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareGateway.QMaster.Model
{
    public class QMasterInfoResponse
    {
        public int QMasterKey { get; set; }
        public string AccountIdentifier { get; set; }
        public string PartnerCaseNo { get; set; }
        public string CaseID { get; set; }
        public string PartnerCallType { get; set; }
        public int PartnerCallTypeKey { get; set; }
        public Guid SessionID { get; set; }
    }
}
