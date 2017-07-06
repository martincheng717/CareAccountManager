using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CareGateway.External.Model.Enum
{
    public enum Status
    {
        Unknown = 0,
        Pending = 1,
        Success = 2,
        Failure = 3,
    }
}
