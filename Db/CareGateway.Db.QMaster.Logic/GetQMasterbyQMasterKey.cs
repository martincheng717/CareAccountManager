using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareGateway.Db.QMaster.Model;
using Gdot.Care.Common.Data;
using Gdot.Care.Common.Extension;
using Gdot.Care.Common.Interface;

namespace CareGateway.Db.QMaster.Logic
{
    public class GetQMasterbyQMasterKey : ISqlCommand<QMasterInfoOutput, GetQMasterByQMasterKeyInput>
    {
        private const string CommandText = "GetQMasterInfoByQMasterKey";

        public async Task<QMasterInfoOutput> ExecuteAsync(GetQMasterByQMasterKeyInput input)
        {
            using (var db = new SqlDatabaseEx(CommandText))
            {
                db.AddInParameter(db.Command, "@pQMasterKey", DbType.Int32, input.QMasterKey);
                var qMasterInfoOutput = new QMasterInfoOutput();
                var dr = await db.ExecuteReaderAsync(new Dictionary<string, object> { { "QMasterKey", input.QMasterKey } });
                if (await dr.ReadAsync())
                {
                    qMasterInfoOutput.PartnerCaseNo = dr.GetValue<string>("PartnerCaseToken");
                    qMasterInfoOutput.QMasterKey = dr.GetValue<int>("QMasterKey");
                    qMasterInfoOutput.AccountIdentifier = dr.GetValue<Guid?>("AccountIdentifier").ToString();
                    qMasterInfoOutput.PartnerCallTypeKey = dr.GetValue<int>("PartnerCallTypeKey");
                    qMasterInfoOutput.SessionID = dr.GetValue<Guid>("SessionID");
                    qMasterInfoOutput.CaseID = dr.GetValue<string>("CaseID");
                    return qMasterInfoOutput;
                }
                return null;
            }
        }
    }
}
