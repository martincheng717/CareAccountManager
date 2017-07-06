using System.Diagnostics.CodeAnalysis;

namespace Gdot.Care.Common.Model
{
    [ExcludeFromCodeCoverage]
    public class PartnerAuthenticationCredentialInfo
    {
        public int PartnerAuthenticationKey { get; set; }
        public string Login { get; set; }
        public string EncryptedPassword { get; set; }
        public string PlainTextPassword { get; set; }
        public string Domain { get; set; }
    }
}
