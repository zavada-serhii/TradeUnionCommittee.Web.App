using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Interfaces.Search;
using TradeUnionCommittee.BLL.Utilities;
using TradeUnionCommittee.Common.ActualResults;
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
                                    x.PositionEmployees.IdSubdivisionNavigation.Id == idSubdivision ||
                                    x.PositionEmployees.IdSubdivisionNavigation.IdSubordinate == idSubdivision, 
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
                                         x.PositionEmployees.IdSubdivisionNavigation.Id == idSubdivision ||
                                         x.PositionEmployees.IdSubdivisionNavigation.IdSubordinate == idSubdivision,
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
                                         x.PositionEmployees.IdSubdivisionNavigation.Id == idSubdivision ||
                                         x.PositionEmployees.IdSubdivisionNavigation.IdSubordinate == idSubdivision,
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
                    IdUser = employee.Id,
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