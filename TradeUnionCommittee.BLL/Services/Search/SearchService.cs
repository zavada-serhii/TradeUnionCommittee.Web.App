using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.BLL.Interfaces.Search;
using TradeUnionCommittee.BLL.Utilities;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.Common.Enums;
using TradeUnionCommittee.DAL.Enums;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.BLL.Services.Search
{
    public class SearchService : ISearchService
    {
        private readonly IUnitOfWork _database;
        private readonly IHashIdUtilities _hashIdUtilities;

        public SearchService(IUnitOfWork database, IHashIdUtilities hashIdUtilities)
        {
            _database = database;
            _hashIdUtilities = hashIdUtilities;
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<ActualResult<IEnumerable<ResultSearchDTO>>> SearchFullName(string fullName)
        {
            var ids = await _database.SearchRepository.SearchByFullName(fullName, TrigramSearch.Gist);
            if (!ids.Any()) return new ActualResult<IEnumerable<ResultSearchDTO>>();

            var listEmployee = new List<DAL.Entities.Employee>();

            foreach (var id in ids)
            {
                var employees = await _database
                    .EmployeeRepository
                    .GetWithInclude(x => x.Id == id, p => p.PositionEmployees.IdSubdivisionNavigation.InverseIdSubordinateNavigation);
                listEmployee.Add(employees.Result.FirstOrDefault());
            }

            return new ActualResult<IEnumerable<ResultSearchDTO>> { Result = ResultFormation(listEmployee) };
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<ActualResult<IEnumerable<ResultSearchDTO>>> SearchGender(string gender, string subdivision)
        {
            if (subdivision != null)
            {
                var idSubdivision = _hashIdUtilities.DecryptLong(subdivision, Enums.Services.Subdivision);

                var searchByGenderAndSubdivision = await _database
                    .EmployeeRepository
                    .GetWithInclude(x => x.Sex == gender &&
                                    (x.PositionEmployees.IdSubdivisionNavigation.Id == idSubdivision ||
                                    x.PositionEmployees.IdSubdivisionNavigation.IdSubordinate == idSubdivision), 
                                    p => p.PositionEmployees.IdSubdivisionNavigation.InverseIdSubordinateNavigation);

                return new ActualResult<IEnumerable<ResultSearchDTO>> { Result = ResultFormation(searchByGenderAndSubdivision.Result) };
            }

            var searchByGender = await _database
                .EmployeeRepository
                .GetWithInclude(x => x.Sex == gender, p => p.PositionEmployees.IdSubdivisionNavigation.InverseIdSubordinateNavigation);

            return new ActualResult<IEnumerable<ResultSearchDTO>> {Result = ResultFormation(searchByGender.Result)};
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<ActualResult<IEnumerable<ResultSearchDTO>>> SearchPosition(string position, string subdivision)
        {
            var idPosition =_hashIdUtilities.DecryptLong(position, Enums.Services.Position);

            if (subdivision != null)
            {
                var idSubdivision = _hashIdUtilities.DecryptLong(subdivision, Enums.Services.Subdivision);

                var searchByGenderAndSubdivision = await _database
                    .EmployeeRepository
                    .GetWithInclude(x => x.PositionEmployees.IdPosition == idPosition &&
                                         (x.PositionEmployees.IdSubdivisionNavigation.Id == idSubdivision ||
                                         x.PositionEmployees.IdSubdivisionNavigation.IdSubordinate == idSubdivision),
                                    p => p.PositionEmployees.IdSubdivisionNavigation.InverseIdSubordinateNavigation);

                return new ActualResult<IEnumerable<ResultSearchDTO>> { Result = ResultFormation(searchByGenderAndSubdivision.Result) };
            }

            var searchByGender = await _database
                .EmployeeRepository
                .GetWithInclude(x => x.PositionEmployees.IdPosition == idPosition, 
                                p => p.PositionEmployees.IdSubdivisionNavigation.InverseIdSubordinateNavigation);

            return new ActualResult<IEnumerable<ResultSearchDTO>> { Result = ResultFormation(searchByGender.Result) };
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<ActualResult<IEnumerable<ResultSearchDTO>>> SearchPrivilege(string privilege, string subdivision)
        {
            var idPrivilege = _hashIdUtilities.DecryptLong(privilege, Enums.Services.Privileges);

            if (subdivision != null)
            {
                var idSubdivision = _hashIdUtilities.DecryptLong(subdivision, Enums.Services.Subdivision);

                var searchByGenderAndSubdivision = await _database
                    .EmployeeRepository
                    .GetWithInclude(x => x.PrivilegeEmployees.IdPrivileges == idPrivilege &&
                                         (x.PositionEmployees.IdSubdivisionNavigation.Id == idSubdivision ||
                                         x.PositionEmployees.IdSubdivisionNavigation.IdSubordinate == idSubdivision),
                                    p => p.PositionEmployees.IdSubdivisionNavigation.InverseIdSubordinateNavigation,
                                    p => p.PrivilegeEmployees);

                return new ActualResult<IEnumerable<ResultSearchDTO>> { Result = ResultFormation(searchByGenderAndSubdivision.Result) };
            }

            var searchByGender = await _database
                .EmployeeRepository
                .GetWithInclude(x => x.PrivilegeEmployees.IdPrivileges == idPrivilege,
                                p => p.PositionEmployees.IdSubdivisionNavigation.InverseIdSubordinateNavigation,
                                p => p.PrivilegeEmployees);

            return new ActualResult<IEnumerable<ResultSearchDTO>> { Result = ResultFormation(searchByGender.Result) };
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        
        public async Task<ActualResult<IEnumerable<ResultSearchDTO>>> SearchAccommodation(AccommodationType type, string dormitory, string departmental)
        {
            switch (type)
            {
                case AccommodationType.Dormitory:
                    var idDormitory = _hashIdUtilities.DecryptLong(dormitory, Enums.Services.Dormitory);
                    var searchByDormitory = await _database
                        .EmployeeRepository
                        .GetWithInclude(x => x.PublicHouseEmployees.Any(t => t.IdAddressPublicHouse == idDormitory),
                                        p => p.PositionEmployees.IdSubdivisionNavigation.InverseIdSubordinateNavigation,
                                        p => p.PublicHouseEmployees);
                    return new ActualResult<IEnumerable<ResultSearchDTO>> { Result = ResultFormation(searchByDormitory.Result) };
                case AccommodationType.Departmental:
                    var idDepartmental = _hashIdUtilities.DecryptLong(departmental, Enums.Services.Departmental);
                    var searchByDepartmental = await _database
                        .EmployeeRepository
                        .GetWithInclude(x => x.PublicHouseEmployees.Any(t => t.IdAddressPublicHouse == idDepartmental),
                                        p => p.PositionEmployees.IdSubdivisionNavigation.InverseIdSubordinateNavigation,
                                        p => p.PublicHouseEmployees);
                    return new ActualResult<IEnumerable<ResultSearchDTO>> { Result = ResultFormation(searchByDepartmental.Result) };
                case AccommodationType.FromUniversity:
                    var searchByFromUniversity = await _database
                        .EmployeeRepository
                        .GetWithInclude(x => x.PrivateHouseEmployees.Any(t => t.DateReceiving != null),
                                        p => p.PositionEmployees.IdSubdivisionNavigation.InverseIdSubordinateNavigation,
                                        p => p.PrivateHouseEmployees);
                    return new ActualResult<IEnumerable<ResultSearchDTO>> { Result = ResultFormation(searchByFromUniversity.Result) };
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
                    var searchByEmployeeBirthDate = await _database
                        .EmployeeRepository
                        .GetWithInclude(x => x.BirthDate >= startDate && x.BirthDate <= endDate,
                                        p => p.PositionEmployees.IdSubdivisionNavigation.InverseIdSubordinateNavigation);
                    return new ActualResult<IEnumerable<ResultSearchDTO>> { Result = ResultFormation(searchByEmployeeBirthDate.Result) };
                case CoverageType.Children:
                    var searchByChildrenBirthDate = await _database
                        .EmployeeRepository
                        .GetWithInclude(x => x.Children.Any(t => t.BirthDate >= startDate && t.BirthDate <= endDate),
                                        p => p.PositionEmployees.IdSubdivisionNavigation.InverseIdSubordinateNavigation,
                                        p => p.Children);
                    return new ActualResult<IEnumerable<ResultSearchDTO>> { Result = ResultFormation(searchByChildrenBirthDate.Result) };
                case CoverageType.GrandChildren:
                    var searchByGrandChildrenBirthDate = await _database
                        .EmployeeRepository
                        .GetWithInclude(x => x.GrandChildren.Any(t => t.BirthDate >= startDate && t.BirthDate <= endDate),
                                        p => p.PositionEmployees.IdSubdivisionNavigation.InverseIdSubordinateNavigation,
                                        p => p.GrandChildren);
                    return new ActualResult<IEnumerable<ResultSearchDTO>> { Result = ResultFormation(searchByGrandChildrenBirthDate.Result) };
                default:
                    return new ActualResult<IEnumerable<ResultSearchDTO>>();
            }
        }

        //------------------------------------------------------------------------------------------------------------------------------------------
        
        public async Task<ActualResult<IEnumerable<ResultSearchDTO>>> SearchHobby(CoverageType type, string hobby)
        {
            var idHobby = _hashIdUtilities.DecryptLong(hobby, Enums.Services.Hobby);

            switch (type)
            {
                case CoverageType.Employee:
                    var searchByEmployeeHobby = await _database
                        .EmployeeRepository
                        .GetWithInclude(x => x.HobbyEmployees.Any(t => t.IdHobby == idHobby),
                                        p => p.PositionEmployees.IdSubdivisionNavigation.InverseIdSubordinateNavigation,
                                        p => p.HobbyEmployees);
                    return new ActualResult<IEnumerable<ResultSearchDTO>> { Result = ResultFormation(searchByEmployeeHobby.Result) };
                case CoverageType.Children:
                    var searchByChildrenHobby = await _database
                        .ChildrenRepository
                        .GetWithInclude(x => x.HobbyChildrens.Any(t => t.IdHobby == idHobby),
                                        p => p.HobbyChildrens,
                                        p => p.IdEmployeeNavigation.PositionEmployees.IdSubdivisionNavigation.InverseIdSubordinateNavigation);
                    return new ActualResult<IEnumerable<ResultSearchDTO>> { Result = ResultFormation(searchByChildrenHobby.Result.Select(x => x.IdEmployeeNavigation)) };

                case CoverageType.GrandChildren:
                    var searchByGrandChildrenHobby = await _database
                        .GrandChildrenRepository
                        .GetWithInclude(x => x.HobbyGrandChildrens.Any(t => t.IdHobby == idHobby),
                                        p => p.HobbyGrandChildrens,
                                        p => p.IdEmployeeNavigation.PositionEmployees.IdSubdivisionNavigation.InverseIdSubordinateNavigation);
                    return new ActualResult<IEnumerable<ResultSearchDTO>> { Result = ResultFormation(searchByGrandChildrenHobby.Result.Select(x => x.IdEmployeeNavigation)) };
                default:
                    return new ActualResult<IEnumerable<ResultSearchDTO>>();
            }
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<ActualResult<long>> SearchEmployee(EmployeeType type, string value)
        {
            switch (type)
            {
                case EmployeeType.MobilePhone:
                    var searchByMobilePhone = await _database.EmployeeRepository.Find(x => x.MobilePhone == value);
                    var firstOrDefaultMobilePhone = searchByMobilePhone.Result.FirstOrDefault();
                    if (firstOrDefaultMobilePhone != null)
                    {
                        return new ActualResult<long> { Result = firstOrDefaultMobilePhone.Id };
                    }
                    return new ActualResult<long>(Errors.NotFound);

                case EmployeeType.CityPhone:
                    var searchByCityPhone = await _database.EmployeeRepository.Find(x => x.CityPhone == value);
                    var firstOrDefaultCityPhone = searchByCityPhone.Result.FirstOrDefault();
                    if (firstOrDefaultCityPhone != null)
                    {
                        return new ActualResult<long> { Result = firstOrDefaultCityPhone.Id };
                    }
                    return new ActualResult<long>(Errors.NotFound);

                case EmployeeType.IdentificationСode:
                    var searchByIdentificationСode = await _database.EmployeeRepository.Find(x => x.IdentificationСode == value);
                    var firstOrDefaultIdentificationСode = searchByIdentificationСode.Result.FirstOrDefault();
                    if (firstOrDefaultIdentificationСode != null)
                    {
                        return new ActualResult<long> { Result = firstOrDefaultIdentificationСode.Id };
                    }
                    return new ActualResult<long>(Errors.NotFound);

                case EmployeeType.MechnikovCard:
                    var searchByMechnikovCard = await _database.EmployeeRepository.Find(x => x.MechnikovCard == value);
                    var firstOrDefaultMechnikovCard = searchByMechnikovCard.Result.FirstOrDefault();
                    if (firstOrDefaultMechnikovCard != null)
                    {
                        return new ActualResult<long> { Result = firstOrDefaultMechnikovCard.Id };
                    }
                    return new ActualResult<long>(Errors.NotFound);

                default:
                    return new ActualResult<long>(Errors.NotFound);
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
                        var subdivision = Task.Run(async() => await _database.SubdivisionsRepository.Get(employee.PositionEmployees.IdSubdivisionNavigation.IdSubordinate.Value));
                        mainSubdivision = subdivision.Result.Result.Name;
                        mainSubdivisionAbbreviation = subdivision.Result.Result.Abbreviation;
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
                    HashIdUser = _hashIdUtilities.EncryptLong(employee.Id, Enums.Services.Employee),
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
            _database.Dispose();
        }
    }
}