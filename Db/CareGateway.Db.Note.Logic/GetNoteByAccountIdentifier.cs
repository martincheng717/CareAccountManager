using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareGateway.Db.Note.Model;
using Gdot.Care.Common.Data;
using Gdot.Care.Common.Extension;
using Gdot.Care.Common.Interface;

namespace CareGateway.Db.Note.Logic
{
    public class GetNoteByAccountIdentifier : ISqlCommand<List<GetNotesOutput>, Guid>
    {
        private const string CommandText = "GetNoteByAccountIdentifier";

        public async Task<List<GetNotesOutput>> ExecuteAsync(Guid input)
        {
            using (var db = new SqlDatabaseEx(CommandText))
            {
                db.AddInParameter(db.Command, "@pAccountIdentifier", DbType.Guid, input);
                var getNotesOutput = new List<GetNotesOutput>();
                var dr = await db.ExecuteReaderAsync(new Dictionary<string, object> { { "AccountIdentifier", input.ToString() } });
                while (await dr.ReadAsync())
                {
                    getNotesOutput.Add(new GetNotesOutput()
                    {
                        Notekey = dr.GetValue<int>("NoteKey"),
                        CareAgentName = dr.GetValue<string>("CareAgentName"),
                        AccountIdentifier = dr.GetValue<Guid?>("AccountIdentifier").ToString(),
                        Note = dr.GetValue<string>("Note"),
                        ChangeBy = dr.GetValue<string>("ChangeBy"),
                        CreateDate = dr.GetValue<DateTime>("CreateDate")
                    });
                }
                return getNotesOutput;
            }
        }
    }
}
