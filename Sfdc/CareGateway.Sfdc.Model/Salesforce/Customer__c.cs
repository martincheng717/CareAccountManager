using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using Salesforce.Common.Attributes;

namespace CareGateway.Sfdc.Model.Salesforce
{
    [ExcludeFromCodeCoverage]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class Customer__c
    {
        [Key]
        [Display(Name = "Record ID")]
        [Createable(false), Updateable(false)]
        public string Id { get; set; }

        [Display(Name = "Owner ID")]
        [Updateable(false)]
        public string OwnerId { get; set; }

        [Display(Name = "Deleted")]
        [Createable(false), Updateable(false)]
        public bool IsDeleted { get; set; }

        [Display(Name = "Account Key")]
        [StringLength(80)]
        public string Name { get; set; }

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

        [Display(Name = "Last Viewed Date")]
        [Createable(false), Updateable(false)]
        public DateTimeOffset? LastViewedDate { get; set; }

        [Display(Name = "Last Referenced Date")]
        [Createable(false), Updateable(false)]
        public DateTimeOffset? LastReferencedDate { get; set; }

        [Display(Name = "PlasticKey")]
        [StringLength(255)]
        public string PlasticKey__c { get; set; }

        [Display(Name = "CustomerKey")]
        [StringLength(255)]
        public string CustomerKey__c { get; set; }

        [Display(Name = "Case")]
        [JsonProperty("Customer__c")]
        public string Customer__c_Property { get; set; }

        [Display(Name = "Account Key")]
        [StringLength(50)]
        public string AccountKey__c { get; set; }

    }
}
