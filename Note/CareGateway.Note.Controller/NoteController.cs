using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gdot.Care.Common.Api;
using CareGateway.Note.Model;
using CareGateway.Note.Logic;
using System.Web.Http;
using CareGateway.Db.Note.Model;
using Gdot.Care.Common.Logging;

namespace CareGateway.Note.Controller
{
    [RoutePrefix("api/note")]
    public class NoteController:
        //ApiController
        ResourceController
    {
        public INote<AddNoteResponse, AddNoteRequest> AddNoteManager { get; set; }
        public INote<List<GetNotesOutput>, string> GetNotesManager { get; set; }

        [HttpPost]
        //[ApiLog]
        public async Task<IHttpActionResult> AddNote([FromBody] AddNoteRequest addNoteRequest)
        {
            var response = await AddNoteManager.Execute(addNoteRequest);
            return CreateResponse(response);
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get([FromUri] string accountIdentifier)
        {
            var response = await GetNotesManager.Execute(accountIdentifier);
            return CreateResponse(response);
        }
    }
}
