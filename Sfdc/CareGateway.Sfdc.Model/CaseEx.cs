using CareGateway.Sfdc.Model.Salesforce;
using System.Diagnostics.CodeAnalysis;

namespace CareGateway.Sfdc.Model
{
    [ExcludeFromCodeCoverage]
    public class CaseEx : Case
    {
        public string RecordType { get; set; }
        public string GroupName { get; set; }
        public string CaseOwner { get; set; }
        public string AccountIdentifier { get; set; }
    }
}
