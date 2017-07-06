using System.Diagnostics.CodeAnalysis;

namespace CareGateway.Sfdc.Model.Authentication
{
    [ExcludeFromCodeCoverage]
    public class SalesforceAuthentication
    {
        public string Login { get; set; }
        public string Domain { get; set; }
        public string ConsumerKey { get; set; }
        public string ConsumerSecret { get; set; }
        public string Password { get; set; }
    }
}
