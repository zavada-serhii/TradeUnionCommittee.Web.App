using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TradeUnionCommittee.BLL.Infrastructure;
using TradeUnionCommittee.BLL.Interfaces.Account;
using TradeUnionCommittee.BLL.Interfaces.Directory;
using TradeUnionCommittee.BLL.Interfaces.Employee;
using TradeUnionCommittee.BLL.Interfaces.Login;
using TradeUnionCommittee.BLL.Interfaces.Search;
using TradeUnionCommittee.BLL.Services.Account;
using TradeUnionCommittee.BLL.Services.Directory;
using TradeUnionCommittee.BLL.Services.Employee;
using TradeUnionCommittee.BLL.Services.Login;
using TradeUnionCommittee.BLL.Services.Search;
using TradeUnionCommittee.Web.GUI.AdditionalSettings.DropDownLists;
using TradeUnionCommittee.Web.GUI.AdditionalSettings.Oops;
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

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new PathString("/Login/Login");
                    options.LogoutPath = new PathString("/Login/Login");
                    options.AccessDeniedPath = new PathString("/Login/AccessDenied");
                });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddFluentValidation();

            new ServiceModule(Configuration.GetConnectionString("DefaultConnection"), services);

            DependencyInjectionService(services);
            DependencyInjectionSearch(services);
            DependencyInjectionSystem(services);
            DependencyInjectionFluentValidation(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
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

        private void DependencyInjectionService(IServiceCollection services)
        {
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IAccountService, AccountService>();

            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IEducationService, EducationService>();
            services.AddScoped<IScientificService, ScientificService>();
            services.AddScoped<IPositionService, PositionService>();
            services.AddScoped<ISocialActivityService, SocialActivityService>();
            services.AddScoped<IPrivilegesService, PrivilegesService>();
            services.AddScoped<IAwardService, AwardService>();
            services.AddScoped<IMaterialAidService, MaterialAidService>();
            services.AddScoped<IHobbyService, HobbyService>();
            services.AddScoped<ITravelService, TravelService>();
            services.AddScoped<IWellnessService, WellnessService>();
            services.AddScoped<ITourService, TourService>();
            services.AddScoped<IActivitiesService, ActivitiesService>();
            services.AddScoped<ICulturalService, CulturalService>();
            services.AddScoped<ISubdivisionsService, SubdivisionsService>();
            services.AddScoped<IDormitoryService, DormitoryService>();
            services.AddScoped<IDepartmentalService, DepartmentalService>();
        }

        private void DependencyInjectionSearch(IServiceCollection services)
        {
            services.AddScoped<ISearchService, SearchService>();
        }

        private void DependencyInjectionSystem(IServiceCollection services)
        {
            services.AddScoped<IOops, Oops>();
            services.AddScoped<IDropDownList, DropDownList>();
        }

        private void DependencyInjectionFluentValidation(IServiceCollection services)
        {
            services.AddScoped<IValidator<AddEmployeeViewModel>, AddEmployeeValidation>();
        }
    }
}