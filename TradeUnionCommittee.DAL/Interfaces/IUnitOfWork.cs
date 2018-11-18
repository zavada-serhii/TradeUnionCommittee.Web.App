using System;
using System.Threading.Tasks;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Employee> EmployeeRepository { get; }
        IRepository<Children> ChildrenRepository { get; }
        IRepository<GrandChildren> GrandChildrenRepository { get; }
        IRepository<Family> FamilyRepository { get; }

        //------------------------------------------------------------------------------------------------------------------------------------------

        IRepository<Award> AwardRepository { get; }
        IRepository<MaterialAid> MaterialAidRepository { get; }
        IRepository<Hobby> HobbyRepository { get; }
        IRepository<Event> EventRepository { get; }
        IRepository<Cultural> CulturalRepository { get; }
        IRepository<Activities> ActivitiesRepository { get; }
        IRepository<Privileges> PrivilegesRepository { get; }
        IRepository<SocialActivity> SocialActivityRepository { get; }
        IRepository<Position> PositionRepository { get; }
        IRepository<Subdivisions> SubdivisionsRepository { get; }
        IRepository<AddressPublicHouse> AddressPublicHouseRepository { get; }

        //------------------------------------------------------------------------------------------------------------------------------------------

        IRepository<AwardEmployees> AwardEmployeesRepository { get; }
        IRepository<MaterialAidEmployees> MaterialAidEmployeesRepository { get; }
        IRepository<HobbyEmployees> HobbyEmployeesRepository { get; }
        IRepository<FluorographyEmployees> FluorographyEmployeesRepository { get; }
        IRepository<EventEmployees> EventEmployeesRepository { get; }
        IRepository<CulturalEmployees> CulturalEmployeesRepository { get; }
        IRepository<ActivityEmployees> ActivityEmployeesRepository { get; }
        IRepository<GiftEmployees> GiftEmployeesRepository { get; }
        IRepository<PrivilegeEmployees> PrivilegeEmployeesRepository { get; }
        IRepository<SocialActivityEmployees> SocialActivityEmployeesRepository { get; }
        IRepository<PositionEmployees> PositionEmployeesRepository { get; }
        IRepository<PublicHouseEmployees> PublicHouseEmployeesRepository { get; }
        IRepository<PrivateHouseEmployees> PrivateHouseEmployeesRepository { get; }
        IRepository<ApartmentAccountingEmployees> ApartmentAccountingEmployeesRepository { get; }
        IRepository<EventChildrens> EventChildrensRepository { get; }
        IRepository<CulturalChildrens> CulturalChildrensRepository { get; }
        IRepository<HobbyChildrens> HobbyChildrensRepository { get; }
        IRepository<ActivityChildrens> ActivityChildrensRepository { get; }
        IRepository<GiftChildrens> GiftChildrensRepository { get; }
        IRepository<EventGrandChildrens> EventGrandChildrensRepository { get; }
        IRepository<CulturalGrandChildrens> CulturalGrandChildrensRepository { get; }
        IRepository<HobbyGrandChildrens> HobbyGrandChildrensRepository { get; }
        IRepository<ActivityGrandChildrens> ActivityGrandChildrensRepository { get; }
        IRepository<GiftGrandChildrens> GiftGrandChildrensRepository { get; }
        IRepository<EventFamily> EventFamilyRepository { get; }
        IRepository<CulturalFamily> CulturalFamilyRepository { get; }
        IRepository<ActivityFamily> ActivityFamilyRepository { get; }

        //------------------------------------------------------------------------------------------------------------------------------------------
        
        IAccountRepository AccountRepository { get; }
        ISearchRepository SearchRepository { get; }
        ISystemAuditRepository SystemAuditRepository { get; }

        //------------------------------------------------------------------------------------------------------------------------------------------

        Task<ActualResult> SaveAsync();
    }
}