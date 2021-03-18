using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.IO.Compression;
using TradeUnionCommittee.Api.Configurations;
using TradeUnionCommittee.Api.Extensions;
using TradeUnionCommittee.Api.Filters;
using TradeUnionCommittee.Api.Models;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.Extensions;
using TradeUnionCommittee.ViewModels.Extensions;

namespace TradeUnionCommittee.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IHostEnvironment hostingEnvironment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json", reloadOnChange: true, optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            var elk = Configuration.GetSection("ElkConnection").Get<ElkConnection>();

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elk.Url))
                {
                    AutoRegisterTemplate = true,
                    ModifyConnectionSettings = x =>
                    {
                        if (elk.UseBasicAuthentication)
                            x.BasicAuthentication(elk.UserName, elk.Password);
                        x.ServerCertificateValidationCallback((sender, certificate, chain, errors) => elk.IgnoreCertificateValidation);
                        return x;
                    }
                })
                .WriteTo.Console(LogEventLevel.Information)
                .CreateLogger();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                var authOptions = Configuration.GetSection("AuthOptions").Get<AuthModel>();
                options.RequireHttpsMetadata = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = authOptions.Issuer,

                    ValidateAudience = true,
                    ValidAudience = authOptions.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,

                    IssuerSigningKey = authOptions.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true
                };
            });

            services
                .AddTradeUnionCommitteeServiceModule(
                    Configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>(),
                    Configuration.GetSection("CloudStorageConnection").Get<CloudStorageConnection>(),
                    Configuration["DataAnalysisUrl"],
                    Configuration.GetSection("HashIdConfiguration").Get<HashIdConfiguration>(),
                    typeof(Configurations.AutoMapperProfile))
                .AddTradeUnionCommitteeViewModelsModule();

            services.AddResponseCompression()
                    .Configure<GzipCompressionProviderOptions>(options => { options.Level = CompressionLevel.Optimal; })
                    .AddResponseCompression(options => { options.EnableForHttps = true; });

            services.AddApiVersioning(options => options.ReportApiVersions = true)
                    .AddVersionedApiExplorer(options => { options.GroupNameFormat = "'v'VVV"; options.SubstituteApiVersionInUrl = true; })
                    .AddSwaggerGen(x =>
                    {
                        x.SchemaFilter<DefaultValueSchemaFilter>();
                        x.UseAllOfToExtendReferenceSchemas();
                    });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddControllers()
                    .AddTradeUnionCommitteeValidationModule();

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.All;
                options.KnownNetworks.Clear();
                options.KnownProxies.Clear();
            });

            DependencyInjectionSystem(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, IApiVersionDescriptionProvider provider)
        {
            loggerFactory.AddSerilog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
            );

            app.UseResponseCompression();
            app.UseCustomMiddlewares();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    c.RoutePrefix = string.Empty;
                    c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
                c.DocExpansion(DocExpansion.None);
                c.EnableValidator();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void DependencyInjectionSystem(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<AuthModel>(Configuration.GetSection("AuthOptions"));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IJwtBearerConfiguration, JwtBearerConfiguration>();
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        }

        private class ElkConnection
        {
            public string Url { get; set; }
            public bool UseBasicAuthentication { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
            public bool IgnoreCertificateValidation { get; set; }
        }
    }
}