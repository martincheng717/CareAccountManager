using System.ComponentModel;

namespace CareGateway.Sfdc.Model.Enum
{
    public enum SalesforceEventTypeEnum
    {
        [Description("CreateCase")]
        CreateCase,
        [Description("CreateCase_Field_Validation")]
        CreateCaseFieldValidation,
        [Description("CreateCase_RecordType_Validation")]
        CreateCaseRecordTypeValidation,
        [Description("CreateCase_AllowToCreateNewCase_Validation")]
        CreateCaseAllowToCreateNewCaseValidation,
        [Description("CreateCaseComments")]
        CreateCaseComments,
        [Description("ForceClient_Authentication")]
        ForceClientAuthentication,
        [Description("CreateCustomer")]
        CreateCustomer,
        [Description("UpdateCase")]
        UpdateCase,
        [Description("CreatePersonAccount")]
        CreatePersonAccount,
    }
}
