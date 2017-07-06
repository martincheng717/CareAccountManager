using System.Diagnostics.CodeAnalysis;

namespace Gdot.Care.Common.Model
{
    [ExcludeFromCodeCoverage]
    public class ErrorMapInfo
    {
        public string ExternalResponseCode { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorName { get; set; }
    }
}
