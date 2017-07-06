using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareGateway.Db.QMaster.Logic;
using CareGateway.Db.QMaster.Model;
using CareGateway.QMaster.Model;
using Gdot.Care.Common.Exceptions;
using Gdot.Care.Common.Interface;
using Gdot.Care.Common.Logging;

namespace CareGateway.QMaster.Logic
{
    public class GetQMasterManager:IQMasterManager<QMasterInfoResponse,int>
    {
        public ISqlCommand<QMasterInfoOutput, GetQMasterByQMasterKeyInput> GetQMasterCommand { get; set; }
        public ISqlCommand<SortedList<int, PartnerCallTypeOutput>> GetAllPartnerCallTypeCommand { get; set; }

        public async Task<QMasterInfoResponse> Execute(int qMasterKey)
        {
            var qMasterInfoTask = GetQMasterCommand.ExecuteAsync(new GetQMasterByQMasterKeyInput()
            {
                QMasterKey = qMasterKey
            });
            var getAllPartnerCallTypeTask = GetAllPartnerCallTypeCommand.ExecuteAsync();
            var qMasterInfo = await qMasterInfoTask;
            var allPartnerCallType = await getAllPartnerCallTypeTask;
            if (qMasterInfo == null)
            {
                throw new NotFoundException("Record not found", new LogObject("GetQMasterManager", qMasterKey));

            }
            var partnerCallType = string.Empty;
            if (allPartnerCallType != null)
            {
                partnerCallType = allPartnerCallType.ContainsKey(qMasterInfo.PartnerCallTypeKey)
                    ? allPartnerCallType[qMasterInfo.PartnerCallTypeKey].PartnerCallType
                    : null;
            }
            return new QMasterInfoResponse()
            {
                QMasterKey = qMasterInfo.QMasterKey,
                AccountIdentifier = qMasterInfo.AccountIdentifier,
                CaseID = qMasterInfo.CaseID,
                PartnerCallTypeKey = qMasterInfo.PartnerCallTypeKey,
                PartnerCallType = partnerCallType,
                SessionID = qMasterInfo.SessionID,
                PartnerCaseNo = qMasterInfo.PartnerCaseNo
            };
        }
    }
}
