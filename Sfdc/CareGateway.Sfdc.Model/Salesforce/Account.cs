using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Salesforce.Common.Attributes;

namespace CareGateway.Sfdc.Model.Salesforce
{
    [ExcludeFromCodeCoverage]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class Account
    {
        [Key]
        [Display(Name = "Account ID")]
        [Createable(false), Updateable(false)]
        public string Id { get; set; }

        [Display(Name = "Deleted")]
        [Createable(false), Updateable(false)]
        public bool IsDeleted { get; set; }

        [Display(Name = "Master Record ID")]
        [Createable(false), Updateable(false)]
        public string MasterRecordId { get; set; }

        [Display(Name = "Account Name")]
        [StringLength(255)]
        public string Name { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(80)]
        public string LastName { get; set; }

        [Display(Name = "First Name")]
        [StringLength(40)]
        public string FirstName { get; set; }

        public string Salutation { get; set; }

        [Display(Name = "Account Type")]
        public string Type { get; set; }

        [Display(Name = "Record Type ID")]
        public string RecordTypeId { get; set; }

        [Display(Name = "Parent Account ID")]
        public string ParentId { get; set; }

        [Display(Name = "Billing Street")]
        public string BillingStreet { get; set; }

        [Display(Name = "Billing City")]
        [StringLength(40)]
        public string BillingCity { get; set; }

        [Display(Name = "Billing State/Province")]
        [StringLength(80)]
        public string BillingState { get; set; }

        [Display(Name = "Billing Zip/Postal Code")]
        [StringLength(20)]
        public string BillingPostalCode { get; set; }

        [Display(Name = "Billing Country")]
        [StringLength(80)]
        public string BillingCountry { get; set; }

        [Display(Name = "Billing Latitude")]
        public double? BillingLatitude { get; set; }

        [Display(Name = "Billing Longitude")]
        public double? BillingLongitude { get; set; }

        [Display(Name = "Shipping Street")]
        public string ShippingStreet { get; set; }

        [Display(Name = "Shipping City")]
        [StringLength(40)]
        public string ShippingCity { get; set; }

        [Display(Name = "Shipping State/Province")]
        [StringLength(80)]
        public string ShippingState { get; set; }

        [Display(Name = "Shipping Zip/Postal Code")]
        [StringLength(20)]
        public string ShippingPostalCode { get; set; }

        [Display(Name = "Shipping Country")]
        [StringLength(80)]
        public string ShippingCountry { get; set; }

        [Display(Name = "Shipping Latitude")]
        public double? ShippingLatitude { get; set; }

        [Display(Name = "Shipping Longitude")]
        public double? ShippingLongitude { get; set; }

        [Display(Name = "Account Phone")]
        [Phone]
        public string Phone { get; set; }

        [Display(Name = "Account Fax")]
        [Phone]
        public string Fax { get; set; }

        [Url]
        public string Website { get; set; }

        [Display(Name = "Photo URL")]
        [Url]
        [Createable(false), Updateable(false)]
        public string PhotoUrl { get; set; }

        public string Industry { get; set; }

        [Display(Name = "Annual Revenue")]
        public double? AnnualRevenue { get; set; }

        [Display(Name = "Employees")]
        public int? NumberOfEmployees { get; set; }

        [Display(Name = "Account Description")]
        public string Description { get; set; }

        [Display(Name = "Owner ID")]
        [Updateable(false)]
        public string OwnerId { get; set; }

        [Display(Name = "Created Date")]
        [Createable(false), Updateable(false)]
        public DateTimeOffset CreatedDate { get; set; }

        [Display(Name = "Created By ID")]
        [Createable(false), Updateable(false)]
        public string CreatedById { get; set; }

        [Display(Name = "Last Modified Date")]
        [Createable(false), Updateable(false)]
        public DateTimeOffset LastModifiedDate { get; set; }

        [Display(Name = "Last Modified By ID")]
        [Createable(false), Updateable(false)]
        public string LastModifiedById { get; set; }

        [Display(Name = "System Modstamp")]
        [Createable(false), Updateable(false)]
        public DateTimeOffset SystemModstamp { get; set; }

        [Display(Name = "Last Activity")]
        [Createable(false), Updateable(false)]
        public DateTimeOffset? LastActivityDate { get; set; }

        [Display(Name = "Last Viewed Date")]
        [Createable(false), Updateable(false)]
        public DateTimeOffset? LastViewedDate { get; set; }

        [Display(Name = "Last Referenced Date")]
        [Createable(false), Updateable(false)]
        public DateTimeOffset? LastReferencedDate { get; set; }

        [Display(Name = "Contact ID")]
        [Createable(false), Updateable(false)]
        public string PersonContactId { get; set; }

        [Display(Name = "Is Person Account")]
        [Createable(false), Updateable(false)]
        public bool IsPersonAccount { get; set; }

        [Display(Name = "Mailing Street")]
        public string PersonMailingStreet { get; set; }

        [Display(Name = "Mailing City")]
        [StringLength(40)]
        public string PersonMailingCity { get; set; }

        [Display(Name = "Mailing State/Province")]
        [StringLength(80)]
        public string PersonMailingState { get; set; }

        [Display(Name = "Mailing Zip/Postal Code")]
        [StringLength(20)]
        public string PersonMailingPostalCode { get; set; }

        [Display(Name = "Mailing Country")]
        [StringLength(80)]
        public string PersonMailingCountry { get; set; }

        [Display(Name = "Mailing Latitude")]
        public double? PersonMailingLatitude { get; set; }

        [Display(Name = "Mailing Longitude")]
        public double? PersonMailingLongitude { get; set; }

        [Display(Name = "Other Street")]
        public string PersonOtherStreet { get; set; }

        [Display(Name = "Other City")]
        [StringLength(40)]
        public string PersonOtherCity { get; set; }

        [Display(Name = "Other State/Province")]
        [StringLength(80)]
        public string PersonOtherState { get; set; }

        [Display(Name = "Other Zip/Postal Code")]
        [StringLength(20)]
        public string PersonOtherPostalCode { get; set; }

        [Display(Name = "Other Country")]
        [StringLength(80)]
        public string PersonOtherCountry { get; set; }

        [Display(Name = "Other Latitude")]
        public double? PersonOtherLatitude { get; set; }

        [Display(Name = "Other Longitude")]
        public double? PersonOtherLongitude { get; set; }

        [Display(Name = "Mobile")]
        [Phone]
        public string PersonMobilePhone { get; set; }

        [Display(Name = "Home Phone")]
        [Phone]
        public string PersonHomePhone { get; set; }

        [Display(Name = "Other Phone")]
        [Phone]
        public string PersonOtherPhone { get; set; }

        [Display(Name = "Asst. Phone")]
        [Phone]
        public string PersonAssistantPhone { get; set; }

        [Display(Name = "Email")]
        [EmailAddress]
        public string PersonEmail { get; set; }

        [Display(Name = "Title")]
        [StringLength(80)]
        public string PersonTitle { get; set; }

        [Display(Name = "Department")]
        [StringLength(80)]
        public string PersonDepartment { get; set; }

        [Display(Name = "Assistant")]
        [StringLength(40)]
        public string PersonAssistantName { get; set; }

        [Display(Name = "Lead Source")]
        public string PersonLeadSource { get; set; }

        [Display(Name = "Birthdate")]
        public DateTimeOffset? PersonBirthdate { get; set; }

        [Display(Name = "Last Stay-in-Touch Request Date")]
        [Createable(false), Updateable(false)]
        public DateTimeOffset? PersonLastCURequestDate { get; set; }

        [Display(Name = "Last Stay-in-Touch Save Date")]
        [Createable(false), Updateable(false)]
        public DateTimeOffset? PersonLastCUUpdateDate { get; set; }

        [Display(Name = "Email Bounced Reason")]
        [StringLength(255)]
        public string PersonEmailBouncedReason { get; set; }

        [Display(Name = "Email Bounced Date")]
        public DateTimeOffset? PersonEmailBouncedDate { get; set; }

        [Display(Name = "Data.com Key")]
        [StringLength(20)]
        public string Jigsaw { get; set; }

        [Display(Name = "Jigsaw Company ID")]
        [StringLength(20)]
        [Createable(false), Updateable(false)]
        public string JigsawCompanyId { get; set; }

        [Display(Name = "Account Source")]
        public string AccountSource { get; set; }

        [Display(Name = "SIC Description")]
        [StringLength(80)]
        public string SicDesc { get; set; }

        [Display(Name = "Activations Viewstate")]
        public string Activations_Viewstate__c { get; set; }

        [Display(Name = "Card Reference Number")]
        [StringLength(40)]
        public string Card_Reference_Number__c { get; set; }

        [Display(Name = "Customer Key")]
        [StringLength(255)]
        public string Customer_Key__c { get; set; }

        [Display(Name = "Email")]
        [EmailAddress]
        public string Email__c { get; set; }

        [Display(Name = "Flow Path")]
        [StringLength(30)]
        public string Flow_Path__c { get; set; }

        [Display(Name = "Mobile Phone")]
        [Phone]
        public string Mobile_Phone__c { get; set; }

        [Display(Name = "Refund Reload Viewstate")]
        public string Refund_Reload_Viewstate__c { get; set; }

        [Display(Name = "View State")]
        public string View_State__c { get; set; }

        [Display(Name = "Registration Token")]
        [StringLength(20)]
        public string Registration_Token__c { get; set; }

        [Display(Name = "Account Key")]
        [StringLength(50)]
        public string Account_Key__c { get; set; }

        [Display(Name = "Account Identifier")]
        [StringLength(200)]
        public string Account_Identifier__c { get; set; }

        [Display(Name = "Product")]
        [StringLength(50)]
        public string Product__c { get; set; }

        [Display(Name = "Brand")]
        [StringLength(50)]
        public string Brand__c { get; set; }
    }
}
