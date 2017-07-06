using System;
using System.ComponentModel.DataAnnotations;
using Salesforce.Common.Attributes;
using System.Diagnostics.CodeAnalysis;

namespace CareGateway.Sfdc.Model.Salesforce
{
    [ExcludeFromCodeCoverage]
    public class Group
    {
        [Key]
        [Display(Name = "Group ID")]
        [Createable(false), Updateable(false)]
        public string Id { get; set; }

        [StringLength(40)]
        public string Name { get; set; }

        [Display(Name = "Developer Name")]
        [StringLength(80)]
        public string DeveloperName { get; set; }

        [Display(Name = "Related ID")]
        [Createable(false), Updateable(false)]
        public string RelatedId { get; set; }

        [Updateable(false)]
        public string Type { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Owner ID")]
        [Createable(false), Updateable(false)]
        public string OwnerId { get; set; }

        [Display(Name = "Send Email to Members")]
        public bool DoesSendEmailToMembers { get; set; }

        [Display(Name = "Include Bosses")]
        public bool DoesIncludeBosses { get; set; }

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

    }
}
