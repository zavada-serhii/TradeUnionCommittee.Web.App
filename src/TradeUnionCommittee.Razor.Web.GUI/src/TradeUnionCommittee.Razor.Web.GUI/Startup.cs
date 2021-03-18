using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;
using System;
using System.IO.Compression;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.Extensions;
using TradeUnionCommittee.Razor.Web.GUI.Controllers.Directory;
using TradeUnionCommittee.ViewModels.Extensions;

namespace TradeUnionCommittee.Razor.Web.GUI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IHostEnvironment hostEnvironment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", reloadOnChange: true, optional: true)
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.Cookie.Name = "TradeUnionCommitteeCookie";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.SlidingExpiration = true;
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
                    .AddDistributedMemoryCache()
                    .Configure<GzipCompressionProviderOptions>(options => { options.Level = CompressionLevel.Optimal; })
                    .AddResponseCompression(options => { options.EnableForHttps = true; });

            services.AddSession();

            services.AddControllersWithViews()
                    .AddTradeUnionCommitteeValidationModule();

            DependencyInjectionSystem(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddSerilog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseResponseCompression();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCookiePolicy();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Directory}/{id?}/{subid?}");
            });
        }

        private void DependencyInjectionSystem(IServiceCollection services)
        {
            services.AddTransient<IDirectories, Directories>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
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