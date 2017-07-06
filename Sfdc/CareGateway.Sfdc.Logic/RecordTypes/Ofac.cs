using System.Diagnostics.CodeAnalysis;
using CareGateway.Sfdc.Model;

namespace CareGateway.Sfdc.Logic.RecordTypes
{
    public class Ofac : IRecordType
    {
        [ExcludeFromCodeCoverage]
        public void Execute(CaseEx request)
        {
            if (!string.IsNullOrWhiteSpace(request.SSN__c))
            {
                request.Masked_SSN2__c = "***-**-" + request.SSN__c.Substring(request.SSN__c.Length - 4, 4);
            }
        }
    }
}
