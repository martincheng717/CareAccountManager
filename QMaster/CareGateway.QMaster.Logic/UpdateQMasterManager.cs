using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareGateway.Db.QMaster.Model;
using CareGateway.QMaster.Model;
using Gdot.Care.Common.Exceptions;
using Gdot.Care.Common.Interface;
using Gdot.Care.Common.Logging;

namespace CareGateway.QMaster.Logic
{
    public class UpdateQMasterManager:IQMasterManager<UpdateQMasterRequest>
    {
        public ISqlCommand<bool, UpdateQMasterInput> UpdateQMasterCommand { get; set; }

        public async Task Execute(UpdateQMasterRequest req)
        {
            try
            {
                var updateQMaster = await UpdateQMasterCommand.ExecuteAsync(new UpdateQMasterInput()
                {
                    QMasterKey = req.QMasterKey,
                    CaseID = req.GDCaseNo,
                    ChangeBy = req.AgentFullName
                });

            }
            catch (Exception ex)
            {
                throw new GdErrorException(
                    $"Error while executing UpdateQMaster for QMasterKey= {req.QMasterKey}, " +
                    $"AgentFullName={req.AgentFullName}, CaseNo={req.GDCaseNo}",
                    new LogObject("UpdateQMasterManager_UpdateQMaster",
                        new Dictionary<string, object>
                        {
                            {"QMasterKey", req.QMasterKey},
                            {"AgentFullName", req.AgentFullName},
                            {"CaseNo", req.GDCaseNo}
                        }), ex);
            }

        }
    }
}
