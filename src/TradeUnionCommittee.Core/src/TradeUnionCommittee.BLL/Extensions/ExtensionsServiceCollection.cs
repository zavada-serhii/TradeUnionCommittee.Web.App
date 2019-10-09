using Microsoft.Extensions.DependencyInjection;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.Helpers;
using TradeUnionCommittee.BLL.Interfaces.Account;
using TradeUnionCommittee.BLL.Interfaces.Dashboard;
using TradeUnionCommittee.BLL.Interfaces.Directory;
using TradeUnionCommittee.BLL.Interfaces.Lists.Children;
using TradeUnionCommittee.BLL.Interfaces.Lists.Employee;
using TradeUnionCommittee.BLL.Interfaces.Lists.Family;
using TradeUnionCommittee.BLL.Interfaces.Lists.GrandChildren;
using TradeUnionCommittee.BLL.Interfaces.PDF;
using TradeUnionCommittee.BLL.Interfaces.Search;
using TradeUnionCommittee.BLL.Interfaces.SystemAudit;
using TradeUnionCommittee.BLL.Services.Account;
using TradeUnionCommittee.BLL.Services.Dashboard;
using TradeUnionCommittee.BLL.Services.Directory;
using TradeUnionCommittee.BLL.Services.Lists.Children;
using TradeUnionCommittee.BLL.Services.Lists.Employee;
using TradeUnionCommittee.BLL.Services.Lists.Family;
using TradeUnionCommittee.BLL.Services.Lists.GrandChildren;
using TradeUnionCommittee.BLL.Services.PDF;
using TradeUnionCommittee.BLL.Services.Search;
using TradeUnionCommittee.BLL.Services.SystemAudit;
using TradeUnionCommittee.CloudStorage.Service.Extensions;
using TradeUnionCommittee.DAL.Audit.Extensions;
using TradeUnionCommittee.DAL.Extensions;
using TradeUnionCommittee.DAL.Identity.Extensions;
using TradeUnionCommittee.DataAnalysis.Service.Extensions;
using TradeUnionCommittee.PDF.Service.Extensions;

namespace TradeUnionCommittee.BLL.Extensions
{
    public static class ExtensionsServiceCollection
    {
        public static IServiceCollection AddTradeUnionCommitteeServiceModule(this IServiceCollection services,
                                                                                  ConnectionStrings connectionStrings,
                                                                                  CloudStorageConnection cloudStorageConnection,
                                                                                  RestConnection restConnection,
                                                                                  HashIdConfiguration setting)
        {
            // Injection Main, Identity, Audit, Cloud Storage, Context, HashIdConfiguration, AutoMapperConfiguration

            services.AddDbContext(connectionStrings.DefaultConnection);
            services.AddIdentityContext(connectionStrings.IdentityConnection);
            services.AddAuditDbContext(connectionStrings.AuditConnection);
            services.AddCloudStorageService(new CloudStorage.Service.Model.CloudStorageConnection
            {
                UseSsl = cloudStorageConnection.UseSsl,
                Url = cloudStorageConnection.Url,
                AccessKey = cloudStorageConnection.AccessKey,
                SecretKey = cloudStorageConnection.SecretKey,
            }, connectionStrings.CloudStorageConnection);
            services.AddDataAnalysisService(restConnection.DataAnalysisUrl);
            services.AddPdfService();

            HashHelper.Configure(setting);

            // Injection All Service
            //---------------------------------------------------------------------------------------------

            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IDashboardService, DashboardService>();

            services.AddTransient<ISearchService, SearchService>();
            services.AddTransient<ISystemAuditService, SystemAuditService>();
            services.AddTransient<IPdfService, PdfService>();

            services.AddTransient<IQualificationService, QualificationService>();
            services.AddTransient<IPositionService, PositionService>();
            services.AddTransient<ISocialActivityService, SocialActivityService>();
            services.AddTransient<IPrivilegesService, PrivilegesService>();
            services.AddTransient<IAwardService, AwardService>();
            services.AddTransient<IMaterialAidService, MaterialAidService>();
            services.AddTransient<IHobbyService, HobbyService>();
            services.AddTransient<ITravelService, TravelService>();
            services.AddTransient<IWellnessService, WellnessService>();
            services.AddTransient<ITourService, TourService>();
            services.AddTransient<IActivitiesService, ActivitiesService>();
            services.AddTransient<ICulturalService, CulturalService>();
            services.AddTransient<ISubdivisionsService, SubdivisionsService>();
            services.AddTransient<IDormitoryService, DormitoryService>();
            services.AddTransient<IDepartmentalService, DepartmentalService>();

            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<IPrivateHouseEmployeesService, PrivateHouseEmployeesService>();
            services.AddTransient<IPublicHouseEmployeesService, PublicHouseEmployeesService>();
            services.AddTransient<IPositionEmployeesService, PositionEmployeesService>();
            services.AddTransient<ISocialActivityEmployeesService, SocialActivityEmployeesService>();
            services.AddTransient<IPrivilegeEmployeesService, PrivilegeEmployeesService>();
            services.AddTransient<IAwardEmployeesService, AwardEmployeesService>();
            services.AddTransient<IMaterialAidEmployeesService, MaterialAidEmployeesService>();
            services.AddTransient<ITravelEmployeesService, TravelEmployeesService>();
            services.AddTransient<IWellnessEmployeesService, WellnessEmployeesService>();
            services.AddTransient<ITourEmployeesService, TourEmployeesService>();
            services.AddTransient<IActivityEmployeesService, ActivityEmployeesService>();
            services.AddTransient<ICulturalEmployeesService, CulturalEmployeesService>();
            services.AddTransient<IGiftEmployeesService, GiftEmployeesService>();
            services.AddTransient<IFluorographyEmployeesService, FluorographyEmployeesService>();
            services.AddTransient<IApartmentAccountingEmployeesService, ApartmentAccountingEmployeesService>();
            services.AddTransient<IHobbyEmployeesService, HobbyEmployeesService>();

            services.AddTransient<IFamilyService, FamilyService>();
            services.AddTransient<ITravelFamilyService, TravelFamilyService>();
            services.AddTransient<IWellnessFamilyService, WellnessFamilyService>();
            services.AddTransient<ITourFamilyService, TourFamilyService>();
            services.AddTransient<IActivityFamilyService, ActivityFamilyService>();
            services.AddTransient<ICulturalFamilyService, CulturalFamilyService>();

            services.AddTransient<IChildrenService, ChildrenService>();
            services.AddTransient<IHobbyChildrenService, HobbyChildrenService>();
            services.AddTransient<ITravelChildrenService, TravelChildrenService>();
            services.AddTransient<IWellnessChildrenService, WellnessChildrenService>();
            services.AddTransient<ITourChildrenService, TourChildrenService>();
            services.AddTransient<IActivityChildrenService, ActivityChildrenService>();
            services.AddTransient<ICulturalChildrenService, CulturalChildrenService>();
            services.AddTransient<IGiftChildrenService, GiftChildrenService>();

            services.AddTransient<IGrandChildrenService, GrandChildrenService>();
            services.AddTransient<IHobbyGrandChildrenService, HobbyGrandChildrenService>();
            services.AddTransient<ITravelGrandChildrenService, TravelGrandChildrenService>();
            services.AddTransient<ITourGrandChildrenService, TourGrandChildrenService>();
            services.AddTransient<IActivityGrandChildrenService, ActivityGrandChildrenService>();
            services.AddTransient<ICulturalGrandChildrenService, CulturalGrandChildrenService>();
            services.AddTransient<IGiftGrandChildrenService, GiftGrandChildrenService>();

            //---------------------------------------------------------------------------------------------

            return services;
        }
    }
}