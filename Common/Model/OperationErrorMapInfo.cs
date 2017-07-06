using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Gdot.Care.Common.Model
{
    [ExcludeFromCodeCoverage]
    public class OperationErrorMapInfo
    {
        public string EventType { get; set; }
        public List<ErrorMapInfo> Maps { get; set; }
    }
}
