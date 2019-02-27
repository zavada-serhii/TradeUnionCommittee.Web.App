using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Interfaces.Lists;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.BLL.Services.Lists
{
    public class TravelFamilyService : ITravelFamilyService
    {
        private readonly IUnitOfWork _database;
        private readonly IAutoMapperConfiguration _mapperService;
        private readonly IHashIdConfiguration _hashIdUtilities;

        public TravelFamilyService(IUnitOfWork database, IAutoMapperConfiguration mapperService, IHashIdConfiguration hashIdUtilities)
        {
            _database = database;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public Task<ActualResult<IEnumerable<TravelFamilyDTO>>> GetAllAsync(string hashIdFamily)
        {
            throw new NotImplementedException();
        }

        public Task<ActualResult<TravelFamilyDTO>> GetAsync(string hashId)
        {
            throw new NotImplementedException();
        }

        public Task<ActualResult> CreateAsync(TravelFamilyDTO item)
        {
            throw new NotImplementedException();
        }

        public Task<ActualResult> UpdateAsync(TravelFamilyDTO item)
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
    }
}