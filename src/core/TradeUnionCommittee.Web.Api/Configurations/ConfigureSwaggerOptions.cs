using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TradeUnionCommittee.Web.Api.Configurations
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, new Info
                {
                    Version = description.ApiVersion.ToString(),
                    Title = $"Trade Union Committee API {description.ApiVersion}",
                    Description = "Swagger Trade Union Committee API"
                });
            }

            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "TradeUnionCommittee.Web.Api.xml"));

            options.AddSecurityDefinition("Bearer", new ApiKeyScheme
            {
               In = "header",
               Description = "Please enter into field the word 'Bearer' following by space and JWT",
               Name = "Authorization",
               Type = "apiKey"
            });

            options.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
            {
                { "Bearer", Enumerable.Empty<string>() }
            });
        }
    }
}
