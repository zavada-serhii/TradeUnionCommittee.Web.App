using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Interfaces.Search;
using TradeUnionCommittee.Common.ActualResults;
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

        public async Task<ActualResult<IEnumerable<ResultSearchDTO>>> ListAddedEmployeesTemp()
        {
            var subdivisions = await _database.SubdivisionsRepository.GetAll();
            var positionEmployees = await _database.PositionEmployeesRepository.GetAll();
            var employee = await _database.EmployeeRepository.GetAll();

            var result = (from s in subdivisions.Result
                from ss in subdivisions.Result
                where s.Id == ss.Id || s.Id == ss.IdSubordinate
                join pe in positionEmployees.Result
                on ss.Id equals pe.IdSubdivision
                join e in employee.Result
                on pe.IdEmployee equals e.Id
                where s.IdSubordinate == null
                select new ResultSearchDTO
                {
                    IdUser = e.Id,
                    FullName = e.FirstName + " " + e.SecondName + " " + e.Patronymic,
                    SurnameAndInitials = e.FirstName + " " + e.SecondName[0] + ". " + e.Patronymic[0] + ".",
                    BirthDate = e.BirthDate,
                    MobilePhone = e.MobilePhone,
                    CityPhone = e.CityPhone,
                    MainSubdivision = s.Name,
                    MainSubdivisionAbbreviation = s.Abbreviation,
                    SubordinateSubdivision = s.Id == ss.Id ? null : ss.Name,
                    SubordinateSubdivisionAbbreviation = s.Id == ss.Id ? null : ss.Abbreviation
                }).OrderBy(x =>x.IdUser).ToList();

            return new ActualResult<IEnumerable<ResultSearchDTO>> { Result = result };
        }

        public async Task<ActualResult<IEnumerable<ResultSearchDTO>>> SearchFullName(string fullName)
        {
            var ids = await _database.SearchRepository.SearchByFullName(fullName);
            var enumerable = ids as IList<long> ?? ids.ToList();
            if (!enumerable.Any()) return new ActualResult<IEnumerable<ResultSearchDTO>>();

            var listEmployee = new List<DAL.Entities.Employee>();

            foreach (var id in enumerable)
            {
                var employees = await _database.EmployeeRepository.GetWithInclude(x => x.Id == id, p => p.PositionEmployees.IdSubdivisionNavigation.InverseIdSubordinateNavigation);
                listEmployee.Add(employees.Result.FirstOrDefault());
            }

            var result = new List<ResultSearchDTO>();

            foreach (var e in listEmployee)
            {
                string mainSubdivision;
                string mainSubdivisionAbbreviation;

                string subordinateSubdivision = null;
                string subordinateSubdivisionAbbreviation = null;

                if (e.PositionEmployees.IdSubdivisionNavigation.IdSubordinateNavigation != null)
                {
                    mainSubdivision = e.PositionEmployees.IdSubdivisionNavigation.IdSubordinateNavigation.Name;
                    mainSubdivisionAbbreviation = e.PositionEmployees.IdSubdivisionNavigation.IdSubordinateNavigation.Abbreviation;
                    subordinateSubdivision = e.PositionEmployees.IdSubdivisionNavigation.Name;
                    subordinateSubdivisionAbbreviation = e.PositionEmployees.IdSubdivisionNavigation.Abbreviation;
                }
                else
                {
                    mainSubdivision = e.PositionEmployees.IdSubdivisionNavigation.Name;
                    mainSubdivisionAbbreviation = e.PositionEmployees.IdSubdivisionNavigation.Abbreviation;
                }

                result.Add(new ResultSearchDTO
                {
                    IdUser = e.Id,
                    FullName = e.FirstName + " " + e.SecondName + " " + e.Patronymic,
                    SurnameAndInitials = e.FirstName + " " + e.SecondName[0] + ". " + e.Patronymic[0] + ".",
                    BirthDate = e.BirthDate,
                    MobilePhone = e.MobilePhone,
                    CityPhone = e.CityPhone,
                    MainSubdivision = mainSubdivision,
                    MainSubdivisionAbbreviation = mainSubdivisionAbbreviation,
                    SubordinateSubdivision = subordinateSubdivision,
                    SubordinateSubdivisionAbbreviation = subordinateSubdivisionAbbreviation
                });
            }
            return new ActualResult<IEnumerable<ResultSearchDTO>> { Result = result };
        }
    }
}