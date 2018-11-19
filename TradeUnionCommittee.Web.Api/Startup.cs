using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using TradeUnionCommittee.BLL.Extensions;
using TradeUnionCommittee.BLL.Utilities;
using TradeUnionCommittee.ViewModels.Extensions;
using TradeUnionCommittee.Web.Api.Configuration;

namespace TradeUnionCommittee.Web.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            Configuration.GetSection("AuthOptions").Get<AuthOptions>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = AuthOptions.Issuer,

                        ValidateAudience = true,
                        ValidAudience = AuthOptions.Audience,
                        ValidateLifetime = true,

                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true,
                    };
                });

            services
                .AddTradeUnionCommitteeServiceModule(Configuration.GetConnectionString("DefaultConnection"), Configuration.GetSection("HashIdUtilitiesSettings").Get<HashIdUtilitiesSetting>())
                .AddTradeUnionCommitteeViewModelsModule()
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddTradeUnionCommitteeValidationModule();

            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Info {Title = "Trade Union Committee API", Description = "Swagger Trade Union Committee API" }); });

            DependencyInjectionSystem(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
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

            app.UseAuthentication();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Trade Union Committee API"); });
        }

        private void DependencyInjectionSystem(IServiceCollection services)
        {
            services.AddSingleton(cm => AutoMapperConfiguration.ConfigureAutoMapper());
        }
    }
}
