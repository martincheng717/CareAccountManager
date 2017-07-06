using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareGateway.Db.Note.Model
{
    [ExcludeFromCodeCoverage]
    public class AddNoteInput
    {
        public string Note{ get; set; }
        public string UserFullName { get; set; }
        public Guid AccountIdentifier { get; set; }
        public string CareAgentUserName { get; set; }
    }
}
