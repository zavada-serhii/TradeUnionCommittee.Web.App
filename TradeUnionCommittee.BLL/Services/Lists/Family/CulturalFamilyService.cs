using System.Collections.Generic;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.DTO.Family;
using TradeUnionCommittee.BLL.Interfaces.Lists.Family;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.BLL.Services.Lists.Family
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

        public async Task<ActualResult<IEnumerable<CulturalFamilyDTO>>> GetAllAsync(string hashIdFamily)
        {
            var id = _hashIdUtilities.DecryptLong(hashIdFamily, Enums.Services.Family);
            var result = await _database.CulturalFamilyRepository.GetWithIncludeToList(x => x.IdFamily == id, c => c.IdCulturalNavigation);
            return _mapperService.Mapper.Map<ActualResult<IEnumerable<CulturalFamilyDTO>>>(result);
        }

        public async Task<ActualResult<CulturalFamilyDTO>> GetAsync(string hashId)
        {
            var id = _hashIdUtilities.DecryptLong(hashId, Enums.Services.CulturalFamily);
            var result = await _database.CulturalFamilyRepository.GetWithInclude(x => x.Id == id, c => c.IdCulturalNavigation);
            return _mapperService.Mapper.Map<ActualResult<CulturalFamilyDTO>>(result);
        }

        public async Task<ActualResult> CreateAsync(CulturalFamilyDTO item)
        {
            await _database.CulturalFamilyRepository.Create(_mapperService.Mapper.Map<CulturalFamily>(item));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> UpdateAsync(CulturalFamilyDTO item)
        {
            await _database.CulturalFamilyRepository.Update(_mapperService.Mapper.Map<CulturalFamily>(item));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> DeleteAsync(string hashId)
        {
            await _database.CulturalFamilyRepository.Delete(_hashIdUtilities.DecryptLong(hashId, Enums.Services.CulturalFamily));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public void Dispose()
        {
            _database.Dispose();
        }

        //--------------- Business Logic ---------------
    }
}