using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.BLL.Extensions;
using TradeUnionCommittee.BLL.Helpers;
using TradeUnionCommittee.BLL.Interfaces.Search;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.Common.Enums;
using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Enums;
using TradeUnionCommittee.DAL.Repository;

namespace TradeUnionCommittee.BLL.Services.Search
{
    internal class SearchService : ISearchService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly ITrigramSearchRepository _searchRepository;
        private readonly HashIdConfiguration _hashIdUtilities;
        private readonly AutoMapperConfiguration _mapperService;

        public SearchService(TradeUnionCommitteeContext context, HashIdConfiguration hashIdUtilities, AutoMapperConfiguration mapperService, ITrigramSearchRepository searchRepository)
        {
            _context = context;
            _hashIdUtilities = hashIdUtilities;
            _mapperService = mapperService;
            _searchRepository = searchRepository;
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<IEnumerable<ResultSearchDTO>> SearchFullName(string fullName) => 
            _mapperService.Mapper.Map<IEnumerable<ResultSearchDTO>>(await _searchRepository.SearchByFullName(fullName, TypeTrigram.Gist));

        //------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<ActualResult<IEnumerable<ResultSearchDTO>>> SearchGender(string gender, string subdivision)
        {
            try
            {
                return await Task.Run(() =>
                {
                    if (subdivision != null)
                    {
                        var idSubdivision = _hashIdUtilities.DecryptLong(subdivision);
                        var searchByGenderAndSubdivision = _context.Employee
                            .Include(x => x.PositionEmployees.IdSubdivisionNavigation.InverseIdSubordinateNavigation)
                            .Where(x => x.Sex == gender &&
                                        (x.PositionEmployees.IdSubdivisionNavigation.Id == idSubdivision ||
                                         x.PositionEmployees.IdSubdivisionNavigation.IdSubordinate == idSubdivision));
                        return new ActualResult<IEnumerable<ResultSearchDTO>> { Result = ResultFormation(searchByGenderAndSubdivision) };
                    }
                    var searchByGender = _context.Employee
                        .Include(x => x.PositionEmployees.IdSubdivisionNavigation.InverseIdSubordinateNavigation)
                        .Where(x => x.Sex == gender);
                    return new ActualResult<IEnumerable<ResultSearchDTO>> { Result = ResultFormation(searchByGender) };
                });
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<ResultSearchDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<ActualResult<IEnumerable<ResultSearchDTO>>> SearchPosition(string position, string subdivision)
        {
            try
            {
                return await Task.Run(() =>
                {
                    var idPosition = _hashIdUtilities.DecryptLong(position);
                    if (subdivision != null)
                    {
                        var idSubdivision = _hashIdUtilities.DecryptLong(subdivision);
                        var searchByGenderAndSubdivision = _context.Employee
                            .Include(x => x.PositionEmployees.IdSubdivisionNavigation.InverseIdSubordinateNavigation)
                            .Where(x => x.PositionEmployees.IdPosition == idPosition &&
                                        (x.PositionEmployees.IdSubdivisionNavigation.Id == idSubdivision ||
                                         x.PositionEmployees.IdSubdivisionNavigation.IdSubordinate == idSubdivision));
                        return new ActualResult<IEnumerable<ResultSearchDTO>> { Result = ResultFormation(searchByGenderAndSubdivision) };
                    }
                    var searchByGender = _context.Employee
                        .Include(x => x.PositionEmployees.IdSubdivisionNavigation.InverseIdSubordinateNavigation)
                        .Where(x => x.PositionEmployees.IdPosition == idPosition);
                    return new ActualResult<IEnumerable<ResultSearchDTO>> { Result = ResultFormation(searchByGender) };
                });
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<ResultSearchDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<ActualResult<IEnumerable<ResultSearchDTO>>> SearchPrivilege(string privilege, string subdivision)
        {
            try
            {
                return await Task.Run(() =>
                {
                    var idPrivilege = _hashIdUtilities.DecryptLong(privilege);
                    if (subdivision != null)
                    {
                        var idSubdivision = _hashIdUtilities.DecryptLong(subdivision);
                        var searchByGenderAndSubdivision = _context.Employee
                            .Include(p => p.PositionEmployees.IdSubdivisionNavigation.InverseIdSubordinateNavigation)
                            .Include(p => p.PrivilegeEmployees)
                            .Where(x => x.PrivilegeEmployees.IdPrivileges == idPrivilege &&
                                        (x.PositionEmployees.IdSubdivisionNavigation.Id == idSubdivision ||
                                         x.PositionEmployees.IdSubdivisionNavigation.IdSubordinate == idSubdivision));
                        return new ActualResult<IEnumerable<ResultSearchDTO>> { Result = ResultFormation(searchByGenderAndSubdivision) };
                    }
                    var searchByGender = _context.Employee
                        .Include(x => x.PositionEmployees.IdSubdivisionNavigation.InverseIdSubordinateNavigation)
                        .Include(x => x.PrivilegeEmployees)
                        .Where(x => x.PrivilegeEmployees.IdPrivileges == idPrivilege);
                    return new ActualResult<IEnumerable<ResultSearchDTO>> { Result = ResultFormation(searchByGender) };
                });
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<ResultSearchDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        //------------------------------------------------------------------------------------------------------------------------------------------
        
        public async Task<ActualResult<IEnumerable<ResultSearchDTO>>> SearchAccommodation(AccommodationType type, string hashId)
        {
            try
            {
                return await Task.Run(() =>
                {
                    var id = _hashIdUtilities.DecryptLong(hashId);
                    switch (type)
                    {
                        case AccommodationType.Dormitory:
                        case AccommodationType.Departmental:
                            var searchByPublicHouse = _context.Employee
                                .Include(x => x.PositionEmployees.IdSubdivisionNavigation.InverseIdSubordinateNavigation)
                                .Include(x => x.PublicHouseEmployees)
                                .Where(x => x.PublicHouseEmployees.AsQueryable().Any(t => t.IdAddressPublicHouse == id));
                            return new ActualResult<IEnumerable<ResultSearchDTO>> { Result = ResultFormation(searchByPublicHouse) };
                        case AccommodationType.FromUniversity:
                            var searchByFromUniversity = _context.Employee
                                .Include(x => x.PositionEmployees.IdSubdivisionNavigation.InverseIdSubordinateNavigation)
                                .Include(x => x.PrivateHouseEmployees)
                                .Where(x => x.PrivateHouseEmployees.AsQueryable().Any(t => t.DateReceiving != null));
                            return new ActualResult<IEnumerable<ResultSearchDTO>> { Result = ResultFormation(searchByFromUniversity) };
                        default:
                            return new ActualResult<IEnumerable<ResultSearchDTO>>();
                    }
                });
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<ResultSearchDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        //------------------------------------------------------------------------------------------------------------------------------------------
        
        public async Task<ActualResult<IEnumerable<ResultSearchDTO>>> SearchBirthDate(CoverageType type, DateTime startDate, DateTime endDate)
        {
            try
            {
                return await Task.Run(() =>
                {
                    switch (type)
                    {
                        case CoverageType.Employee:
                            var searchByEmployeeBirthDate = _context.Employee
                                .Include(p => p.PositionEmployees.IdSubdivisionNavigation.InverseIdSubordinateNavigation)
                                .Where(x => x.BirthDate >= startDate && x.BirthDate <= endDate);
                            return new ActualResult<IEnumerable<ResultSearchDTO>> { Result = ResultFormation(searchByEmployeeBirthDate) };
                        case CoverageType.Children:
                            var searchByChildrenBirthDate = _context.Employee
                                .Include(p => p.PositionEmployees.IdSubdivisionNavigation.InverseIdSubordinateNavigation)
                                .Include(p => p.Children)
                                .Where(x => x.Children.AsQueryable().Any(t => t.BirthDate >= startDate && t.BirthDate <= endDate));
                            return new ActualResult<IEnumerable<ResultSearchDTO>> { Result = ResultFormation(searchByChildrenBirthDate) };
                        case CoverageType.GrandChildren:
                            var searchByGrandChildrenBirthDate = _context.Employee
                                .Include(p => p.PositionEmployees.IdSubdivisionNavigation.InverseIdSubordinateNavigation)
                                .Include(p => p.GrandChildren)
                                .Where(x => x.GrandChildren.AsQueryable().Any(t => t.BirthDate >= startDate && t.BirthDate <= endDate));
                            return new ActualResult<IEnumerable<ResultSearchDTO>> { Result = ResultFormation(searchByGrandChildrenBirthDate) };
                        default:
                            return new ActualResult<IEnumerable<ResultSearchDTO>>();
                    }
                });
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<ResultSearchDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        //------------------------------------------------------------------------------------------------------------------------------------------
        
        public async Task<ActualResult<IEnumerable<ResultSearchDTO>>> SearchHobby(CoverageType type, string hobby)
        {
            try
            {
                return await Task.Run(() =>
                {
                    var idHobby = _hashIdUtilities.DecryptLong(hobby);
                    switch (type)
                    {
                        case CoverageType.Employee:
                            var searchByEmployeeHobby = _context.Employee
                                .Include(p => p.PositionEmployees.IdSubdivisionNavigation.InverseIdSubordinateNavigation)
                                .Include(p => p.HobbyEmployees)
                                .Where(x => x.HobbyEmployees.AsQueryable().Any(t => t.IdHobby == idHobby));
                            return new ActualResult<IEnumerable<ResultSearchDTO>> { Result = ResultFormation(searchByEmployeeHobby) };
                        case CoverageType.Children:
                            var searchByChildrenHobby = _context.Children
                                .Include(p => p.HobbyChildrens)
                                .Include(p => p.IdEmployeeNavigation.PositionEmployees.IdSubdivisionNavigation.InverseIdSubordinateNavigation)
                                .Where(x => x.HobbyChildrens.AsQueryable().Any(t => t.IdHobby == idHobby))
                                .Select(x => x.IdEmployeeNavigation);
                            return new ActualResult<IEnumerable<ResultSearchDTO>> { Result = ResultFormation(searchByChildrenHobby) };
                        case CoverageType.GrandChildren:
                            var searchByGrandChildrenHobby = _context.GrandChildren
                                .Include(p => p.HobbyGrandChildrens)
                                .Include(p => p.IdEmployeeNavigation.PositionEmployees.IdSubdivisionNavigation.InverseIdSubordinateNavigation)
                                .Where(x => x.HobbyGrandChildrens.AsQueryable().Any(t => t.IdHobby == idHobby))
                                .Select(x => x.IdEmployeeNavigation);
                            return new ActualResult<IEnumerable<ResultSearchDTO>> { Result = ResultFormation(searchByGrandChildrenHobby) };
                        default:
                            return new ActualResult<IEnumerable<ResultSearchDTO>>();
                    }
                });
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<ResultSearchDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<ActualResult<string>> SearchEmployee(EmployeeType type, string value)
        {
            try
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
            catch (Exception exception)
            {
                return new ActualResult<string>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        private IEnumerable<ResultSearchDTO> ResultFormation(IQueryable<DAL.Entities.Employee> employees)
        {
            return employees.OrderBy(x => x.FirstName).Select(employee => new ResultSearchDTO
            {
                HashIdUser = _hashIdUtilities.EncryptLong(employee.Id),
                FullName = $"{employee.FirstName} {employee.SecondName} {employee.Patronymic}",
                SurnameAndInitials = $"{employee.FirstName} {employee.SecondName[0]}. {(!string.IsNullOrEmpty(employee.Patronymic) ? $"{employee.Patronymic[0]}." : string.Empty)}",
                BirthDate = employee.BirthDate,
                MobilePhone = employee.MobilePhone,
                CityPhone = employee.CityPhone,

                MainSubdivision = employee.PositionEmployees.IdSubdivisionNavigation.IdSubordinate == null ?
                    employee.PositionEmployees.IdSubdivisionNavigation.Name :
                    employee.PositionEmployees.IdSubdivisionNavigation.IdSubordinateNavigation.Name,

                MainSubdivisionAbbreviation = employee.PositionEmployees.IdSubdivisionNavigation.IdSubordinate == null ?
                    employee.PositionEmployees.IdSubdivisionNavigation.Abbreviation :
                    employee.PositionEmployees.IdSubdivisionNavigation.IdSubordinateNavigation.Abbreviation,

                SubordinateSubdivision = employee.PositionEmployees.IdSubdivisionNavigation.IdSubordinate != null ?
                    employee.PositionEmployees.IdSubdivisionNavigation.Name : null,

                SubordinateSubdivisionAbbreviation = employee.PositionEmployees.IdSubdivisionNavigation.IdSubordinate != null ?
                    employee.PositionEmployees.IdSubdivisionNavigation.Abbreviation : null,
            });
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        public void Dispose()
        {
            _context.Dispose();
            _searchRepository.Dispose();
        }
    }
}