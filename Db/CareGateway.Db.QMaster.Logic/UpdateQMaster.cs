using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareGateway.Db.QMaster.Model;
using Gdot.Care.Common.Data;
using Gdot.Care.Common.Interface;

namespace CareGateway.Db.QMaster.Logic
{
    public class UpdateQMaster : ISqlCommand<bool, UpdateQMasterInput>
    {
        private const string CommandText = "UpdateQMaster";
        public async Task<bool> ExecuteAsync(UpdateQMasterInput input)
        {
            using (var db = new SqlDatabaseEx(CommandText))
            {
                db.AddInParameter(db.Command, "@pQMasterKey", DbType.Int32, input.QMasterKey);
                db.AddInParameter(db.Command, "@pCaseID", DbType.String, input.CaseID);
                db.AddInParameter(db.Command, "@pChangeBy", DbType.String, Environment.UserName);
                await db.ExecuteNonQueryAsync(new Dictionary<string, object> { { "QMasterKey", input.QMasterKey } });
                return true;
            }
        }
    }
}
