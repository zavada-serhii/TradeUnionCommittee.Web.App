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
    public class CulturalFamilyService : ICulturalFamilyService
    {
        private readonly IUnitOfWork _database;
        private readonly IAutoMapperConfiguration _mapperService;
        private readonly IHashIdConfiguration _hashIdUtilities;

        public CulturalFamilyService(IUnitOfWork database, IAutoMapperConfiguration mapperService, IHashIdConfiguration hashIdUtilities)
        {
            _database = database;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public Task<ActualResult<IEnumerable<CulturalFamilyDTO>>> GetAllAsync(string hashIdEmployee)
        {
            throw new NotImplementedException();
        }

        public Task<ActualResult<CulturalFamilyDTO>> GetAsync(string hashId)
        {
            throw new NotImplementedException();
        }

        public Task<ActualResult> CreateAsync(CulturalFamilyDTO item)
        {
            throw new NotImplementedException();
        }

        public Task<ActualResult> UpdateAsync(CulturalFamilyDTO item)
        {
            throw new NotImplementedException();
        }

        public Task<ActualResult> DeleteAsync(string hashId)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}