using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareGateway.External.Model.Request
{
    public class SendEmailRequest
    {
        public string AccountIdentifier { get; set; }
        public string TemplateName { get; set; }
        public Dictionary<string, string> DynamicElements { get; set; }
    }
}
