using System.Diagnostics.CodeAnalysis;

namespace CareGateway.Sfdc.Model
{
    [ExcludeFromCodeCoverage]
    public class CaseResponse
    {
        public string Id;
        public bool Success;
        public object Errors;
        public string CaseNumber { get; set; }
    }
}
