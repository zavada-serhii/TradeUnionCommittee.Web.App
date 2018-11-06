using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO.Compression;
using TradeUnionCommittee.BLL.Extensions;
using TradeUnionCommittee.BLL.Utilities;
using TradeUnionCommittee.Web.GUI.Configuration;
using TradeUnionCommittee.Web.GUI.Configuration.DropDownLists;
using TradeUnionCommittee.Web.GUI.Controllers.Oops;
using TradeUnionCommittee.Web.GUI.FluentValidation;
using TradeUnionCommittee.Web.GUI.Models;

namespace TradeUnionCommittee.Web.GUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
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
                .AddTradeUnionCommitteeServiceModule(Configuration.GetConnectionString("DefaultConnection"), Configuration.GetSection("HashIdUtilitiesSettings").Get<HashIdUtilitiesSetting>())
                .AddResponseCompression()
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddFluentValidation();

            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });

            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
            });

            DependencyInjectionSystem(services);
            DependencyInjectionFluentValidation(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
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
            services.AddScoped<IOops, OopsController>();
            services.AddScoped<IDropDownList, DropDownList>();
            services.AddSingleton(cm => AutoMapperConfiguration.ConfigureAutoMapper());
        }

        private void DependencyInjectionFluentValidation(IServiceCollection services)
        {
            services.AddScoped<IValidator<CreateEmployeeViewModel>, CreateEmployeeValidation>();
        }
    }
}