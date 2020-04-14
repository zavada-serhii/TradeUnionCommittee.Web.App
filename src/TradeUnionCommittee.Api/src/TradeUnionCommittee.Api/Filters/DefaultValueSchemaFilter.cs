using System;
using System.ComponentModel;
using System.Reflection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TradeUnionCommittee.Api.Filters
{
    public class DefaultValueSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema.Properties == null)
            {
                return;
            }

            foreach (var propertyInfo in context.Type.GetProperties())
            {
                var defaultAttribute = propertyInfo.GetCustomAttribute<DefaultValueAttribute>();
                if (defaultAttribute != null)
                {
                    foreach (var (key, value) in schema.Properties)
                    {
                        if (ToCamelCase(propertyInfo.Name) == key)
                        {
                            if (defaultAttribute.Value is int intValue)
                            {
                                value.Example = new OpenApiInteger(intValue);
                                break;
                            }

                            if (defaultAttribute.Value is string stringValue)
                            {
                                value.Example = new OpenApiString(stringValue);
                                break;
                            }

                            if (defaultAttribute.Value is DateTime dateTimeValue)
                            {
                                value.Example = new OpenApiDateTime(dateTimeValue);
                                break;
                            }
                        }
                    }
                }
            }
        }

        private string ToCamelCase(string name) => char.ToLowerInvariant(name[0]) + name.Substring(1);
    }
}