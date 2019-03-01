using System.Collections.Generic;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.DTO.Children;
using TradeUnionCommittee.BLL.Interfaces.Lists.Children;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.BLL.Services.Lists.Children
{
    public class WellnessChildrenService : IWellnessChildrenService
    {
        private readonly IUnitOfWork _database;
        private readonly IAutoMapperConfiguration _mapperService;
        private readonly IHashIdConfiguration _hashIdUtilities;

        public WellnessChildrenService(IUnitOfWork database, IAutoMapperConfiguration mapperService, IHashIdConfiguration hashIdUtilities)
        {
            _database = database;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public Task<ActualResult<IEnumerable<WellnessChildrenDTO>>> GetAllAsync(string hashIdChildren)
        {
            throw new System.NotImplementedException();
        }

        public Task<ActualResult<WellnessChildrenDTO>> GetAsync(string hashId)
        {
            throw new System.NotImplementedException();
        }

        public Task<ActualResult> CreateAsync(WellnessChildrenDTO item)
        {
            throw new System.NotImplementedException();
        }

        public Task<ActualResult> UpdateAsync(WellnessChildrenDTO item)
        {
            throw new System.NotImplementedException();
        }

        public Task<ActualResult> DeleteAsync(string hashId)
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            _database.Dispose();
        }

        public Task<string> GetHashIdEmployee(string hashIdHeir)
        {
            throw new System.NotImplementedException();
        }
    }
}