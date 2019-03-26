using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.Common.Enums;
using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Extensions;
using TradeUnionCommittee.DAL.Interfaces;
using TradeUnionCommittee.DAL.Repositories.Account;
using TradeUnionCommittee.DAL.Repositories.Directories;
using TradeUnionCommittee.DAL.Repositories.General;
using TradeUnionCommittee.DAL.Repositories.Lists;
using TradeUnionCommittee.DAL.Repositories.Search;
using TradeUnionCommittee.DAL.Repositories.SystemAudit;

namespace TradeUnionCommittee.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UnitOfWork(TradeUnionCommitteeContext context, UserManager<User> userManager,
            SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        //------------------------------------------------------------------------------------------------------------------------------------------
        
        private AccountRepository _accountRepository;
        private SearchRepository _searchRepository;
        private SystemAuditRepository _systemAuditRepository;

        //------------------------------------------------------------------------------------------------------------------------------------------

        private EmployeeRepository _employeeRepository;
        private ChildrenRepository _childrenRepository;
        private GrandChildrenRepository _grandChildrenRepository;
        private FamilyRepository _familyRepository;

        //------------------------------------------------------------------------------------------------------------------------------------------

        private AwardRepository _awardRepository;
        private MaterialAidRepository _materialAidRepository;
        private HobbyRepository _hobbyRepository;
        private EventRepository _eventRepository;
        private CulturalRepository _culturalRepository;
        private ActivitiesRepository _activitiesRepository;
        private PrivilegesRepository _privilegesRepository;
        private SocialActivityRepository _socialActivityRepository;
        private PositionRepository _positionRepository;
        private SubdivisionsRepository _subdivisionsRepository;
        private AddressPublicHouseRepository _addressPublicHouseRepository;

        //------------------------------------------------------------------------------------------------------------------------------------------

        private AwardEmployeesRepository _awardEmployeesRepository;
        private MaterialAidEmployeesRepository _materialAidEmployeesRepository;
        private HobbyEmployeesRepository _hobbyEmployeesRepository;
        private FluorographyEmployeesRepository _fluorographyEmployeesRepository;
        private EventEmployeesRepository _eventEmployeesRepository;
        private CulturalEmployeesRepository _culturalEmployeesRepository;
        private ActivityEmployeesRepository _activityEmployeesRepository;
        private GiftEmployeesRepository _giftEmployeesRepository;
        private PrivilegeEmployeesRepository _privilegeEmployeesRepository;
        private SocialActivityEmployeesRepository _socialActivityEmployeesRepository;
        private PositionEmployeesRepository _positionEmployeesRepository;
        private PublicHouseEmployeesRepository _publicHouseEmployeesRepository;
        private PrivateHouseEmployeesRepository _privateHouseEmployeesRepository;
        private ApartmentAccountingEmployeesRepository _apartmentAccountingEmployeesRepository;
        private EventChildrensRepository _eventChildrensRepository;
        private CulturalChildrensRepository _culturalChildrensRepository;
        private HobbyChildrensRepository _hobbyChildrensRepository;
        private ActivityChildrensRepository _activityChildrensRepository;
        private GiftChildrensRepository _giftChildrensRepository;
        private EventGrandChildrensRepository _eventGrandChildrensRepository;
        private CulturalGrandChildrensRepository _culturalGrandChildrensRepository;
        private HobbyGrandChildrensRepository _hobbyGrandChildrensRepository;
        private ActivityGrandChildrensRepository _activityGrandChildrensRepository;
        private GiftGrandChildrensRepository _giftGrandChildrensRepository;
        private EventFamilyRepository _eventFamilyRepository;
        private CulturalFamilyRepository _culturalFamilyRepository;
        private ActivityFamilyRepository _activityFamilyRepository;

        //------------------------------------------------------------------------------------------------------------------------------------------

        public IAccountRepository AccountRepository => _accountRepository ?? (_accountRepository = new AccountRepository(_userManager, _signInManager, _roleManager));
        public ISearchRepository SearchRepository => _searchRepository ?? (_searchRepository = new SearchRepository(_context));
        public ISystemAuditRepository SystemAuditRepository => _systemAuditRepository ?? (_systemAuditRepository = new SystemAuditRepository(_context));

        //------------------------------------------------------------------------------------------------------------------------------------------

        public IRepository<Employee> EmployeeRepository => _employeeRepository ?? (_employeeRepository = new EmployeeRepository(_context));
        public IRepository<Children> ChildrenRepository => _childrenRepository ?? (_childrenRepository = new ChildrenRepository(_context));
        public IRepository<GrandChildren> GrandChildrenRepository => _grandChildrenRepository ?? (_grandChildrenRepository = new GrandChildrenRepository(_context));
        public IRepository<Family> FamilyRepository => _familyRepository ?? (_familyRepository = new FamilyRepository(_context));

        //------------------------------------------------------------------------------------------------------------------------------------------

        public IRepository<Award> AwardRepository => _awardRepository ?? (_awardRepository = new AwardRepository(_context));
        public IRepository<MaterialAid> MaterialAidRepository => _materialAidRepository ?? (_materialAidRepository = new MaterialAidRepository(_context));
        public IRepository<Hobby> HobbyRepository => _hobbyRepository ?? (_hobbyRepository = new HobbyRepository(_context));
        public IRepository<Event> EventRepository => _eventRepository ?? (_eventRepository = new EventRepository(_context));
        public IRepository<Cultural> CulturalRepository => _culturalRepository ?? (_culturalRepository = new CulturalRepository(_context));
        public IRepository<Activities> ActivitiesRepository => _activitiesRepository ?? (_activitiesRepository = new ActivitiesRepository(_context));
        public IRepository<Privileges> PrivilegesRepository => _privilegesRepository ?? (_privilegesRepository = new PrivilegesRepository(_context));
        public IRepository<SocialActivity> SocialActivityRepository => _socialActivityRepository ?? (_socialActivityRepository = new SocialActivityRepository(_context));
        public IRepository<Position> PositionRepository => _positionRepository ?? (_positionRepository = new PositionRepository(_context));
        public IRepository<Subdivisions> SubdivisionsRepository => _subdivisionsRepository ?? (_subdivisionsRepository = new SubdivisionsRepository(_context));
        public IRepository<AddressPublicHouse> AddressPublicHouseRepository => _addressPublicHouseRepository ?? (_addressPublicHouseRepository = new AddressPublicHouseRepository(_context));

        //------------------------------------------------------------------------------------------------------------------------------------------
        
        public IRepository<AwardEmployees> AwardEmployeesRepository => _awardEmployeesRepository ?? (_awardEmployeesRepository = new AwardEmployeesRepository(_context));
        public IRepository<MaterialAidEmployees> MaterialAidEmployeesRepository => _materialAidEmployeesRepository ?? (_materialAidEmployeesRepository = new MaterialAidEmployeesRepository(_context));
        public IRepository<HobbyEmployees> HobbyEmployeesRepository => _hobbyEmployeesRepository ?? (_hobbyEmployeesRepository = new HobbyEmployeesRepository(_context));
        public IRepository<FluorographyEmployees> FluorographyEmployeesRepository => _fluorographyEmployeesRepository ?? (_fluorographyEmployeesRepository = new FluorographyEmployeesRepository(_context));
        public IRepository<EventEmployees> EventEmployeesRepository => _eventEmployeesRepository ?? (_eventEmployeesRepository = new EventEmployeesRepository(_context));
        public IRepository<CulturalEmployees> CulturalEmployeesRepository => _culturalEmployeesRepository ?? (_culturalEmployeesRepository = new CulturalEmployeesRepository(_context));
        public IRepository<ActivityEmployees> ActivityEmployeesRepository => _activityEmployeesRepository ?? (_activityEmployeesRepository = new ActivityEmployeesRepository(_context));
        public IRepository<GiftEmployees> GiftEmployeesRepository => _giftEmployeesRepository ?? (_giftEmployeesRepository = new GiftEmployeesRepository(_context));
        public IRepository<PrivilegeEmployees> PrivilegeEmployeesRepository => _privilegeEmployeesRepository ?? (_privilegeEmployeesRepository = new PrivilegeEmployeesRepository(_context));
        public IRepository<SocialActivityEmployees> SocialActivityEmployeesRepository => _socialActivityEmployeesRepository ?? (_socialActivityEmployeesRepository = new SocialActivityEmployeesRepository(_context));
        public IRepository<PositionEmployees> PositionEmployeesRepository => _positionEmployeesRepository ?? (_positionEmployeesRepository = new PositionEmployeesRepository(_context));
        public IRepository<PublicHouseEmployees> PublicHouseEmployeesRepository => _publicHouseEmployeesRepository ?? (_publicHouseEmployeesRepository = new PublicHouseEmployeesRepository(_context));
        public IRepository<PrivateHouseEmployees> PrivateHouseEmployeesRepository => _privateHouseEmployeesRepository ?? (_privateHouseEmployeesRepository = new PrivateHouseEmployeesRepository(_context));
        public IRepository<ApartmentAccountingEmployees> ApartmentAccountingEmployeesRepository => _apartmentAccountingEmployeesRepository ?? (_apartmentAccountingEmployeesRepository = new ApartmentAccountingEmployeesRepository(_context));
        public IRepository<EventChildrens> EventChildrensRepository => _eventChildrensRepository ?? (_eventChildrensRepository = new EventChildrensRepository(_context));
        public IRepository<CulturalChildrens> CulturalChildrensRepository => _culturalChildrensRepository ?? (_culturalChildrensRepository = new CulturalChildrensRepository(_context));
        public IRepository<HobbyChildrens> HobbyChildrensRepository => _hobbyChildrensRepository ?? (_hobbyChildrensRepository = new HobbyChildrensRepository(_context));
        public IRepository<ActivityChildrens> ActivityChildrensRepository => _activityChildrensRepository ?? (_activityChildrensRepository = new ActivityChildrensRepository(_context));
        public IRepository<GiftChildrens> GiftChildrensRepository => _giftChildrensRepository ?? (_giftChildrensRepository = new GiftChildrensRepository(_context));
        public IRepository<EventGrandChildrens> EventGrandChildrensRepository => _eventGrandChildrensRepository ?? (_eventGrandChildrensRepository = new EventGrandChildrensRepository(_context));
        public IRepository<CulturalGrandChildrens> CulturalGrandChildrensRepository => _culturalGrandChildrensRepository ?? (_culturalGrandChildrensRepository = new CulturalGrandChildrensRepository(_context));
        public IRepository<HobbyGrandChildrens> HobbyGrandChildrensRepository => _hobbyGrandChildrensRepository ?? (_hobbyGrandChildrensRepository = new HobbyGrandChildrensRepository(_context));
        public IRepository<ActivityGrandChildrens> ActivityGrandChildrensRepository => _activityGrandChildrensRepository ?? (_activityGrandChildrensRepository = new ActivityGrandChildrensRepository(_context));
        public IRepository<GiftGrandChildrens> GiftGrandChildrensRepository => _giftGrandChildrensRepository ?? (_giftGrandChildrensRepository = new GiftGrandChildrensRepository(_context));
        public IRepository<EventFamily> EventFamilyRepository => _eventFamilyRepository ?? (_eventFamilyRepository = new EventFamilyRepository(_context));
        public IRepository<CulturalFamily> CulturalFamilyRepository => _culturalFamilyRepository ?? (_culturalFamilyRepository = new CulturalFamilyRepository(_context));
        public IRepository<ActivityFamily> ActivityFamilyRepository => _activityFamilyRepository ?? (_activityFamilyRepository = new ActivityFamilyRepository(_context));
        
        //------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<ActualResult> SaveAsync()
        {
            try
            {
               await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entity = ex.Entries.Single().GetDatabaseValues();
                if (entity == null)
                {
                    _context.UndoChanges();
                    return new ActualResult(Errors.TupleDeleted);
                }
                var data = ex.Entries.Single();
                data.OriginalValues.SetValues(data.GetDatabaseValues());
                return new ActualResult(Errors.TupleUpdated);
            }
            catch (Exception e)
            {
                _context.UndoChanges();
                return new ActualResult(e.Message);
            }
            return new ActualResult();
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        private bool _disposed;

        private void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}