using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TradeUnionCommittee.BLL.Infrastructure;
using TradeUnionCommittee.BLL.Interfaces.Directory;
using TradeUnionCommittee.BLL.Services.Directory;
using TradeUnionCommittee.GUI.Models.FluentValidators;
using TradeUnionCommittee.GUI.Models.ViewModels;

namespace TradeUnionCommittee.GUI
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
            services.AddMvc().AddFluentValidation();

            new ServiceModule(Configuration.GetConnectionString("DefaultConnection"), services);

            DependencyInjectionService(services);
            DependencyInjectionValidator(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

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
    }
}
