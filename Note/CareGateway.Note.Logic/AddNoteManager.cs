using Gdot.Care.Common.Exceptions;
using Gdot.Care.Common.Interface;
using Gdot.Care.Common.Logging;
using CareGateway.Db.Note.Model;
using CareGateway.Note.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareGateway.Note.Logic
{
    public class AddNoteManager :
         INote<AddNoteResponse, AddNoteRequest>
    {
        public ISqlCommand<AddNoteOutput, AddNoteInput> AddNoteCommand { get; set; }
        public IRequestHeaderInfo RequestHeaderInfo { get; set; }

        public async Task<AddNoteResponse> Execute(AddNoteRequest req)
        {
            var careAgentUserName = RequestHeaderInfo.GetUserName();
            try
            {               
                if (String.IsNullOrEmpty(careAgentUserName))
                {
                    throw new BadRequestException("Invalid parameter header.UserName");
                }
                var rsp = await AddNoteCommand.ExecuteAsync(new AddNoteInput { Note = req.Note,
                    UserFullName = req.UserFullName,
                    CareAgentUserName = careAgentUserName,
                    AccountIdentifier = new Guid(req.AccountIdentifier) }
                                                                );
                return new AddNoteResponse { Notekey = rsp.Notekey };
            }
            catch (GdErrorException)
            {
                throw;
            } 
            catch (Exception ex)
            {
                throw new GdErrorException(
                                    $"Error while executing AddNote for UserFullName={req.UserFullName}, Note={req.Note}",
                                    new LogObject("NoteManager_AddNote",
                                        new Dictionary<string, object>
                                        {
                            {"CareAgentName", careAgentUserName},
                            {"Note", req.Note}
                                        }), ex);
            }
        }
    }
}
