﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace TradeUnionCommittee.Api.Configurations
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
            var version = Assembly.GetEntryAssembly()?.GetName().Version;
            var os = Environment.OSVersion;
            var clr = Environment.Version;
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var project = Assembly.GetEntryAssembly()?.GetName().Name;
            var repository = "https://github.com/zavada-serhii/TradeUnionCommittee.Web.App";

            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, new OpenApiInfo
                {
                    Version = description.ApiVersion.ToString(),
                    Title = $"Trade Union Committee API - v{description.ApiVersion}",
                    Description = $"Build - `{version}` | OS - `{os}` | CLR - `{clr}` | Environment - `{environment}` <br> <br> [Repository]({repository})",
                    Contact = new OpenApiContact
                    {
                        Name = "Support",
                        Email = $"s.zavada@bankvostok.com.ua?subject={project}"
                    }
                });
            }

            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "TradeUnionCommittee.Api.xml"));

            var openApiSecurityScheme = new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter into field the word 'Bearer' following by space and JWT",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = JwtBearerDefaults.AuthenticationScheme }
            };

            options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, openApiSecurityScheme);
            options.AddSecurityRequirement(new OpenApiSecurityRequirement{ { openApiSecurityScheme, new List<string>()} });
        }
    }
}
