using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
using TradeUnionCommittee.Api.Models;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.Extensions;
using TradeUnionCommittee.ViewModels.Extensions;
using BllAutoMapperProfile = TradeUnionCommittee.BLL.Configurations.AutoMapperProfile;
using MainAutoMapperProfile = TradeUnionCommittee.Api.Configurations.AutoMapperProfile;

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

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(Configuration["RestConnection:ElasticUrl"]))
                {
                    AutoRegisterTemplate = true
                })
                .WriteTo.Console(LogEventLevel.Information)
                .CreateLogger();
        }

        public void ConfigureServices(IServiceCollection services)
        {
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
                    Configuration.GetSection("RestConnection").Get<RestConnection>(),
                    Configuration.GetSection("HashIdConfiguration").Get<HashIdConfiguration>())
                .AddTradeUnionCommitteeViewModelsModule();

            services.AddResponseCompression()
                    .Configure<GzipCompressionProviderOptions>(options => { options.Level = CompressionLevel.Optimal; })
                    .AddResponseCompression(options => { options.EnableForHttps = true; });

            services.AddApiVersioning(options => options.ReportApiVersions = true)
                    .AddVersionedApiExplorer(options => { options.GroupNameFormat = "'v'VVV"; options.SubstituteApiVersionInUrl = true; })
                    .AddSwaggerGen();

            services.AddControllers()
                    .AddTradeUnionCommitteeValidationModule();

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
            app.UseResponseCompression();
            app.UseCustomMiddlewares();
            app.UseAuthentication();
            app.UseAuthorization();

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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void DependencyInjectionSystem(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<AuthModel>(Configuration.GetSection("AuthOptions"));
            services.AddAutoMapper(typeof(MainAutoMapperProfile), typeof(BllAutoMapperProfile));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IJwtBearerConfiguration, JwtBearerConfiguration>();
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        }
    }
}