using System.ComponentModel;
namespace Gdot.Care.Common.Enum
{
    public enum PartnerAuthenticationKeyEnum
    {
        None = 0,
        [Description("Disbursement")]
        Disbursement = 51,
        [Description("Salesforce")]
        Salesforce = 52
    }
}
