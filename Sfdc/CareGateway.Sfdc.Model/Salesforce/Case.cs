using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Gdot.Care.Common.Utilities;
using Newtonsoft.Json;
using Salesforce.Common.Attributes;

namespace CareGateway.Sfdc.Model.Salesforce
{
    [ExcludeFromCodeCoverage]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class Case
    {
        [Key]
        [Createable(false), Updateable(false)]
        public string Id { get; set; }

        [Createable(false), Updateable(false)]
        public bool? IsDeleted { get; set; }

        [StringLength(30)]
        [Createable(false), Updateable(false)]
        public string CaseNumber { get; set; }

        public string ContactId { get; set; }

        public string AccountId { get; set; }

        public string ParentId { get; set; }

        [StringLength(80)]
        public string SuppliedName { get; set; }

        [StringLength(40)]
        public string SuppliedPhone { get; set; }

        [StringLength(80)]
        public string SuppliedCompany { get; set; }

        public string Type { get; set; }

        public string RecordTypeId { get; set; }

        public string Status { get; set; }

        public string Reason { get; set; }

        [StringLength(255)]
        public string Subject { get; set; }

        [StringLength(32000)]
        public string Description { get; set; }
        [Createable(false), Updateable(false)]
        public bool? IsClosed { get; set; }

        [Createable(false), Updateable(false)]
        public DateTimeOffset? ClosedDate { get; set; }

        public bool? IsEscalated { get; set; }

        public string OwnerId { get; set; }

        [Createable(false), Updateable(false)]
        public DateTimeOffset? CreatedDate { get; set; }

        [Createable(false), Updateable(false)]
        public string CreatedById { get; set; }

        [Createable(false), Updateable(false)]
        public DateTimeOffset? LastModifiedDate { get; set; }

        [Createable(false), Updateable(false)]
        public string LastModifiedById { get; set; }

        [Createable(false), Updateable(false)]
        public DateTimeOffset? SystemModstamp { get; set; }

        [Createable(false), Updateable(false)]
        public DateTimeOffset? LastViewedDate { get; set; }

        [Createable(false), Updateable(false)]
        public DateTimeOffset? LastReferencedDate { get; set; }

        [StringLength(255)]
        public string Account_External_Id__c { get; set; }

        [StringLength(255)]
        public string Activation_Reload_PIN__c { get; set; }

        public string Activation_Representative__c { get; set; }

        [StringLength(255)]
        public string CVS_Authorization_Transaction_Number__c { get; set; }

        [StringLength(1300)]
        [Createable(false), Updateable(false)]
        public string Card_Reference_Number__c { get; set; }

        [StringLength(255)]
        public string City__c { get; set; }

        [StringLength(175)]
        [EmailAddress]
        [JsonConverter(typeof(EmptyToNullConverter))]
        public string Customer_Email__c { get; set; }

        [StringLength(175)]
        public string Customer_Home_Phone__c { get; set; }

        public string Customer_Language__c { get; set; }

        [StringLength(175)]
        public string Customer_Mobile_Phone__c { get; set; }

        [StringLength(175)]
        public string Customer_Name__c { get; set; }

        public string DOB2__c { get; set; }

        public DateTimeOffset? DOB__c { get; set; }

        public string Destination_Account_Type__c { get; set; }

        [StringLength(16)]
        public string Destination_Card_Reference__c { get; set; }

        [StringLength(1300)]
        [Createable(false), Updateable(false)]
        public string Email__c { get; set; }
        public string Error_Log__c { get; set; }

        public string Internal_Notes__c { get; set; }

        public string Mailing_Address__c { get; set; }

        [StringLength(175)]
        public string Mailing_City__c { get; set; }

        [StringLength(175)]
        public string Mailing_Street__c { get; set; }

        [StringLength(175)]
        public string Mailing_Zip__c { get; set; }

        [StringLength(11)]
        public string Masked_SSn__c { get; set; }

        [Phone]
        public string Mobile_Phone2__c { get; set; }

        [StringLength(1300)]
        [Createable(false), Updateable(false)]
        public string Mobile_Phone__c { get; set; }

        public string OFAC_Notes__c { get; set; }

        [StringLength(255)]
        public string OFAC_Status_Description__c { get; set; }

        [StringLength(255)]
        public string OFAC_Status__c { get; set; }

        public string Originating_Call_Center__c { get; set; }

        public string Outbound_Call_Status__c { get; set; }

        public string Outcome_Resolution_RPO__c { get; set; }


        public double? Amount_Refunded__c { get; set; }
        public string PIN_Key__c { get; set; }

        public string PIN_Serial_Number2__c { get; set; }

        [StringLength(255)]
        public string PIN_Serial_Number__c { get; set; }

        public double? PIN_Value__c { get; set; }

        public string Payment_Partner__c { get; set; }

        [StringLength(1300)]
        [Createable(false), Updateable(false)]
        public string Phone__c { get; set; }


        [StringLength(20)]
        public string Created_New_Case__c { get; set; }

        [StringLength(50)]
        public string Duplicate_Case__c { get; set; }

        public string Product_Type__c { get; set; }

        public double? Receipt_Amount__c { get; set; }

        public string Receipt_Notes2__c { get; set; }

        public string Receipt_Notes__c { get; set; }

        [StringLength(255)]
        public string Reference_Number__c { get; set; }

        [StringLength(255)]
        public string Registration_Token__c { get; set; }

        public string Residential_Address__c { get; set; }

        public string Residential_City__c { get; set; }

        public string Residential_Street__c { get; set; }

        public string Residential_Zip__c { get; set; }

        public string Resolution__c { get; set; }

        public string Retailer__c { get; set; }

        [StringLength(50)]
        public string RiteAid_Transaction_Number__c { get; set; }

        public double? SSN_Was_Unmasked__c { get; set; }

        [StringLength(11)]
        public string SSN__c { get; set; }

        [StringLength(255)]
        public string State__c { get; set; }

        [StringLength(255)]
        public string Store_Number__c { get; set; }

        [Phone]
        public string Store_Phone_Number__c { get; set; }

        [StringLength(255)]
        public string Street__c { get; set; }

        public string TEST_ENCRYPTED__c { get; set; }

        public string TEST_ENCRYPTED_del__c { get; set; }

        public DateTimeOffset? Transaction_Date__c { get; set; }

        public string Transaction_Information_Located__c { get; set; }

        [StringLength(255)]
        public string Transaction_Time__c { get; set; }

        public string Type__c { get; set; }

        [StringLength(255)]
        public string Zip__c { get; set; }

        public string Activation_Reload_PIN2__c { get; set; }

        public string Activation_Reload_PIN_old__c { get; set; }

        [StringLength(175)]
        public string Mailing_Address2__c { get; set; }

        [StringLength(175)]
        public string Mailing_State__c { get; set; }

        public string Masked_SSN2__c { get; set; }

        public string PIN_Number__c { get; set; }

        public string Residential_Address2__c { get; set; }

        public string Residential_State__c { get; set; }

        public string SSN2__c { get; set; }

        [StringLength(80)]
        public string Product_Bank__c { get; set; }

        [Display(Name = "Destination GDN Customer Key")]
        [StringLength(25)]
        public string Destination_GDN_Customer_Key__c { get; set; }
        public string Reload_Type__c { get; set; }

        public string Sub_Type__c { get; set; }

        public string ACH_DDReversal__c { get; set; }


        [StringLength(20)]
        public string KCRC_Blackhawk_Case__c { get; set; }


        public string CH_Disputes_Error_Reason__c { get; set; }

        public DateTimeOffset? Card_Reported_Date__c { get; set; }


        public DateTimeOffset? Completed_Date__c { get; set; }

        [StringLength(100)]
        public string Current_Card_Status__c { get; set; }

        [StringLength(25)]
        public string Customer_Key__c { get; set; }


        public string Declined_Reason__c { get; set; }

        public double? DisputeAmount__c { get; set; }

        [StringLength(255)]
        public string DisputeMerchantName__c { get; set; }


        public string Dispute_Reasons__c { get; set; }

        public string Dispute_Resolution__c { get; set; }


        public string Flagged_by_Monitoring__c { get; set; }

        public string Int_l_Domestic__c { get; set; }


        [StringLength(50)]
        public string MCC__c { get; set; }

        public string MOTO6_CAT6__c { get; set; }

        public DateTimeOffset? Notification_Received_Date__c { get; set; }

        public string OOW_Result__c { get; set; }

        public double? Partial_Credit_Amount__c { get; set; }


        [StringLength(50)]
        public string Pega_Dispute_ID__c { get; set; }

        public string Pending_Transaction__c { get; set; }

        public DateTimeOffset? Postmarked_Date__c { get; set; }


        public string Processing_Status__c { get; set; }


        public DateTimeOffset? Provisional_Credit_Date__c { get; set; }

        public double? Provisional_Credit__c { get; set; }

        public DateTimeOffset? SLA_Due_Date__c { get; set; }

        public string TransRef__c { get; set; }

        public string Transfer_Source__c { get; set; }

        public string Written_Notification_Type__c { get; set; }

        public double? Disputed_Transaction_Amount__c { get; set; }
        public double? Disputed_Transaction_Child_Case_Count__c { get; set; }


        [StringLength(50)]
        public string Pega_Case_ID__c { get; set; }
        public string Provisional_Credit_Status__c { get; set; }

        public string Action_Taken_Chargeback__c { get; set; }

        public string Action_Taken_Representment__c { get; set; }

        public string CB_Reason_Code_Replacement__c { get; set; }


        public string CH_Association__c { get; set; }

        public string Chargeback_Driver_Representment__c { get; set; }

        public string Chargeback_Issue_Status__c { get; set; }

        public string Chargeback_Reason_Code_Chargeback__c { get; set; }

        public DateTimeOffset? Date_Received_Chargeback__c { get; set; }

        public DateTimeOffset? Date_Received_Representment__c { get; set; }


        public DateTimeOffset? Progressive_Letter_Requested_Date_Repres__c { get; set; }

        public string Rep_Error_Representment__c { get; set; }

        public string Representment_Issue_Status__c { get; set; }

        public DateTimeOffset? Response_Provided_Chargeback__c { get; set; }

        public DateTimeOffset? Response_Provided_Representment__c { get; set; }

        public DateTimeOffset? SLA_Due_Date_Chargeback__c { get; set; }

        public DateTimeOffset? SLA_Due_Date_Representment__c { get; set; }

        [StringLength(15)]
        public string VROL_MC_Case_Chargeback__c { get; set; }


        public string Action_Taken_Arbitration__c { get; set; }


        public string Action_Taken_Pre_Arbitration__c { get; set; }


        public string Arbitration_Case_Decision__c { get; set; }

        public string Arbitration_Issue_Status__c { get; set; }

        public string Arbitration_Response__c { get; set; }


        public DateTimeOffset? Date_Received_Arbitration__c { get; set; }


        public DateTimeOffset? Date_Received_Pre_Arbitration__c { get; set; }

        public string Pre_Arbitration_Issue_Status__c { get; set; }

        public string Pre_Arbitration_Response__c { get; set; }

        public DateTimeOffset? Prog_Letter_Req_Date_Pre_Arbitration__c { get; set; }

        public DateTimeOffset? Progressive_Letter_Req_Date_Arbitration__c { get; set; }



        public string Rep_Error_Arbitration__c { get; set; }

        public string Rep_Error_Pre_Arbitration__c { get; set; }


        public DateTimeOffset? Response_Provided_Arbitration__c { get; set; }


        public DateTimeOffset? Response_Provided_Pre_Arbitration__c { get; set; }


        public DateTimeOffset? SLA_Due_Date_Arbitration__c { get; set; }

        public DateTimeOffset? SLA_Due_Date_Pre_Arbitration__c { get; set; }

        public string Negative_Balance_Reason__c { get; set; }


        [StringLength(100)]
        public string ACH_DD_Reversal__c { get; set; }

        public string Action_Taken__c { get; set; }


        public DateTimeOffset? Authorization_Date_Time__c { get; set; }



        public DateTimeOffset? Date_Received__c { get; set; }

        public string Error_Log_AR__c { get; set; }

        public string Error_Reason_AR__c { get; set; }



        public string Reason_For_Request__c { get; set; }


        public string Rep_Error__c { get; set; }

        public string Representative__c { get; set; }


        public string Reversal_Status__c { get; set; }

        public DateTimeOffset? Recent_SLA_Due_Date__c { get; set; }

        [StringLength(255)]
        public string Change_Name_to_First_Name__c { get; set; }

        [StringLength(255)]
        public string Change_Name_to_Last_Name__c { get; set; }

        [StringLength(255)]
        public string Change_Name_to_Middle_Initial__c { get; set; }

        public string Correspondence_Notes__c { get; set; }

        public string Correspondence_Reload_Type__c { get; set; }

        [StringLength(255)]
        public string Delivery_Address__c { get; set; }

        [StringLength(255)]
        public string Delivery_Apt_Suit__c { get; set; }

        [StringLength(255)]
        public string Delivery_City__c { get; set; }

        public string Delivery_Option__c { get; set; }

        [StringLength(255)]
        public string Delivery_State__c { get; set; }

        [StringLength(5)]
        public string Delivery_Zip_Code__c { get; set; }

        public string Fraud_Type__c { get; set; }

        public string Funding_Research_Type__c { get; set; }

        public string Name_Change_for__c { get; set; }

        public string New_Delivery_Address__c { get; set; }

        public string Reason_for_Callback__c { get; set; }

        public string Request_Channel__c { get; set; }

        public string Request_Type__c { get; set; }

        public string Statement_Date_Range_End_Month__c { get; set; }

        public string Statement_Date_Range_End_Year__c { get; set; }

        public string Statement_Date_Range_Start_Month__c { get; set; }

        public string Statement_Date_Range_Start_Year__c { get; set; }

        public double? Dispute_MoneyPak_Amount__c { get; set; }

        [StringLength(25)]
        public string Dispute_Product_Type__c { get; set; }

        public double? Disputed_MoneyPak_Child_Case_Count__c { get; set; }

        [StringLength(30)]
        public string GDN_Partner_Name__c { get; set; }

        [StringLength(30)]
        public string GDN_Partner__c { get; set; }


        [StringLength(50)]
        public string Intended_Recipient_Account_Number__c { get; set; }

        [StringLength(25)]
        public string Intended_Recipient__c { get; set; }

        public string MP_Dispute_Resolution__c { get; set; }

        public string MP_Dispute_Scenario__c { get; set; }

        [StringLength(30)]
        public string MoneyPak_Pin_Key__c { get; set; }

        [StringLength(50)]
        public string Moneypak_Retailer__c { get; set; }


        public string Recipient_Account_Action__c { get; set; }

        [StringLength(50)]
        public string Recipient_Account_Number__c { get; set; }

        [StringLength(50)]
        public string Recipient_Name__c { get; set; }

        [StringLength(30)]
        public string Recipient_Type__c { get; set; }


        public string Scam_Type__c { get; set; }


        public string Willingly_Provided__c { get; set; }

        public string MoneyPak_Dispute_Error_Reason__c { get; set; }
        public DateTimeOffset? Agency_Due_Date__c { get; set; }

        [StringLength(40)]
        public string CE_Product_Type__c { get; set; }

        public string CE_Retailer__c { get; set; }


        public string Channel__c { get; set; }

        public string Corp_Escalation_From__c { get; set; }

        public string Corp_Escalation_Type__c { get; set; }


        public DateTimeOffset? Date_Received_by_Agency__c { get; set; }

        public DateTimeOffset? Date_Received_by_GreenDot__c { get; set; }

        [StringLength(175)]
        public string Delivery_Apt_Suite__c { get; set; }

        [StringLength(175)]
        public string Delivery_Street__c { get; set; }

        [StringLength(15)]
        public string Delivery_Zip__c { get; set; }

        public string Follow_Up_Call_Back__c { get; set; }

        public string Line_of_Business__c { get; set; }

        public string Originating_Agency__c { get; set; }

        [StringLength(10)]
        public string Refund_Check_Amount__c { get; set; }

        public string Refund_Check_Research_Type__c { get; set; }


        public string Resolution_Detail__c { get; set; }

        public string Resolution_Disposition__c { get; set; }

        public string Valid_Complaint__c { get; set; }

        [StringLength(15)]
        public string Bill_Pay_Amount__c { get; set; }

        public string Blocked_Account_Research_Type__c { get; set; }

        public string Caller_Type__c { get; set; }

        public string Customer_Alternate_Phone__c { get; set; }

        public string Enhanced_Verification_Results__c { get; set; }

        public string Error_Log_Reason_BAR__c { get; set; }
        public string Funding_Research_Sub_Type__c { get; set; }
        [StringLength(125)]
        public string Payee_Name__c { get; set; }

        public string Provisional_Credit_Reason__c { get; set; }

        public string Requested_Action__c { get; set; }
        public string Review_Outcome__c { get; set; }


        public double? CreditWithoutOffsettingSale_Amount__c { get; set; }

        public double? CreditWithoutOffsettingSale_Count__c { get; set; }

        public double? Credit_Amount__c { get; set; }

        public DateTimeOffset? Credit_Posted_Date__c { get; set; }

        public string Credit_Review_Resolution__c { get; set; }

        [StringLength(3)]
        public string High_Valued_Customer_HVC__c { get; set; }

        public string Merchant_Contacted__c { get; set; }

        [Phone]
        public string Merchant_Phone_Number__c { get; set; }

        public string Rule_Number__c { get; set; }

        public double? Case_Age_In_Business_Hours__c { get; set; }

        public DateTimeOffset? Last_Status_Change__c { get; set; }

        public double? Time_With_Customer__c { get; set; }

        public double? Time_With_Support__c { get; set; }

        [StringLength(120)]
        public string Card_Reference_of_Where_DD_Posted__c { get; set; }

        public string Cardholder_Direct_Deposit_Inquiry_Type__c { get; set; }

        public DateTimeOffset? Date_DD_Expected__c { get; set; }

        public DateTimeOffset? Date_DD_Posted__c { get; set; }

        public string Inquiry_Resolution__c { get; set; }

        [StringLength(120)]
        public string Sender__c { get; set; }


        public string Call_Type__c { get; set; }

        [EmailAddress]
        [StringLength(125)]
        [JsonConverter(typeof(EmptyToNullConverter))]
        public string Caller_Email_Address__c { get; set; }

        [StringLength(125)]
        public string Caller_First_Name__c { get; set; }

        [StringLength(125)]
        public string Caller_Last_Name__c { get; set; }

        [StringLength(20)]
        public string Caller_Phone_Number__c { get; set; }


        public string Channel_Leach__c { get; set; }


        public string Error_Log_Reason_LEACH__c { get; set; }


        public string Inquiry_Type__c { get; set; }

        [StringLength(125)]
        public string Organization_Name__c { get; set; }

        public string Reference_Type__c { get; set; }



        public string Representment_Rep_Error__c { get; set; }

        public string Source_of_Request__c { get; set; }


        public double? Check_Amount__c { get; set; }

        public DateTimeOffset? Check_Date__c { get; set; }

        [StringLength(30)]
        public string Check__c { get; set; }

        public string Declined_Reason_PaperCheck__c { get; set; }

        public DateTimeOffset? Dispute_Form_Received_Date__c { get; set; }

        public string Dispute_Reason_PaperCheck__c { get; set; }

        public string Dispute_Resolution_PaperCheck__c { get; set; }

        public double? Intended_Amount__c { get; set; }

        [StringLength(30)]
        public string Intended_Payee__c { get; set; }

        public string Notification_Type__c { get; set; }

        [StringLength(30)]
        public string Payable_To__c { get; set; }

        public DateTimeOffset? Pre_Authorization_Date__c { get; set; }

        public double? Total_Amount_of_Loss__c { get; set; }

        [StringLength(10)]
        public string AccountKey__c { get; set; }

        public DateTimeOffset? CustomerDisputeStatusDate__c { get; set; }

        public string Customer_s_Dispute_Status__c { get; set; }

        [Createable(false), Updateable(false)]
        public double? DisputeStatusDays__c { get; set; }

        public bool? TriggerStatus__c { get; set; }



        [StringLength(4)]
        public string Last_4_of_Card_Number__c { get; set; }

        public string PIN_Request_Type__c { get; set; }

        public string Pin_Generated_By__c { get; set; }


        public string Action_Taken_SalesAlert__c { get; set; }

        [StringLength(120)]
        public string AlertType__c { get; set; }
        [StringLength(25)]
        public string ProductToken__c { get; set; }

        [StringLength(120)]
        public string ProductType__c { get; set; }

        [StringLength(120)]
        public string RetailerName__c { get; set; }


        [StringLength(20)]
        public string StoreNumber__c { get; set; }

        public double? TotalLoadValue__c { get; set; }

        public double? TransactionCount__c { get; set; }

        public DateTimeOffset? SLA_Due_Date_Time__c { get; set; }
        public string Type_Of_Request__c { get; set; }

        [StringLength(25)]
        public string AgencyCity__c { get; set; }

        [StringLength(25)]
        public string AgencyState__c { get; set; }

        [StringLength(25)]
        public string Agency_Name__c { get; set; }

        [StringLength(25)]
        public string Agency_Reference_Number__c { get; set; }

        [StringLength(25)]
        public string AgentName__c { get; set; }

        [StringLength(25)]
        public string AgentPhoneNumber__c { get; set; }

        [EmailAddress]
        [JsonConverter(typeof(EmptyToNullConverter))]
        public string Agent_Email_Address__c { get; set; }

        public string Authenticity_of_Records_Needed__c { get; set; }

        public string Billing_Documents_Received__c { get; set; }

        public double? Blocked_Accounts_Only__c { get; set; }

        public double? Call_Recordings__c { get; set; }

        public double? Cardholder_Account_Records__c { get; set; }

        public string Channel_Subpoena__c { get; set; }


        public string Data_Disc_Requested__c { get; set; }

        public DateTimeOffset? Date_Response_Provided__c { get; set; }

        public DateTimeOffset? First_Extension_Date__c { get; set; }


        public double? GDN_Account_Records__c { get; set; }


        public string Incoming_Review_Decision__c { get; set; }

        public DateTimeOffset? Internal_Due_Date__c { get; set; }


        public string Issuing_Bank__c { get; set; }

        public double? MoneyPak_Records__c { get; set; }

        public double? Non_Responsive_Records__c { get; set; }

        public double? Number_of_Records_Requested__c { get; set; }


        public double? Other_Records_see_notes__c { get; set; }

        public string Properly_Served__c { get; set; }

        public string Provided_Response_Via__c { get; set; }

        public string Query_Needed__c { get; set; }

        public DateTimeOffset? Second_Extension_Date__c { get; set; }

        [StringLength(25)]
        public string Shipping_Tracking_Number__c { get; set; }

        public string Time_Frame_of_Card_Records__c { get; set; }

        public string TypeOf_Request__c { get; set; }

        [StringLength(255)]
        public string ACH_Batch_Key__c { get; set; }

        [StringLength(255)]
        public string ACH_Return_Trace__c { get; set; }

        public double? Amount_Available__c { get; set; }

        public double? Amount_Requested__c { get; set; }

        public double? Amount_Returned__c { get; set; }
        [StringLength(255)]
        public string Check_Request__c { get; set; }

        public string Duplicate_Request__c { get; set; }
        public string Funds_Available__c { get; set; }

        public string Funds_Provided_via__c { get; set; }

        public string Funds_Type__c { get; set; }

        public string Incoming_Review_Decision_Request__c { get; set; }

        public string Issuing_Bank_Request__c { get; set; }

        [StringLength(25)]
        public string Organization_City__c { get; set; }

        public DateTimeOffset? Organization_Due_Date__c { get; set; }

        [StringLength(25)]
        public string Organization_State__c { get; set; }


        [EmailAddress]
        [StringLength(50)]
        [JsonConverter(typeof(EmptyToNullConverter))]
        public string Requestor_Email_Address__c { get; set; }

        [StringLength(25)]
        public string Requestor_Name__c { get; set; }

        [Phone]
        public string Requestor_Phone_Number__c { get; set; }

        [StringLength(120)]
        public string Requestor_Reference_Number__c { get; set; }

        public double? Total_Card_Reference_Numbers__c { get; set; }

        public double? Total_GDN_Customer_Key_Numbers__c { get; set; }

        public double? Total_PIN_Key_Numbers__c { get; set; }


        public string Type_of_Request_Request__c { get; set; }


        public string Duplicate_Request_Parent__c { get; set; }


        public string Action_Taken_FraudMailbox__c { get; set; }

        public string Email_Contents_Fraud__c { get; set; }

        [StringLength(255)]
        public string Email_Contents__c { get; set; }

        public string Email_Subject_Fraud__c { get; set; }

        [StringLength(255)]
        public string Email_Subject__c { get; set; }

        public string Error_Type__c { get; set; }
        [StringLength(255)]
        public string Sent_CC__c { get; set; }

        [StringLength(255)]
        public string Sent_From__c { get; set; }

        [StringLength(255)]
        public string Sent_To__c { get; set; }

        public DateTimeOffset? Expiration_date_time__c { get; set; }

        [StringLength(50)]
        public string Register_ID__c { get; set; }

        [StringLength(50)]
        public string Register_Type__c { get; set; }

        public double? Sales_Amount__c { get; set; }


        [StringLength(50)]
        public string Store_ID_Number__c { get; set; }

        public string Transaction_Detail_That_Triggered_Block__c { get; set; }

        [StringLength(50)]
        public string Transaction_Type_to_Be_Blocked__c { get; set; }

        [StringLength(50)]
        public string Transaction_Type_to_Trigger_Block__c { get; set; }


        [EmailAddress]
        [JsonConverter(typeof(EmptyToNullConverter))]
        public string Sent_CC_Email__c { get; set; }

        [EmailAddress]
        [JsonConverter(typeof(EmptyToNullConverter))]
        public string Sent_From_Email__c { get; set; }

        [EmailAddress]
        [JsonConverter(typeof(EmptyToNullConverter))]
        public string Sent_To_Email__c { get; set; }

        public string Second_Cardholder_Name__c { get; set; }
        public string SentFromCC__c { get; set; }

        public string SentTo__c { get; set; }

        public string Customer_Question__c { get; set; }

        [EmailAddress]
        [JsonConverter(typeof(EmptyToNullConverter))]
        public string Email_Address__c { get; set; }

        [StringLength(255)]
        public string First_Name__c { get; set; }

        public string General_Topic__c { get; set; }

        [StringLength(255)]
        public string Last_Name__c { get; set; }

        [Phone]
        public string Phone_Number__c { get; set; }

        public string Walmart_MoneyCard_Holder__c { get; set; }

        [StringLength(255)]
        public string Address2CdDomestic__c { get; set; }

        [StringLength(255)]
        public string Address2CdInter__c { get; set; }

        [StringLength(255)]
        public string Address2CrDomestic__c { get; set; }

        [StringLength(255)]
        public string Address2CrInter__c { get; set; }

        [StringLength(255)]
        public string Authorization_Code__c { get; set; }

        public double? Available_Balance__c { get; set; }

        public DateTimeOffset? Available_Balance_as_of__c { get; set; }

        [StringLength(255)]
        public string CdAddressInter__c { get; set; }

        public bool? CdAddress_Validated__c { get; set; }

        [StringLength(255)]
        public string CityCdDomestic__c { get; set; }

        [StringLength(255)]
        public string CityCrDomestic__c { get; set; }

        [StringLength(255)]
        public string CityInterCd__c { get; set; }

        [StringLength(255)]
        public string CityInterCr__c { get; set; }

        [StringLength(255)]
        public string CompanyEcd__c { get; set; }

        [StringLength(255)]
        public string CompanyEcr__c { get; set; }

        [StringLength(255)]
        public string CountryCd__c { get; set; }

        [StringLength(255)]
        public string CountryCr__c { get; set; }

        [StringLength(255)]
        public string CrAddressInter__c { get; set; }

        public bool? CrAddress_Validated__c { get; set; }

        public DateTimeOffset? Date_Issued__c { get; set; }

        public DateTimeOffset? Date_of_Departure__c { get; set; }

        [StringLength(255)]
        public string DomesticAddressCR__c { get; set; }

        [StringLength(255)]
        public string DomesticAddressCd__c { get; set; }

        public double? ECD_Amount__c { get; set; }

        public double? ECR_Amount__c { get; set; }

        public string Identification_in_Possession__c { get; set; }

        [StringLength(4)]
        public string Last_4_of_STIP__c { get; set; }

        [StringLength(255)]
        public string Name_to_print_on_plastic__c { get; set; }

        [StringLength(255)]
        public string Number_on_ID__c { get; set; }

        [StringLength(255)]
        public string Place_Issued__c { get; set; }

        [StringLength(255)]
        public string Place_of_Birth__c { get; set; }

        [StringLength(255)]
        public string RecipientNameCd__c { get; set; }

        [StringLength(255)]
        public string STIP_Card_Reference_Number__c { get; set; }

        public DateTimeOffset? STIP_Expiration_Date__c { get; set; }

        public string StateCdDomestic__c { get; set; }

        public string StateCrDomestic__c { get; set; }

        [StringLength(150)]
        public string StateInterCd__c { get; set; }

        [StringLength(255)]
        public string StateInterCr__c { get; set; }

        [Phone]
        public string Temporary_Phone_Number__c { get; set; }

        public string Type_of_ID__c { get; set; }

        [StringLength(255)]
        public string Visa_Reference_Number__c { get; set; }

        [StringLength(255)]
        public string ZipCdDomestic__c { get; set; }

        [StringLength(255)]
        public string ZipCdInter__c { get; set; }

        [StringLength(255)]
        public string ZipCrDomestic__c { get; set; }

        [StringLength(255)]
        public string ZipCrInter__c { get; set; }

        [StringLength(255)]
        public string ProfileID__c { get; set; }

        public string Agency__c { get; set; }

        public string Agent_Coaching_Performed__c { get; set; }

        public string Bank__c { get; set; }

        [StringLength(50)]
        public string CE_Ticket_Number__c { get; set; }

        public string Coaching_Action__c { get; set; }

        [StringLength(255)]
        public string Corporate_Resolution_Contact__c { get; set; }

        public DateTimeOffset? Date_Sent_to_Compliance__c { get; set; }

        public string Disposition__c { get; set; }

        [StringLength(16)]
        public string GDFN_Reference__c { get; set; }

        public double? No_of_times_cust_called_for_same_issue__c { get; set; }

        public string Resolution_Letter__c { get; set; }

        [StringLength(255)]
        public string Written_By__c { get; set; }

        [StringLength(30)]
        public string PIN_Reference__c { get; set; }

        [StringLength(255)]
        public string Account_Number__c { get; set; }



        [EmailAddress]
        [JsonConverter(typeof(EmptyToNullConverter))]
        public string Business_Email__c { get; set; }
        [StringLength(255)]
        public string Business_Name__c { get; set; }

        [Phone]
        public string Business_Phone__c { get; set; }

        [EmailAddress]
        [StringLength(255)]
        [JsonConverter(typeof(EmptyToNullConverter))]
        public string CustomerEmail__c { get; set; }

        [StringLength(255)]
        public string CustomerName__c { get; set; }
        [StringLength(255)]
        public string CustomerPhone__c { get; set; }


        // Add New field after 2016/7/12

        [StringLength(255)]
        public string Dispute_Category__c { get; set; }


        public double? Partial_Recovery__c { get; set; }

        [StringLength(255)]
        public string GDN_Customer_Key__c { get; set; }


        public string NetworkCustomer__c { get; set; }

        public string Resolution_Correspondence__c { get; set; }


        public string Customer__c { get; set; }


        [Phone]
        public string CustHomePhone__c { get; set; }


        [Phone]
        public string CustMobilePhone__c { get; set; }

        [StringLength(255)]
        public string MailingAptSuite__c { get; set; }

        public string Channel_Disposition__c { get; set; }

        public string Disposition_Sub_Type__c { get; set; }

        public string Disposition_Type__c { get; set; }

        public string Issue__c { get; set; }

        public string Application__c { get; set; }

        public DateTimeOffset? Date_Time_of_Issue__c { get; set; }

        public string Summary_of_the_Issue__c { get; set; }

        public string Detailed_steps_to_recreate_the_issue__c { get; set; }

        public string Internal_Instructions__c { get; set; }

        [StringLength(25)]
        public string Jira_Ticket__c { get; set; }

        public string Compliance_or_Legal_Impact__c { get; set; }

        [StringLength(25)]
        public string Agent_Computer_ID__c { get; set; }

        public string Type_of_Issue__c { get; set; }

        public string Behavior_Triggered_Complaint__c { get; set; }

        public string Agent_Name__c { get; set; }

        public string Agent_Location__c { get; set; }

        [StringLength(64)]
        public string SessionId__c { get; set; }

        public string Issue_Sub_Type__c { get; set; }

        [Display(Name = "Brand")]
        [StringLength(50)]
        public string Brand_OFAC__c { get; set; }

    }
}
