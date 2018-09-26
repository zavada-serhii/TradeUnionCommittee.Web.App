using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Interfaces.Search;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.DAL.Enums;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.BLL.Services.Search
{
    public class SearchService : ISearchService
    {
        private readonly IUnitOfWork _database;

        public SearchService(IUnitOfWork database)
        {
            _database = database;
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<ActualResult<IEnumerable<ResultSearchDTO>>> SearchFullName(string fullName)
        {
            var ids = await _database.SearchRepository.SearchByFullName(fullName, AlgorithmSearchFullName.Gist);
            if (!ids.Any()) return new ActualResult<IEnumerable<ResultSearchDTO>>();

            var listEmployee = new List<DAL.Entities.Employee>();

            foreach (var id in ids)
            {
                var employees = await _database.EmployeeRepository.GetWithInclude(x => x.Id == id, p => p.PositionEmployees.IdSubdivisionNavigation.InverseIdSubordinateNavigation);
                listEmployee.Add(employees.Result.FirstOrDefault());
            }

            return new ActualResult<IEnumerable<ResultSearchDTO>> { Result = ResultFormation(listEmployee) };
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
                    mainSubdivision = employee.PositionEmployees.IdSubdivisionNavigation.Name;
                    mainSubdivisionAbbreviation = employee.PositionEmployees.IdSubdivisionNavigation.Abbreviation;
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