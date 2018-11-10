using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Interfaces.Directory;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.BLL.Services.Directory
{
    public class EducationService : IEducationService
    {
        private readonly IUnitOfWork _database;

        public EducationService(IUnitOfWork database)
        {
            _database = database;
        }

        public async Task<ActualResult<IEnumerable<string>>> GetAllLevelEducationAsync()
        {
            var result = await GetEducation();
            return new ActualResult<IEnumerable<string>> { Result = result.Result.Select(x => x.LevelEducation).Distinct() };
        }

        public async Task<ActualResult<IEnumerable<string>>> GetAllNameInstitutionAsync()
        {
            var result = await GetEducation();
            return new ActualResult<IEnumerable<string>> { Result = result.Result.Select(x => x.NameInstitution).Distinct() };
        }

        private async Task<ActualResult<IEnumerable<DAL.Entities.Employee>>> GetEducation()
        {
            return await _database.EmployeeRepository.GetAll();
        }

        //------------------------------------------------------------------------------------------------------------------------------------------
        
        public void Dispose()
        {
            _database.Dispose();
        }
    }
}