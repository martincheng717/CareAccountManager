using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace CareGateway.Sfdc.Model
{
    [ExcludeFromCodeCoverage]
    public class FieldInfo
    {
        public string Name { get; set; }
        public string DefaultValue { get; set; }
        public bool Required { get; set; }
        public string ErrorMessage { get; set; }
        public IEnumerable<string> PickList { get; set; }
    }
}
