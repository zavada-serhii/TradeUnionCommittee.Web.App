using Microsoft.Extensions.DependencyInjection;
using TradeUnionCommittee.BLL.Interfaces.Account;
using TradeUnionCommittee.BLL.Interfaces.Directory;
using TradeUnionCommittee.BLL.Interfaces.Employee;
using TradeUnionCommittee.BLL.Interfaces.PDF;
using TradeUnionCommittee.BLL.Interfaces.Search;
using TradeUnionCommittee.BLL.Interfaces.SystemAudit;
using TradeUnionCommittee.BLL.Services.Account;
using TradeUnionCommittee.BLL.Services.Directory;
using TradeUnionCommittee.BLL.Services.Employee;
using TradeUnionCommittee.BLL.Services.PDF;
using TradeUnionCommittee.BLL.Services.Search;
using TradeUnionCommittee.BLL.Services.SystemAudit;
using TradeUnionCommittee.BLL.Utilities;
using TradeUnionCommittee.DAL.Extensions;

namespace TradeUnionCommittee.BLL.Extensions
{
    public static class ExtensionsServiceCollection
    {
        public static IServiceCollection AddTradeUnionCommitteeServiceModule(this IServiceCollection services, string connectionString, HashIdUtilitiesSetting setting)
        {
            // Injection UnitOfWork, HashIdUtilities, AutoMapper

            services.AddUnitOfWork(connectionString);
            services.AddSingleton<IHashIdUtilities, HashIdUtilities>(x => new HashIdUtilities(setting));
            services.AddSingleton<IAutoMapperUtilities, AutoMapperUtilities>();

            // Injection All Service
            //---------------------------------------------------------------------------------------------
            
            services.AddScoped<IAccountService, AccountService>();

            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IEducationService, EducationService>();
            services.AddScoped<IQualificationService, QualificationService>();
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

            services.AddScoped<ISearchService, SearchService>();
            services.AddScoped<ISystemAuditService, SystemAuditService>();
            services.AddScoped<IPdfService, PdfService>();

            //---------------------------------------------------------------------------------------------

            return services;
        }
    }
}