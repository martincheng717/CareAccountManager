using Gdot.Care.Common.Data;
using Gdot.Care.Common.Exceptions;
using Gdot.Care.Common.Interface;
using Gdot.Care.Common.Logging;
using CareGateway.Db.Note.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace CareGateway.Db.Note.Logic
{
    [ExcludeFromCodeCoverage]
    public class AddNote : ISqlCommand<AddNoteOutput, AddNoteInput>
    {
        private const string CommandText = "AddNote";
        public async Task<AddNoteOutput> ExecuteAsync(AddNoteInput req)
        {
            using (var db = new SqlDatabaseEx(CommandText))
            {
                db.AddInParameter(db.Command, "@pChangeBy ", DbType.String, Environment.UserName);
                db.AddInParameter(db.Command, "@pNote ", DbType.String, req.Note);
                db.AddInParameter(db.Command, "@pAccountIdentifier", DbType.Guid, req.AccountIdentifier);
                db.AddInParameter(db.Command, "@pCareAgentName", DbType.String, req.UserFullName);
                db.AddInParameter(db.Command, "@pCareAgentUserName", DbType.String, req.CareAgentUserName);
                db.AddParameter(db.Command, "@RETURN_VALUE", DbType.Int32, ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Default, null);
                await db.ExecuteNonQueryAsync();
                int noteKey;
                if (!int.TryParse(Convert.ToString(db.Command.Parameters["@RETURN_VALUE"].Value), out noteKey))
                {
                    throw new GdErrorException("Invalid NoteKey returned",
                        new LogObject("AddNote_ExecuteAsync",
                            new Dictionary<string, object>
                            {
                                {"CareAgentUserName", req.CareAgentUserName},
                                {"Note", req.Note}
                            }));
                }
                return new AddNoteOutput { Notekey = noteKey };
            }
        }
    }
}
