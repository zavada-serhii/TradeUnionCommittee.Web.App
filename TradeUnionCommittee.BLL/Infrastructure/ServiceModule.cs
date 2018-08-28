using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
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
using TradeUnionCommittee.DAL.Interfaces;
using TradeUnionCommittee.DAL.Repositories;
using TradeUnionCommittee.Encryption;

namespace TradeUnionCommittee.BLL.Infrastructure
{
    public static class ServiceModule
    {
        public static IServiceCollection AddTradeUnionCommitteeServiceModule(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>(o => new UnitOfWork(connectionString));
            services.AddScoped<ICryptoUtilities, CryptoUtilities>();
            services.AddSingleton(cm => AutoMapperModule.ConfigureAutoMapper());

            //---------------------------------------------------------------------------------------------

            services.AddScoped<ILoginService, LoginService>();
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

            //---------------------------------------------------------------------------------------------

            return services;
        }
    }

    internal class AutoMapperModule
    {
        internal static IMapper ConfigureAutoMapper()
        {
            return new MapperConfiguration(map =>
            {


            }).CreateMapper();
        }
    }
}