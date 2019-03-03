using System.Collections.Generic;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.DTO.GrandChildren;
using TradeUnionCommittee.BLL.Interfaces.Lists.GrandChildren;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.BLL.Services.Lists.GrandChildren
{
    public class CulturalGrandChildrenService : ICulturalGrandChildrenService
    {
        private readonly IUnitOfWork _database;
        private readonly IAutoMapperConfiguration _mapperService;
        private readonly IHashIdConfiguration _hashIdUtilities;

        public CulturalGrandChildrenService(IUnitOfWork database, IAutoMapperConfiguration mapperService, IHashIdConfiguration hashIdUtilities)
        {
            _database = database;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<IEnumerable<CulturalGrandChildrenDTO>>> GetAllAsync(string hashIdGrandChildren)
        {
            var id = _hashIdUtilities.DecryptLong(hashIdGrandChildren, Enums.Services.GrandChildren);
            var result = await _database.CulturalGrandChildrensRepository.GetWithIncludeToList(x => x.IdGrandChildren == id, c => c.IdCulturalNavigation);
            return _mapperService.Mapper.Map<ActualResult<IEnumerable<CulturalGrandChildrenDTO>>>(result);
        }

        public async Task<ActualResult<CulturalGrandChildrenDTO>> GetAsync(string hashId)
        {
            var id = _hashIdUtilities.DecryptLong(hashId, Enums.Services.CulturalGrandChildren);
            var result = await _database.CulturalGrandChildrensRepository.GetWithInclude(x => x.Id == id, c => c.IdCulturalNavigation);
            return _mapperService.Mapper.Map<ActualResult<CulturalGrandChildrenDTO>>(result);
        }

        public async Task<ActualResult> CreateAsync(CulturalGrandChildrenDTO item)
        {
            await _database.CulturalGrandChildrensRepository.Create(_mapperService.Mapper.Map<CulturalGrandChildrens>(item));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> UpdateAsync(CulturalGrandChildrenDTO item)
        {
            await _database.CulturalGrandChildrensRepository.Update(_mapperService.Mapper.Map<CulturalGrandChildrens>(item));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> DeleteAsync(string hashId)
        {
            await _database.CulturalGrandChildrensRepository.Delete(_hashIdUtilities.DecryptLong(hashId, Enums.Services.CulturalGrandChildren));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}