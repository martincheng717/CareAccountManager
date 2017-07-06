using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Salesforce.Common.Attributes;

namespace CareGateway.Sfdc.Model.Salesforce
{
    [ExcludeFromCodeCoverage]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class User
    {
        [Key]
        [Display(Name = "User ID")]
        [Createable(false), Updateable(false)]
        public string Id { get; set; }

        [StringLength(80)]
        public string Username { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(80)]
        public string LastName { get; set; }

        [Display(Name = "First Name")]
        [StringLength(40)]
        public string FirstName { get; set; }

        [Display(Name = "Full Name")]
        [StringLength(121)]
        [Createable(false), Updateable(false)]
        public string Name { get; set; }

        [Display(Name = "Company Name")]
        [StringLength(80)]
        public string CompanyName { get; set; }

        [StringLength(80)]
        public string Division { get; set; }

        [StringLength(80)]
        public string Department { get; set; }

        [StringLength(80)]
        public string Title { get; set; }

        public string Street { get; set; }

        [StringLength(40)]
        public string City { get; set; }

        [Display(Name = "State/Province")]
        [StringLength(80)]
        public string State { get; set; }

        [Display(Name = "Zip/Postal Code")]
        [StringLength(20)]
        public string PostalCode { get; set; }

        [StringLength(80)]
        public string Country { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "AutoBcc")]
        public bool EmailPreferencesAutoBcc { get; set; }

        [Display(Name = "AutoBccStayInTouch")]
        public bool EmailPreferencesAutoBccStayInTouch { get; set; }

        [Display(Name = "StayInTouchReminder")]
        public bool EmailPreferencesStayInTouchReminder { get; set; }

        [Display(Name = "Email Sender Address")]
        [EmailAddress]
        public string SenderEmail { get; set; }

        [Display(Name = "Email Sender Name")]
        [StringLength(80)]
        public string SenderName { get; set; }

        [Display(Name = "Email Signature")]
        [StringLength(1333)]
        public string Signature { get; set; }

        [Display(Name = "Stay-in-Touch Email Subject")]
        [StringLength(80)]
        public string StayInTouchSubject { get; set; }

        [Display(Name = "Stay-in-Touch Email Signature")]
        [StringLength(512)]
        public string StayInTouchSignature { get; set; }

        [Display(Name = "Stay-in-Touch Email Note")]
        [StringLength(512)]
        public string StayInTouchNote { get; set; }

        [Phone]
        public string Phone { get; set; }

        [Phone]
        public string Fax { get; set; }

        [Display(Name = "Cell")]
        [Phone]
        public string MobilePhone { get; set; }

        [StringLength(8)]
        public string Alias { get; set; }

        [Display(Name = "Nickname")]
        [StringLength(40)]
        public string CommunityNickname { get; set; }

        [Display(Name = "User Photo has a badge overlay")]
        [Createable(false), Updateable(false)]
        public bool IsBadged { get; set; }

        [Display(Name = "User Photo badge text overlay")]
        [StringLength(80)]
        [Createable(false), Updateable(false)]
        public string BadgeText { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Time Zone")]
        public string TimeZoneSidKey { get; set; }

        [Display(Name = "Role ID")]
        public string UserRoleId { get; set; }

        [Display(Name = "Locale")]
        public string LocaleSidKey { get; set; }

        [Display(Name = "Info Emails")]
        public bool ReceivesInfoEmails { get; set; }

        [Display(Name = "Admin Info Emails")]
        public bool ReceivesAdminInfoEmails { get; set; }

        [Display(Name = "Email Encoding")]
        public string EmailEncodingKey { get; set; }

        [Display(Name = "Profile ID")]
        public string ProfileId { get; set; }

        [Display(Name = "User Type")]
        [Createable(false), Updateable(false)]
        public string UserType { get; set; }

        [Display(Name = "Language")]
        public string LanguageLocaleKey { get; set; }

        [Display(Name = "Employee Number")]
        [StringLength(20)]
        public string EmployeeNumber { get; set; }

        [Display(Name = "Delegated Approver ID")]
        public string DelegatedApproverId { get; set; }

        [Display(Name = "Manager ID")]
        public string ManagerId { get; set; }

        [Display(Name = "Last Login")]
        [Createable(false), Updateable(false)]
        public DateTimeOffset? LastLoginDate { get; set; }

        [Display(Name = "Last Password Change or Reset")]
        [Createable(false), Updateable(false)]
        public DateTimeOffset? LastPasswordChangeDate { get; set; }

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

        [Display(Name = "Offline Edition Trial Expiration Date")]
        [Createable(false), Updateable(false)]
        public DateTimeOffset? OfflineTrialExpirationDate { get; set; }

        [Display(Name = "Sales Anywhere Trial Expiration Date")]
        [Createable(false), Updateable(false)]
        public DateTimeOffset? OfflinePdaTrialExpirationDate { get; set; }

        [Display(Name = "Marketing User")]
        public bool UserPermissionsMarketingUser { get; set; }

        [Display(Name = "Offline User")]
        public bool UserPermissionsOfflineUser { get; set; }

        [Display(Name = "AvantGo User")]
        public bool UserPermissionsAvantgoUser { get; set; }

        [Display(Name = "Auto-login To Call Center")]
        public bool UserPermissionsCallCenterAutoLogin { get; set; }

        [Display(Name = "Apex Mobile User")]
        public bool UserPermissionsMobileUser { get; set; }

        [Display(Name = "Salesforce CRM Content User")]
        public bool UserPermissionsSFContentUser { get; set; }

        [Display(Name = "Knowledge User")]
        public bool UserPermissionsKnowledgeUser { get; set; }

        [Display(Name = "Force.com Flow User")]
        public bool UserPermissionsInteractionUser { get; set; }

        [Display(Name = "Service Cloud User")]
        public bool UserPermissionsSupportUser { get; set; }

        [Display(Name = "Live Agent User")]
        public bool UserPermissionsLiveAgentUser { get; set; }

        [Display(Name = "Chatter Answers User")]
        public bool UserPermissionsChatterAnswersUser { get; set; }

        [Display(Name = "Allow Forecasting")]
        public bool ForecastEnabled { get; set; }

        [Display(Name = "ActivityRemindersPopup")]
        public bool UserPreferencesActivityRemindersPopup { get; set; }

        [Display(Name = "EventRemindersCheckboxDefault")]
        public bool UserPreferencesEventRemindersCheckboxDefault { get; set; }

        [Display(Name = "TaskRemindersCheckboxDefault")]
        public bool UserPreferencesTaskRemindersCheckboxDefault { get; set; }

        [Display(Name = "ReminderSoundOff")]
        public bool UserPreferencesReminderSoundOff { get; set; }

        [Display(Name = "DisableAllFeedsEmail")]
        public bool UserPreferencesDisableAllFeedsEmail { get; set; }

        [Display(Name = "DisableFollowersEmail")]
        public bool UserPreferencesDisableFollowersEmail { get; set; }

        [Display(Name = "DisableProfilePostEmail")]
        public bool UserPreferencesDisableProfilePostEmail { get; set; }

        [Display(Name = "DisableChangeCommentEmail")]
        public bool UserPreferencesDisableChangeCommentEmail { get; set; }

        [Display(Name = "DisableLaterCommentEmail")]
        public bool UserPreferencesDisableLaterCommentEmail { get; set; }

        [Display(Name = "DisProfPostCommentEmail")]
        public bool UserPreferencesDisProfPostCommentEmail { get; set; }

        [Display(Name = "ApexPagesDeveloperMode")]
        public bool UserPreferencesApexPagesDeveloperMode { get; set; }

        [Display(Name = "HideCSNGetChatterMobileTask")]
        public bool UserPreferencesHideCSNGetChatterMobileTask { get; set; }

        [Display(Name = "DisableMentionsPostEmail")]
        public bool UserPreferencesDisableMentionsPostEmail { get; set; }

        [Display(Name = "DisMentionsCommentEmail")]
        public bool UserPreferencesDisMentionsCommentEmail { get; set; }

        [Display(Name = "HideCSNDesktopTask")]
        public bool UserPreferencesHideCSNDesktopTask { get; set; }

        [Display(Name = "HideChatterOnboardingSplash")]
        public bool UserPreferencesHideChatterOnboardingSplash { get; set; }

        [Display(Name = "HideSecondChatterOnboardingSplash")]
        public bool UserPreferencesHideSecondChatterOnboardingSplash { get; set; }

        [Display(Name = "DisCommentAfterLikeEmail")]
        public bool UserPreferencesDisCommentAfterLikeEmail { get; set; }

        [Display(Name = "DisableLikeEmail")]
        public bool UserPreferencesDisableLikeEmail { get; set; }

        [Display(Name = "DisableMessageEmail")]
        public bool UserPreferencesDisableMessageEmail { get; set; }

        [Display(Name = "DisableBookmarkEmail")]
        public bool UserPreferencesDisableBookmarkEmail { get; set; }

        [Display(Name = "DisableSharePostEmail")]
        public bool UserPreferencesDisableSharePostEmail { get; set; }

        [Display(Name = "EnableAutoSubForFeeds")]
        public bool UserPreferencesEnableAutoSubForFeeds { get; set; }

        [Display(Name = "DisableFileShareNotificationsForApi")]
        public bool UserPreferencesDisableFileShareNotificationsForApi { get; set; }

        [Display(Name = "ShowTitleToExternalUsers")]
        public bool UserPreferencesShowTitleToExternalUsers { get; set; }

        [Display(Name = "ShowManagerToExternalUsers")]
        public bool UserPreferencesShowManagerToExternalUsers { get; set; }

        [Display(Name = "ShowEmailToExternalUsers")]
        public bool UserPreferencesShowEmailToExternalUsers { get; set; }

        [Display(Name = "ShowWorkPhoneToExternalUsers")]
        public bool UserPreferencesShowWorkPhoneToExternalUsers { get; set; }

        [Display(Name = "ShowMobilePhoneToExternalUsers")]
        public bool UserPreferencesShowMobilePhoneToExternalUsers { get; set; }

        [Display(Name = "ShowFaxToExternalUsers")]
        public bool UserPreferencesShowFaxToExternalUsers { get; set; }

        [Display(Name = "ShowStreetAddressToExternalUsers")]
        public bool UserPreferencesShowStreetAddressToExternalUsers { get; set; }

        [Display(Name = "ShowCityToExternalUsers")]
        public bool UserPreferencesShowCityToExternalUsers { get; set; }

        [Display(Name = "ShowStateToExternalUsers")]
        public bool UserPreferencesShowStateToExternalUsers { get; set; }

        [Display(Name = "ShowPostalCodeToExternalUsers")]
        public bool UserPreferencesShowPostalCodeToExternalUsers { get; set; }

        [Display(Name = "ShowCountryToExternalUsers")]
        public bool UserPreferencesShowCountryToExternalUsers { get; set; }

        [Display(Name = "ShowProfilePicToGuestUsers")]
        public bool UserPreferencesShowProfilePicToGuestUsers { get; set; }

        [Display(Name = "ShowTitleToGuestUsers")]
        public bool UserPreferencesShowTitleToGuestUsers { get; set; }

        [Display(Name = "ShowCityToGuestUsers")]
        public bool UserPreferencesShowCityToGuestUsers { get; set; }

        [Display(Name = "ShowStateToGuestUsers")]
        public bool UserPreferencesShowStateToGuestUsers { get; set; }

        [Display(Name = "ShowPostalCodeToGuestUsers")]
        public bool UserPreferencesShowPostalCodeToGuestUsers { get; set; }

        [Display(Name = "ShowCountryToGuestUsers")]
        public bool UserPreferencesShowCountryToGuestUsers { get; set; }

        [Display(Name = "HideS1BrowserUI")]
        public bool UserPreferencesHideS1BrowserUI { get; set; }

        [Display(Name = "DisableEndorsementEmail")]
        public bool UserPreferencesDisableEndorsementEmail { get; set; }

        [Display(Name = "LightningExperiencePreferred")]
        public bool UserPreferencesLightningExperiencePreferred { get; set; }

        [Display(Name = "Contact ID")]
        [Updateable(false)]
        public string ContactId { get; set; }

        [Display(Name = "Account ID")]
        [Createable(false), Updateable(false)]
        public string AccountId { get; set; }

        [Display(Name = "Call Center ID")]
        public string CallCenterId { get; set; }

        [Phone]
        public string Extension { get; set; }

        [Display(Name = "SAML Federation ID")]
        [StringLength(512)]
        public string FederationIdentifier { get; set; }

        [Display(Name = "About Me")]
        public string AboutMe { get; set; }

        [Display(Name = "Url for full-sized Photo")]
        [Url]
        [Createable(false), Updateable(false)]
        public string FullPhotoUrl { get; set; }

        [Display(Name = "Photo")]
        [Url]
        [Createable(false), Updateable(false)]
        public string SmallPhotoUrl { get; set; }

        [Display(Name = "Chatter Email Highlights Frequency")]
        public string DigestFrequency { get; set; }

        [Display(Name = "Default Notification Frequency when Joining Groups")]
        public string DefaultGroupNotificationFrequency { get; set; }

        [Display(Name = "Last Viewed Date")]
        [Createable(false), Updateable(false)]
        public DateTimeOffset? LastViewedDate { get; set; }

        [Display(Name = "Last Referenced Date")]
        [Createable(false), Updateable(false)]
        public DateTimeOffset? LastReferencedDate { get; set; }

        [Display(Name = "SysUserKey")]
        [StringLength(10)]
        public string SysUserKey__c { get; set; }

        [Display(Name = "Today + 10 Business Days")]
        [Createable(false), Updateable(false)]
        public DateTimeOffset? Today_10_Business_Days__c { get; set; }

        [Display(Name = "Today + 3 Business Days")]
        [Createable(false), Updateable(false)]
        public DateTimeOffset? Today_3_Business_Days__c { get; set; }

        [Display(Name = "Today + 14 Business Days")]
        [Createable(false), Updateable(false)]
        public DateTimeOffset? Today_14_Business_Days__c { get; set; }

        [Display(Name = "CFS Status")]
        public string lmscons__CFS_Status__c { get; set; }

        [Display(Name = "Cornerstone Group Admin")]
        public bool lmscons__Cornerstone_Group_Admin__c { get; set; }

        [Display(Name = "Cornerstone ID")]
        [StringLength(200)]
        public string lmscons__Cornerstone_ID__c { get; set; }

    }
}
