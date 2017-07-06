using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareGateway.Sfdc.Logic.CaseClientProxy
{
    [ExcludeFromCodeCoverage]
    public class ProxyDescribeGlobalResult<T>
    {
        public string Encoding { get; set; }
        
        public int MaxBatchSize { get; set; }
        
        public List<T> SObjects { get; set; }
    }
}
