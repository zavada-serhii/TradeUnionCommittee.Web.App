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
            return await Task.Run(() =>
            {
                var subdivisions = _database.SubdivisionsRepository.GetAll().Result;
                var positionEmployees = _database.PositionEmployeesRepository.GetAll().Result;
                var employee = _database.EmployeeRepository.GetAll().Result;

                var result = (from s in subdivisions
                        from ss in subdivisions
                        where s.Id == ss.Id || s.Id == ss.IdSubordinate
                        join pe in positionEmployees
                        on ss.Id equals pe.IdSubdivision
                        join e in employee
                        on pe.IdEmployee equals e.Id
                        where s.IdSubordinate == null
                        select new ResultSearchDTO
                        {
                            IdUser = e.Id,
                            FullName = e.FirstName + " " + e.SecondName + " " + e.Patronymic,
                            SurnameAndInitials = e.FirstName + " " + e.SecondName[0] + ". " + e.Patronymic[0] + ". ",
                            BirthDate = e.BirthDate,
                            MobilePhone = e.MobilePhone,
                            CityPhone = e.CityPhone,
                            MainSubdivision = s.Name,
                            MainSubdivisionAbbreviation = s.Abbreviation,
                            SubordinateSubdivision = s.Id == ss.Id ? null : ss.Name,
                            SubordinateSubdivisionAbbreviation = s.Id == ss.Id ? null : ss.Abbreviation
                        }).ToList();

                return new ActualResult<IEnumerable<ResultSearchDTO>> {Result = result};
            });
        }
    }
}