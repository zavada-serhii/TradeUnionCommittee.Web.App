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
    public class GiftChildrenService : IGiftChildrenService
    {
        private readonly IUnitOfWork _database;
        private readonly IAutoMapperConfiguration _mapperService;
        private readonly IHashIdConfiguration _hashIdUtilities;

        public GiftChildrenService(IUnitOfWork database, IAutoMapperConfiguration mapperService, IHashIdConfiguration hashIdUtilities)
        {
            _database = database;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public Task<ActualResult<IEnumerable<GiftChildrenDTO>>> GetAllAsync(string hashIdEmployee)
        {
            throw new NotImplementedException();
        }

        public Task<ActualResult<GiftChildrenDTO>> GetAsync(string hashId)
        {
            throw new NotImplementedException();
        }

        public Task<ActualResult> CreateAsync(GiftChildrenDTO item)
        {
            throw new NotImplementedException();
        }

        public Task<ActualResult> UpdateAsync(GiftChildrenDTO item)
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