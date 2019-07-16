using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Extensions;
using TradeUnionCommittee.ViewModels.Extensions;
using TradeUnionCommittee.Web.Api.Configurations;
using TradeUnionCommittee.Web.Api.Extensions;
using TradeUnionCommittee.Web.Api.Models;

namespace TradeUnionCommittee.Web.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment hostingEnvironment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json", reloadOnChange: true, optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            var elasticUri = Configuration["ElasticConfiguration:Uri"];

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticUri))
                {
                    AutoRegisterTemplate = true
                })
                .WriteTo.Console(LogEventLevel.Information)
                .CreateLogger();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                var authOptions = Configuration.GetSection("AuthOptions").Get<AuthOptions>();
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

            var connectionString = Convert.ToBoolean(Configuration.GetConnectionString("UseSSL"))
                ? Configuration.GetConnectionString("DefaultConnectionSSL")
                : Configuration.GetConnectionString("DefaultConnection");

            var identityConnectionString = Convert.ToBoolean(Configuration.GetConnectionString("UseSSL"))
                ? Configuration.GetConnectionString("IdentityConnectionSSL")
                : Configuration.GetConnectionString("IdentityConnection");

            var auditConnectionString = Convert.ToBoolean(Configuration.GetConnectionString("UseSSL"))
                ? Configuration.GetConnectionString("AuditConnectionSSL")
                : Configuration.GetConnectionString("AuditConnection");

            var cloudStorageConnectionString = Convert.ToBoolean(Configuration.GetConnectionString("UseSSL"))
                ? Configuration.GetConnectionString("CloudStorageConnectionSSL")
                : Configuration.GetConnectionString("CloudStorageConnection");

            services
                .AddTradeUnionCommitteeServiceModule(connectionString,
                    identityConnectionString,
                    auditConnectionString,
                    new CloudStorageServiceCredentials
                    {
                        DbConnectionString = cloudStorageConnectionString,
                        UseStorageSsl = Convert.ToBoolean(Configuration["CloudStorageConfiguration:UseSSL"]),
                        Url = Configuration["CloudStorageConfiguration:Url"],
                        AccessKey = Configuration["CloudStorageConfiguration:AccessKey"],
                        SecretKey = Configuration["CloudStorageConfiguration:SecretKey"]
                    },
                    Configuration.GetSection("HashIdConfigurationSetting").Get<HashIdConfigurationSetting>())
                .AddTradeUnionCommitteeViewModelsModule()
                .AddResponseCompression()
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddTradeUnionCommitteeValidationModule();

            services.AddApiVersioning(options => options.ReportApiVersions = true);
            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
            services.AddSwaggerGen();

            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });

            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
            });

            DependencyInjectionSystem(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApiVersionDescriptionProvider provider)
        {
            loggerFactory.AddSerilog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseResponseCompression();
            app.UseCustomMiddlewares();

            app.UseAuthentication();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant()); 
                }
                c.DocExpansion(DocExpansion.None);
                c.EnableValidator();
            });
        }

        private void DependencyInjectionSystem(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<AuthOptions>(Configuration.GetSection("AuthOptions"));
            services.AddSingleton(cm => AutoMapperConfiguration.ConfigureAutoMapper());
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IJwtBearerConfiguration, JwtBearerConfiguration>();
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        }
    }
}