using System;
using System.ComponentModel.DataAnnotations;
using Salesforce.Common.Attributes;
using System.Diagnostics.CodeAnalysis;

namespace CareGateway.Sfdc.Model.Salesforce
{
    [ExcludeFromCodeCoverage]
    public class RecordType
    {
        [Key]
        [Display(Name = "Record Type ID")]
        [Createable(false), Updateable(false)]
        public string Id { get; set; }

        [StringLength(80)]
        public string Name { get; set; }

        [Display(Name = "Record Type Name")]
        [StringLength(80)]
        public string DeveloperName { get; set; }

        [Display(Name = "Namespace Prefix")]
        [StringLength(15)]
        [Createable(false), Updateable(false)]
        public string NamespacePrefix { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [Display(Name = "Business Process ID")]
        public string BusinessProcessId { get; set; }

        [Display(Name = "Sobject Type Name")]
        [Updateable(false)]
        public string SobjectType { get; set; }

        [Display(Name = "Active")]
        [Createable(false)]
        public bool IsActive { get; set; }

        [Display(Name = "Is Person Type")]
        [Createable(false), Updateable(false)]
        public bool IsPersonType { get; set; }

        [Display(Name = "Created By ID")]
        [Createable(false), Updateable(false)]
        public string CreatedById { get; set; }

        [Display(Name = "Created Date")]
        [Createable(false), Updateable(false)]
        public DateTimeOffset CreatedDate { get; set; }

        [Display(Name = "Last Modified By ID")]
        [Createable(false), Updateable(false)]
        public string LastModifiedById { get; set; }

        [Display(Name = "Last Modified Date")]
        [Createable(false), Updateable(false)]
        public DateTimeOffset LastModifiedDate { get; set; }

        [Display(Name = "System Modstamp")]
        [Createable(false), Updateable(false)]
        public DateTimeOffset SystemModstamp { get; set; }

    }
}
