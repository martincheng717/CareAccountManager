using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareGateway.Db.Note.Model
{
    public class GetNotesOutput
    {
        public int Notekey { get; set; }
        public string ChangeBy { get; set; }
        public string Note { get; set; }
        public string AccountIdentifier { get; set; }
        public string CareAgentName { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
