using CareGateway.Db.PartnerAuthentication.Model.Enum;
using System.Diagnostics.CodeAnalysis;

namespace CareGateway.Db.PartnerAuthentication.Model
{
    [ExcludeFromCodeCoverage]
    public class GetPartnerAuthenticationInput
    {
        public PartnerAuthenticationEnum PartnerAuthenticationKey { get; set; }
    }
}
