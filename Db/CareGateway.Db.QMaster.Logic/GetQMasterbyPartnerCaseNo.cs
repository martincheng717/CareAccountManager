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
    public class GetQMasterbyPartnerCaseNo : ISqlCommand<QMasterInfoOutput, GetQMasterbyPartnerCaseNoInput>
    {
        private const string CommandText = "GetQMasterInfoByPartnerCaseToken";
        public async Task<QMasterInfoOutput> ExecuteAsync(GetQMasterbyPartnerCaseNoInput input)
        {
            using (var db = new SqlDatabaseEx(CommandText))
            {
                db.AddInParameter(db.Command, "@pPartnerCaseToken", DbType.String, input.PartnerCaseNo);
                var dr = await db.ExecuteReaderAsync(new Dictionary<string, object> { { "PartnerCaseToken", input.PartnerCaseNo } });
                if (await dr.ReadAsync())
                {
                    return new QMasterInfoOutput()
                    {
                        PartnerCaseNo = dr.GetValue<string>("PartnerCaseToken"),
                        QMasterKey = dr.GetValue<int>("QMasterKey"),
                        AccountIdentifier = dr.GetValue<Guid?>("AccountIdentifier").ToString(),
                        PartnerCallTypeKey = dr.GetValue<int>("PartnerCallTypeKey"),
                        SessionID = dr.GetValue<Guid>("SessionID"),
                        CaseID = dr.GetValue<string>("CaseID")
                    };
                }
            }
            return null;
        }
    }
}
