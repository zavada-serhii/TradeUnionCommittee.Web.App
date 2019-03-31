using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;
using System.IO.Compression;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.Extensions;
using TradeUnionCommittee.Mvc.Web.GUI.Configurations;
using TradeUnionCommittee.Mvc.Web.GUI.Controllers.Directory;
using TradeUnionCommittee.ViewModels.Extensions;

namespace TradeUnionCommittee.Mvc.Web.GUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json", reloadOnChange: true, optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            var elasticUri = Configuration["ElasticConfiguration:Uri"];

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticUri))
                {
                    AutoRegisterTemplate = true,
                })
                .CreateLogger();
        }

        public IConfiguration Configuration { get; }

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

            var connectionString = Convert.ToBoolean(Configuration.GetConnectionString("UseSSL"))
                ? Configuration.GetConnectionString("DefaultConnectionSSL")
                : Configuration.GetConnectionString("DefaultConnection");

            services
                .AddTradeUnionCommitteeServiceModule(connectionString, Configuration.GetSection("HashIdConfigurationSetting").Get<HashIdConfigurationSetting>())
                .AddTradeUnionCommitteeViewModelsModule()
                .AddResponseCompression()
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddTradeUnionCommitteeValidationModule();

            services.AddDistributedMemoryCache();
            services.AddSession();

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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddSerilog();

            //env.EnvironmentName = EnvironmentName.Production;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseResponseCompression();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Directory}/{id?}");
            });
        }

        private void DependencyInjectionSystem(IServiceCollection services)
        {
            services.AddTransient<IDirectories, Directories>();
            services.AddSingleton(cm => AutoMapperConfiguration.ConfigureAutoMapper());
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}