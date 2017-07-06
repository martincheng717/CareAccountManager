using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;

namespace CareGateway.Swagger
{

    [ExcludeFromCodeCoverage]
    public class SwaggerHeader
    {
        public string Description { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string DefaultValue { get; set; }
        public bool IsRequired { get; set; }
        public string Type { get; set; }
    }
}