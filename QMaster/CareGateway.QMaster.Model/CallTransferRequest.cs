using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gdot.Care.Common.Model;
using Newtonsoft.Json;

namespace CareGateway.QMaster.Model
{
    public class CallTransferRequest
    {
        /// <summary>
        ///    Not Required : to generate QMasterKey and call pop to sales force case
        /// </summary>
        public string AccountIdentifier { get; set; }

        /// <summary>
        ///     Required : to generate QMasterKey and call pop to sales force case
        /// </summary>
        [Required]
        public string OriginatorCaseNo { get; set; }

        /// <summary>
        ///     Required : to generate QMasterKey and call pop to sales force case
        /// </summary>
        [Required]
        public string IssueCode { get; set; }
    }
}
