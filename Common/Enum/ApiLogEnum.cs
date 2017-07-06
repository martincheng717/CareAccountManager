using System;

namespace Gdot.Care.Common.Enum
{
    [Flags]
    public enum ApiLogOption
    {
        None=0,
        LogRequest,
        LogResponse
        
    }
}
