using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Interfaces.Directory;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.BLL.Services.Directory
{
    public class QualificationService : IQualificationService
    {
        private readonly IUnitOfWork _database;

        public QualificationService(IUnitOfWork database)
        {
            _database = database;
        }

        public async Task<ActualResult<IEnumerable<string>>> GetAllScientificDegreeAsync()
        {
            var result = await GetAllScientific();
            return new ActualResult<IEnumerable<string>> {Result = result.Result.Select(x => x.ScientificDegree).Distinct().ToList()};
        }

        public async Task<ActualResult<IEnumerable<string>>> GetAllScientificTitleAsync()
        {
            var result = await GetAllScientific();
            return new ActualResult<IEnumerable<string>> { Result = result.Result.Select(x => x.ScientificTitle).Distinct().ToList() };
        }

        private async Task<ActualResult<IEnumerable<DAL.Entities.Employee>>> GetAllScientific()
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