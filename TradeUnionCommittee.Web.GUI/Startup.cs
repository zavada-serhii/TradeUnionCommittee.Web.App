using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TradeUnionCommittee.BLL.Infrastructure;
using TradeUnionCommittee.BLL.Interfaces.Directory;
using TradeUnionCommittee.BLL.Services.Directory;
using TradeUnionCommittee.Web.GUI.Models.FluentValidators;
using TradeUnionCommittee.Web.GUI.Models.ViewModels;
using TradeUnionCommittee.Web.GUI.Oops;

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

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddFluentValidation();

            new ServiceModule(Configuration.GetConnectionString("DefaultConnection"), services);

            DependencyInjectionService(services);
            DependencyInjectionValidator(services);
            DependencyInjectionOops(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
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
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void DependencyInjectionService(IServiceCollection services)
        {
            services.AddScoped<IPositionService, PositionService>();
        }

        private void DependencyInjectionValidator(IServiceCollection services)
        {
            services.AddScoped<IValidator<DirectoryViewModel>, DirectoryValidator>();
        }

        private void DependencyInjectionOops(IServiceCollection services)
        {
            services.AddScoped<IOops, Oops.Oops>();
        }
    }
}
