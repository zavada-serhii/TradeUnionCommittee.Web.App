using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Interfaces.Search;
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

        public async Task<IEnumerable<ResultSearchDTO>> ListAddedEmployeesTemp()
        {
            return await Task.Run(() =>
            {
                var employees = _database.EmployeeRepository.GetAll().Result;
                var positionEmployees = _database.PositionEmployeesRepository.GetAll().Result;
                var subdivision = _database.SubdivisionsRepository.GetAll().Result;
                var subordinateSubdivision =_database.SubdivisionsRepository.Find(x => x.IdSubordinate != null).Result;

                var listLong = new List<long?>();
                foreach (var subdivisionse in subdivision)
                {
                    listLong.Add(subdivisionse.Id);
                    listLong.Add(subdivisionse.IdSubordinate);
                }
                
                /*
                --Initial SQL Query
                
                SELECT COALESCE(ss.DeptName,s.DeptName) AS MainSubdivision, 
                CASE WHEN ss.DeptName IS NULL THEN NULL ELSE s.DeptName END AS SubordinateSubdivision 
                
                FROM maindb.StructuralSubdivision AS s 
                JOIN maindb.StructuralSubdivision AS ss ON s.DeptKod IN (ss.PKod, ss.DeptKod) 
                JOIN maindb.ListPositionSubdivision AS ls ON ls.NameSubdivision = ss.DeptKod 
                JOIN maindb.EmployeesUniversity AS e ON e.ID = ls.IDEmployees
                
                */
                
                //var result = (from e in employees
                //              join pe in positionEmployees on e.Id equals pe.IdEmployee
                //              join s in subdivision on pe.IdSubdivision equals s.Id

                //              join ss in subordinateSubdivision on s.Id equals ss.IdSubordinate

                //              select new ResultSearchDTO
                //              {
                //                  IdUser = e.Id,
                //                  FullName = e.FirstName + " " + e.SecondName + " " + e.Patronymic,
                //                  BirthDate = e.BirthDate,
                //                  MobilePhone = e.MobilePhone,
                //                  CityPhone = e.CityPhone,

                //                  MainSubdivision = s.DeptName,
                //                  SubordinateSubdivision = ss.DeptName
                //              }).ToList();


                var sub = from s in subdivision
                    where listLong.Contains(s.Id)
                    select s;

                foreach (var subdivisionse in sub)
                {
                    var x = subdivisionse.Id;
                }




                //var result2 = (from pe in positionEmployees
                //               join s in sub on pe.IdSubdivision equals s.InverseIdSubordinateNavigation
                //               select new ResultSearchDTO
                //               {
                //                   MainSubdivision = s.DeptName
                //               }).ToList();




                return new List<ResultSearchDTO>();
                // return result;
                //return result1;
            });
        }
    }
}
