using System.Diagnostics.CodeAnalysis;

namespace Gdot.Care.Common.Model
{
    [ExcludeFromCodeCoverage]
    public class GetClientAuthenticationSaltInput
    {
        public int ClientAuthenticationPartnerKey { get; set; }
        public string UserName { get; set; }
    }
}
