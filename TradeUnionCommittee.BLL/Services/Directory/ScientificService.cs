using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Interfaces.Directory;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.BLL.Services.Directory
{
    public class ScientificService : IScientificService
    {
        private readonly IUnitOfWork _database;

        public ScientificService(IUnitOfWork database)
        {
            _database = database;
        }

        public async Task<ActualResult<IEnumerable<string>>> GetAllScientificDegreeAsync()
        {
            return await Task.Run(() =>
            {
                return new ActualResult<IEnumerable<string>>
                {
                    Result = _database.ScientificRepository.GetAll().Result.Select(x => x.ScientificDegree).ToList()
                };
            });
        }

        public async Task<ActualResult<IEnumerable<string>>> GetAllScientificTitleAsync()
        {
            return await Task.Run(() =>
            {
                return new ActualResult<IEnumerable<string>>
                {
                    Result = _database.ScientificRepository.GetAll().Result.Select(x => x.ScientificTitle).ToList()
                };
            });
        }
    }
}