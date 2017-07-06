using System.Diagnostics.CodeAnalysis;

namespace Gdot.Care.Common.Model
{
    [ExcludeFromCodeCoverage]
    public class ClientAuthenticationValidationInput
    {
        public string UserName { get; set; }
        public string HashPassword { get; set; }
        public int ClientAuthenticationPartnerKey { get; set; }

    }
}
