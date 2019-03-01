using System.Collections.Generic;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.DTO.Children;
using TradeUnionCommittee.BLL.Interfaces.Lists.Children;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.DAL.Entities;
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

        public async Task<ActualResult<IEnumerable<CulturalChildrenDTO>>> GetAllAsync(string hashIdChildren)
        {
            var id = _hashIdUtilities.DecryptLong(hashIdChildren, Enums.Services.Children);
            var result = await _database.CulturalChildrensRepository.GetWithIncludeToList(x => x.IdChildren == id, c => c.IdCulturalNavigation);
            return _mapperService.Mapper.Map<ActualResult<IEnumerable<CulturalChildrenDTO>>>(result);
        }

        public async Task<ActualResult<CulturalChildrenDTO>> GetAsync(string hashId)
        {
            var id = _hashIdUtilities.DecryptLong(hashId, Enums.Services.CulturalChildren);
            var result = await _database.CulturalChildrensRepository.GetWithInclude(x => x.Id == id, c => c.IdCulturalNavigation);
            return _mapperService.Mapper.Map<ActualResult<CulturalChildrenDTO>>(result);
        }

        public async Task<ActualResult> CreateAsync(CulturalChildrenDTO item)
        {
            await _database.CulturalChildrensRepository.Create(_mapperService.Mapper.Map<CulturalChildrens>(item));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> UpdateAsync(CulturalChildrenDTO item)
        {
            await _database.CulturalChildrensRepository.Update(_mapperService.Mapper.Map<CulturalChildrens>(item));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> DeleteAsync(string hashId)
        {
            await _database.CulturalChildrensRepository.Delete(_hashIdUtilities.DecryptLong(hashId, Enums.Services.CulturalChildren));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}