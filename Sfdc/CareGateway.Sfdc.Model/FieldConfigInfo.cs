using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace CareGateway.Sfdc.Model
{
    [ExcludeFromCodeCoverage]
    public class FieldConfigInfo
    {
        public string AssemplyName { get; set; }
        public string ClassName { get; set; }
        public int MaxChildCase { get; set; }
        public List<FieldInfo> Fields { get; set; }

        public FieldConfigInfo()
        {
            MaxChildCase = 200;
        }
    }
}
