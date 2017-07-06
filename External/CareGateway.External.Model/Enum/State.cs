using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CareGateway.External.Model.Enum
{
    public enum State
    {
        NotActivated = 1,
        Active = 2,
        Restricted = 3,
        Closed = 4,
        Locked = 5
    }
}
