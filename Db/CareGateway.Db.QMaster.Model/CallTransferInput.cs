using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareGateway.Db.QMaster.Model
{
    public class CallTransferInput
    {
        public string AccountIdentifier { get; set; }
        /// <summary>
        ///     Required : to generate QMasterKey and call pop to sales force case
        /// </summary>
        public string PartnerCaseNo { get; set; }
        /// <summary>
        ///     Required : to generate QMasterKey and call pop to sales force case
        /// </summary>
        public int? PartnerCallTypeKey { get; set; }
    }
}
