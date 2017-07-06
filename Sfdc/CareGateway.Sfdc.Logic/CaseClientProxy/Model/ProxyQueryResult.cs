using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareGateway.Sfdc.Logic.CaseClientProxy
{
    [ExcludeFromCodeCoverage]
    public class ProxyQueryResult<T>
    {
 
        public bool Done { get; set; }
       
        public string NextRecordsUrl { get; set; }
        
        public List<T> Records { get; set; }
        
        public int TotalSize { get; set; }
    }
}
