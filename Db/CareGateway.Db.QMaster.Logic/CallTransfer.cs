using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareGateway.Db.QMaster.Model;
using Gdot.Care.Common.Data;
using Gdot.Care.Common.Exceptions;
using Gdot.Care.Common.Interface;
using Gdot.Care.Common.Logging;

namespace CareGateway.Db.QMaster.Logic
{
    public class CallTransfer : ISqlCommand<CallTransferOutput, CallTransferInput>
    {
        private string CommandText = "InsertQMaster";

        public async Task<CallTransferOutput> ExecuteAsync(CallTransferInput input)
        {
            using (var db = new SqlDatabaseEx(CommandText))
            {
                db.AddInParameter(db.Command, "@pPartnerCaseToken", DbType.String, input.PartnerCaseNo);
                db.AddInParameter(db.Command, "@pPartnerCallTypeKey", DbType.Int32, input.PartnerCallTypeKey);
                db.AddInParameter(db.Command, "@pSessionID", DbType.Guid, Guid.NewGuid());
                db.AddInParameter(db.Command, "@pChangeBy", DbType.String, "Partner");
                if (!string.IsNullOrEmpty(input.AccountIdentifier))
                {
                    db.AddInParameter(db.Command, "@pAccountIdentifier", DbType.Guid, new Guid(input.AccountIdentifier));
                }
                db.AddParameter(db.Command, "@RETURN_VALUE", DbType.Int32, ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Default, null);
                await db.ExecuteNonQueryAsync();
                int qMasterKey;
                if (!int.TryParse(Convert.ToString(db.Command.Parameters["@RETURN_VALUE"].Value), out qMasterKey))
                {
                    throw new GdErrorException("Invalid QMasterKey returned",
                        new LogObject("InsertQMaster_ExecuteAsync",
                            new Dictionary<string, object>
                            {
                                {"PartnerCaseToken", input.PartnerCaseNo},
                                {"AccountIdentifier", input.AccountIdentifier},
                                {"PartnerCallTypeKey", input.PartnerCallTypeKey},
                            }));
                }
                return new CallTransferOutput { QMasterKey = qMasterKey };
            }
        }
    }
}
