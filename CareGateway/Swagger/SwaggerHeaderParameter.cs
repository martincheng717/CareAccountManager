using Swashbuckle.Application;
using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;
using System.Web.Http.Description;

namespace CareGateway.Swagger
{
    [ExcludeFromCodeCoverage]
    public class SwaggerHeaderParameter : IOperationFilter
    {
        public List<SwaggerHeader> headers { get; set; }

        public void Apply(SwaggerDocsConfig c)
        {
            foreach (var header in headers)
            {
                c.ApiKey(header.Key).Name(header.Name).Description(header.Description).In("header");
            }
            c.OperationFilter(() => this);
        }


        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            operation.parameters = operation.parameters ?? new List<Parameter>();
            foreach (var header in headers)
            {
                operation.parameters.Add(new Parameter
                {
                    name = header.Name,
                    description = header.Description,
                    @in = "header",
                    required = header.IsRequired,
                    type = header.Type,
                    @default = header.DefaultValue
                });
            }
        }

        public void Apply(SwaggerDocument swaggerDoc, SchemaRegistry schemaRegistry, IApiExplorer apiExplorer)
        {
            var security = headers.Select(header => new Dictionary<string, IEnumerable<string>> { { header.Key, new string[0] } })
                .Cast<IDictionary<string, IEnumerable<string>>>().ToList();
            swaggerDoc.security = security;
        }
    }
}