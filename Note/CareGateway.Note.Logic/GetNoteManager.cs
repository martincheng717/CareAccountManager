using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareGateway.Db.Note.Model;
using CareGateway.Note.Model;
using Gdot.Care.Common.Exceptions;
using Gdot.Care.Common.Interface;
using Gdot.Care.Common.Logging;

namespace CareGateway.Note.Logic
{
    public class GetNoteManager:INote<List<GetNotesOutput>,string>
    {
        public ISqlCommand<List<GetNotesOutput>, Guid> GetNoteCommand { get; set; }

        public async Task<List<GetNotesOutput>> Execute(string accountIdentifier)
        {
            try
            {
                Guid accountID;
                if (!Guid.TryParse(accountIdentifier, out accountID))
                {
                    throw new GdValidateException("Invalid Account Identifier",
                        new LogObject("GetNoteManager", accountIdentifier));
                }
                var notes = await GetNoteCommand.ExecuteAsync(accountID);

                if (notes == null || !notes.Any())
                {
                    throw new NotFoundException($"No note found for AccountIdentifier={accountIdentifier}",
                        new LogObject("GetNote",
                            new Dictionary<string, object> {{"AccountIdentifier", accountIdentifier}}));
                }
                return notes;
            }
            catch (GdErrorException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new GdErrorException(
                    $"Error while executing GetNote for AccountIdentifier={accountIdentifier}",
                    new LogObject("NoteManager_GetNote",
                        new Dictionary<string, object>
                        {
                            {"AccountIdentifier", accountIdentifier}
                        }), ex);
            }
        }

    }
}
