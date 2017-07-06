using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareGateway.External.Model.Data;

namespace CareGateway.External.Model.Response
{
    public class CloseAccountResponse:ResponseBase
    {
        public Account Account { get; set; }
    }
}
