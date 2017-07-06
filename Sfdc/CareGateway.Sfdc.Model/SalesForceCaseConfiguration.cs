using System.Diagnostics.CodeAnalysis;

namespace CareGateway.Sfdc.Model
{
    [ExcludeFromCodeCoverage]
    // ReSharper disable once InconsistentNaming
    public class SalesForceCaseConfiguration
    {
        public string RecordType { get; set; }
        public string CaseSettingPath { get; set; }
        public string UpdateCaseSettingPath { get; set; }
    }
}
