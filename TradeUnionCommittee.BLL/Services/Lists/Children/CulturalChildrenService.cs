using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.DTO.Children;
using TradeUnionCommittee.BLL.Interfaces.Lists.Children;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.BLL.Services.Lists.Children
{
    public class CulturalChildrenService : ICulturalChildrenService
    {
        private readonly IUnitOfWork _database;
        private readonly IAutoMapperConfiguration _mapperService;
        private readonly IHashIdConfiguration _hashIdUtilities;

        public CulturalChildrenService(IUnitOfWork database, IAutoMapperConfiguration mapperService, IHashIdConfiguration hashIdUtilities)
        {
            _database = database;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public Task<ActualResult<IEnumerable<CulturalChildrenDTO>>> GetAllAsync(string hashIdEmployee)
        {
            throw new NotImplementedException();
        }

        public Task<ActualResult<CulturalChildrenDTO>> GetAsync(string hashId)
        {
            throw new NotImplementedException();
        }

        public Task<ActualResult> CreateAsync(CulturalChildrenDTO item)
        {
            throw new NotImplementedException();
        }

        public Task<ActualResult> UpdateAsync(CulturalChildrenDTO item)
        {
            throw new NotImplementedException();
        }

        public Task<ActualResult> DeleteAsync(string hashId)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _database.Dispose();
        }

        public Task<string> GetHashIdEmployee(string hashIdHeir)
        {
            throw new NotImplementedException();
        }
    }
}