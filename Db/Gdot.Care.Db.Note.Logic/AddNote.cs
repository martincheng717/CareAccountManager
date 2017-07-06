using Gdot.Care.Common.Data;
using Gdot.Care.Common.Exceptions;
using Gdot.Care.Common.Interface;
using Gdot.Care.Common.Logging;
using Gdot.Care.Db.Note.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gdot.Care.Db.Note.Logic
{
    public class AddNote : ISqlCommand<AddNoteOutput, AddNoteInput>
    {
        private const string CommandText = "AddNoteCareTest";
        public async Task<AddNoteOutput> ExecuteAsync(AddNoteInput req)
        {
            using (var db = new SqlDatabaseEx("CrmDatabaseConnection", CommandText))
            {
                db.AddInParameter(db.Command, "@pEnterBy ", DbType.String, req.UserFullName);
                db.AddInParameter(db.Command, "@pNote ", DbType.String, req.Note);
                db.AddInParameter(db.Command, "@pAccountIdentifier", DbType.Guid, Guid.NewGuid());
                db.AddParameter(db.Command, "@RETURN_VALUE", DbType.Int32, ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Default, null);
                await db.ExecuteNonQueryAsync();
                int noteKey;
                if (!int.TryParse(Convert.ToString(db.Command.Parameters["@RETURN_VALUE"].Value), out noteKey))
                {
                    throw new GdErrorException("Invalid NoteKey returned",
                        new LogObject("AddNote_ExecuteAsync",
                            new Dictionary<string, object>
                            {
                                {"UserFullName", req.UserFullName},
                                {"Note", req.Note}
                            }));
                }
                return new AddNoteOutput {  Notekey = noteKey};
            }
        }
    }
}
