using CareGateway.External.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareGateway.External.Model.Response
{
    public class GetAllTransTypeResponse : ResponseBase
    {
        public List<TransType> listTransType { get; set; }
    }
}
