using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareGateway.TakeAction.Model
{
    public class SendEmailTriggerReqeust
    {
        public string AccountIdentifier { get; set; }
        public string TemplateName { get; set; }
        public Dictionary<string, string> DynamicElements { get; set; }
    }
}
