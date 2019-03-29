using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.BLL.Extensions;
using TradeUnionCommittee.BLL.Interfaces.Search;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.Common.Enums;
using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Enums;
using TradeUnionCommittee.DAL.Native;

namespace TradeUnionCommittee.BLL.Services.Search
{
    public class SearchService : ISearchService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly ISearchNative _searchRepository;
        private readonly IHashIdConfiguration _hashIdUtilities;
        private readonly IAutoMapperConfiguration _mapperService;

        public SearchService(TradeUnionCommitteeContext context, IHashIdConfiguration hashIdUtilities, IAutoMapperConfiguration mapperService, ISearchNative searchRepository)
        {
            _context = context;
            _hashIdUtilities = hashIdUtilities;
            _mapperService = mapperService;
            _searchRepository = searchRepository;
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<IEnumerable<ResultSearchDTO>> SearchFullName(string fullName) => 
            _mapperService.Mapper.Map<IEnumerable<ResultSearchDTO>>(await _searchRepository.SearchByFullName(fullName, TrigramSearch.Gist));

        //------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<ActualResult<IEnumerable<ResultSearchDTO>>> SearchGender(string gender, string subdivision)
        {
            try
            {
                if (subdivision != null)
                {
                    var idSubdivision = _hashIdUtilities.DecryptLong(subdivision);
                    var searchByGenderAndSubdivision = await _context.Employee
                        .Include(x => x.PositionEmployees.IdSubdivisionNavigation.InverseIdSubordinateNavigation)
                        .Where(x => x.Sex == gender &&
                                    (x.PositionEmployees.IdSubdivisionNavigation.Id == idSubdivision ||
                                     x.PositionEmployees.IdSubdivisionNavigation.IdSubordinate == idSubdivision))
                        .ToListAsync();

                    return new ActualResult<IEnumerable<ResultSearchDTO>> { Result = ResultFormation(searchByGenderAndSubdivision) };
                }

                var searchByGender = await _context.Employee
                    .Include(x => x.PositionEmployees.IdSubdivisionNavigation.InverseIdSubordinateNavigation)
                    .Where(x => x.Sex == gender)
                    .ToListAsync();

                return new ActualResult<IEnumerable<ResultSearchDTO>> { Result = ResultFormation(searchByGender) };
            }
            catch (Exception)
            {
                return new ActualResult<IEnumerable<ResultSearchDTO>>(Errors.DataBaseError);
            }
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<ActualResult<IEnumerable<ResultSearchDTO>>> SearchPosition(string position, string subdivision)
        {
            var idPosition =_hashIdUtilities.DecryptLong(position);
            if (subdivision != null)
            {
                var idSubdivision = _hashIdUtilities.DecryptLong(subdivision);
                var searchByGenderAndSubdivision = await _context.Employee
                    .Include(x => x.PositionEmployees.IdSubdivisionNavigation.InverseIdSubordinateNavigation)
                    .Where(x => x.PositionEmployees.IdPosition == idPosition &&
                                (x.PositionEmployees.IdSubdivisionNavigation.Id == idSubdivision ||
                                 x.PositionEmployees.IdSubdivisionNavigation.IdSubordinate == idSubdivision))
                    .ToListAsync();
                return new ActualResult<IEnumerable<ResultSearchDTO>> { Result = ResultFormation(searchByGenderAndSubdivision) };
            }

            var searchByGender = await _context.Employee
                .Include(x => x.PositionEmployees.IdSubdivisionNavigation.InverseIdSubordinateNavigation)
                .Where(x => x.PositionEmployees.IdPosition == idPosition)
                .ToListAsync();
            return new ActualResult<IEnumerable<ResultSearchDTO>> { Result = ResultFormation(searchByGender) };
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<ActualResult<IEnumerable<ResultSearchDTO>>> SearchPrivilege(string privilege, string subdivision)
        {
            var idPrivilege = _hashIdUtilities.DecryptLong(privilege);
            if (subdivision != null)
            {
                var idSubdivision = _hashIdUtilities.DecryptLong(subdivision);
                var searchByGenderAndSubdivision = await _context.Employee
                    .Include(p => p.PositionEmployees.IdSubdivisionNavigation.InverseIdSubordinateNavigation)
                    .Include(p => p.PrivilegeEmployees)
                    .Where(x => x.PrivilegeEmployees.IdPrivileges == idPrivilege &&
                                (x.PositionEmployees.IdSubdivisionNavigation.Id == idSubdivision ||
                                 x.PositionEmployees.IdSubdivisionNavigation.IdSubordinate == idSubdivision))
                    .ToListAsync();
                return new ActualResult<IEnumerable<ResultSearchDTO>> { Result = ResultFormation(searchByGenderAndSubdivision) };
            }

            var searchByGender = await _context.Employee
                .Include(x => x.PositionEmployees.IdSubdivisionNavigation.InverseIdSubordinateNavigation)
                .Include(x => x.PrivilegeEmployees)
                .Where(x => x.PrivilegeEmployees.IdPrivileges == idPrivilege)
                .ToListAsync();
            return new ActualResult<IEnumerable<ResultSearchDTO>> { Result = ResultFormation(searchByGender) };
        }

        //------------------------------------------------------------------------------------------------------------------------------------------
        
        public async Task<ActualResult<IEnumerable<ResultSearchDTO>>> SearchAccommodation(AccommodationType type, string hashId)
        {
            var id = _hashIdUtilities.DecryptLong(hashId);
            switch (type)
            {
                case AccommodationType.Dormitory:
                case AccommodationType.Departmental:
                    var searchByPublicHouse = await _context.Employee
                        .Include(x => x.PositionEmployees.IdSubdivisionNavigation.InverseIdSubordinateNavigation)
                        .Include(x => x.PublicHouseEmployees)
                        .Where(x => x.PublicHouseEmployees.Any(t => t.IdAddressPublicHouse == id))
                        .ToListAsync();
                    return new ActualResult<IEnumerable<ResultSearchDTO>> { Result = ResultFormation(searchByPublicHouse) };
                case AccommodationType.FromUniversity:
                    var searchByFromUniversity = await _context.Employee
                        .Include(x => x.PositionEmployees.IdSubdivisionNavigation.InverseIdSubordinateNavigation)
                        .Include(x => x.PrivateHouseEmployees)
                        .Where(x => x.PrivateHouseEmployees.Any(t => t.DateReceiving != null))
                        .ToListAsync();
                    return new ActualResult<IEnumerable<ResultSearchDTO>> { Result = ResultFormation(searchByFromUniversity) };
                default:
                    return new ActualResult<IEnumerable<ResultSearchDTO>>();
            }
        }

        //------------------------------------------------------------------------------------------------------------------------------------------
        
        public async Task<ActualResult<IEnumerable<ResultSearchDTO>>> SearchBirthDate(CoverageType type, DateTime startDate, DateTime endDate)
        {
            switch (type)
            {
                case CoverageType.Employee:
                    var searchByEmployeeBirthDate = await _context.Employee
                        .Include(p => p.PositionEmployees.IdSubdivisionNavigation.InverseIdSubordinateNavigation)
                        .Where(x => x.BirthDate >= startDate && x.BirthDate <= endDate)
                        .ToListAsync();
                    return new ActualResult<IEnumerable<ResultSearchDTO>> { Result = ResultFormation(searchByEmployeeBirthDate) };
                case CoverageType.Children:
                    var searchByChildrenBirthDate = await _context.Employee
                        .Include(p => p.PositionEmployees.IdSubdivisionNavigation.InverseIdSubordinateNavigation)
                        .Include(p => p.Children)
                        .Where(x => x.Children.Any(t => t.BirthDate >= startDate && t.BirthDate <= endDate))
                        .ToListAsync();
                    return new ActualResult<IEnumerable<ResultSearchDTO>> { Result = ResultFormation(searchByChildrenBirthDate) };
                case CoverageType.GrandChildren:
                    var searchByGrandChildrenBirthDate = await _context.Employee
                        .Include(p => p.PositionEmployees.IdSubdivisionNavigation.InverseIdSubordinateNavigation)
                        .Include(p => p.GrandChildren)
                        .Where(x => x.GrandChildren.Any(t => t.BirthDate >= startDate && t.BirthDate <= endDate))
                        .ToListAsync();
                    return new ActualResult<IEnumerable<ResultSearchDTO>> { Result = ResultFormation(searchByGrandChildrenBirthDate) };
                default:
                    return new ActualResult<IEnumerable<ResultSearchDTO>>();
            }
        }

        //------------------------------------------------------------------------------------------------------------------------------------------
        
        public async Task<ActualResult<IEnumerable<ResultSearchDTO>>> SearchHobby(CoverageType type, string hobby)
        {
            var idHobby = _hashIdUtilities.DecryptLong(hobby);
            switch (type)
            {
                case CoverageType.Employee:
                    var searchByEmployeeHobby = await _context.Employee
                        .Include(p => p.PositionEmployees.IdSubdivisionNavigation.InverseIdSubordinateNavigation)
                        .Include(p => p.HobbyEmployees)
                        .Where(x => x.HobbyEmployees.Any(t => t.IdHobby == idHobby))
                        .ToListAsync();
                    return new ActualResult<IEnumerable<ResultSearchDTO>> { Result = ResultFormation(searchByEmployeeHobby) };
                case CoverageType.Children:
                    var searchByChildrenHobby = await _context.Children
                        .Include(p => p.HobbyChildrens)
                        .Include(p => p.IdEmployeeNavigation.PositionEmployees.IdSubdivisionNavigation.InverseIdSubordinateNavigation)
                        .Where(x => x.HobbyChildrens.Any(t => t.IdHobby == idHobby))
                        .Select(x => x.IdEmployeeNavigation)
                        .ToListAsync();
                    return new ActualResult<IEnumerable<ResultSearchDTO>> { Result = ResultFormation(searchByChildrenHobby) };
                case CoverageType.GrandChildren:
                    var searchByGrandChildrenHobby = await _context.GrandChildren
                        .Include(p => p.HobbyGrandChildrens)
                        .Include(p => p.IdEmployeeNavigation.PositionEmployees.IdSubdivisionNavigation.InverseIdSubordinateNavigation)
                        .Where(x => x.HobbyGrandChildrens.Any(t => t.IdHobby == idHobby))
                        .Select(x => x.IdEmployeeNavigation)
                        .ToListAsync();
                    return new ActualResult<IEnumerable<ResultSearchDTO>> { Result = ResultFormation(searchByGrandChildrenHobby) };
                default:
                    return new ActualResult<IEnumerable<ResultSearchDTO>>();
            }
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<ActualResult<string>> SearchEmployee(EmployeeType type, string value)
        {
            switch (type)
            {
                case EmployeeType.MobilePhone:
                    var searchByMobilePhone = await _context.Employee.FirstOrDefaultAsync(x => x.MobilePhone == value);
                    if (searchByMobilePhone != null)
                    {
                        return new ActualResult<string> { Result = _hashIdUtilities.EncryptLong(searchByMobilePhone.Id) };
                    }
                    return new ActualResult<string>(Errors.NotFound);
                case EmployeeType.CityPhone:
                    var searchByCityPhone = await _context.Employee.FirstOrDefaultAsync(x => x.CityPhone == value.AddMaskForCityPhone());
                    if (searchByCityPhone != null)
                    {
                        return new ActualResult<string> { Result = _hashIdUtilities.EncryptLong(searchByCityPhone.Id) };
                    }
                    return new ActualResult<string>(Errors.NotFound);
                case EmployeeType.IdentificationСode:
                    var searchByIdentificationСode = await _context.Employee.FirstOrDefaultAsync(x => x.IdentificationСode == value);
                    if (searchByIdentificationСode != null)
                    {
                        return new ActualResult<string> { Result = _hashIdUtilities.EncryptLong(searchByIdentificationСode.Id) };
                    }
                    return new ActualResult<string>(Errors.NotFound);
                case EmployeeType.MechnikovCard:
                    var searchByMechnikovCard = await _context.Employee.FirstOrDefaultAsync(x => x.MechnikovCard == value);
                    if (searchByMechnikovCard != null)
                    {
                        return new ActualResult<string> { Result = _hashIdUtilities.EncryptLong(searchByMechnikovCard.Id) };
                    }
                    return new ActualResult<string>(Errors.NotFound);
                default:
                    return new ActualResult<string>(Errors.NotFound);
            }
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        private IEnumerable<ResultSearchDTO> ResultFormation(IEnumerable<DAL.Entities.Employee> employees)
        {
            var result = new List<ResultSearchDTO>();

            foreach (var employee in employees)
            {
                string mainSubdivision;
                string mainSubdivisionAbbreviation;

                string subordinateSubdivision = null;
                string subordinateSubdivisionAbbreviation = null;

                if (employee.PositionEmployees.IdSubdivisionNavigation.IdSubordinateNavigation != null)
                {
                    mainSubdivision = employee.PositionEmployees.IdSubdivisionNavigation.IdSubordinateNavigation.Name;
                    mainSubdivisionAbbreviation = employee.PositionEmployees.IdSubdivisionNavigation.IdSubordinateNavigation.Abbreviation;
                    subordinateSubdivision = employee.PositionEmployees.IdSubdivisionNavigation.Name;
                    subordinateSubdivisionAbbreviation = employee.PositionEmployees.IdSubdivisionNavigation.Abbreviation;
                }
                else
                {
                    if (employee.PositionEmployees.IdSubdivisionNavigation.IdSubordinate != null)
                    {
                        var subdivision = Task.Run(async() => await _context.Subdivisions.FindAsync(employee.PositionEmployees.IdSubdivisionNavigation.IdSubordinate.Value));
                        mainSubdivision = subdivision.Result.Name;
                        mainSubdivisionAbbreviation = subdivision.Result.Abbreviation;
                        subordinateSubdivision = employee.PositionEmployees.IdSubdivisionNavigation.Name;
                        subordinateSubdivisionAbbreviation = employee.PositionEmployees.IdSubdivisionNavigation.Abbreviation;
                    }
                    else
                    {
                        mainSubdivision = employee.PositionEmployees.IdSubdivisionNavigation.Name;
                        mainSubdivisionAbbreviation = employee.PositionEmployees.IdSubdivisionNavigation.Abbreviation;
                    }
                }

                var patronymic = string.Empty;
                if (!string.IsNullOrEmpty(employee.Patronymic))
                {
                    patronymic = $"{employee.Patronymic[0]}.";
                }

                result.Add(new ResultSearchDTO
                {
                    HashIdUser = _hashIdUtilities.EncryptLong(employee.Id),
                    FullName = $"{employee.FirstName} {employee.SecondName} {employee.Patronymic}",
                    SurnameAndInitials = $"{employee.FirstName} {employee.SecondName[0]}. {patronymic}",
                    BirthDate = employee.BirthDate,
                    MobilePhone = employee.MobilePhone,
                    CityPhone = employee.CityPhone,
                    MainSubdivision = mainSubdivision,
                    MainSubdivisionAbbreviation = mainSubdivisionAbbreviation,
                    SubordinateSubdivision = subordinateSubdivision,
                    SubordinateSubdivisionAbbreviation = subordinateSubdivisionAbbreviation
                });
            }
            return result;
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}