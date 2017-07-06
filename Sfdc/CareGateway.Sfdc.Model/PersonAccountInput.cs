using System.Diagnostics.CodeAnalysis;

namespace CareGateway.Sfdc.Model
{
    [ExcludeFromCodeCoverage]
    public class PersonAccountInput
    {
        public string AccountIdentifier { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Product { get; set; }
        public string Brand { get; set; }
    }
}
