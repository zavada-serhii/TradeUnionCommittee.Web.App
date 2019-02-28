using System.Collections.Generic;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Interfaces.Lists;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.DAL.Entities;
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

        public async Task<ActualResult<IEnumerable<TravelFamilyDTO>>> GetAllAsync(string hashIdFamily)
        {
            var id = _hashIdUtilities.DecryptLong(hashIdFamily, Enums.Services.Family);
            var result = await _database.EventFamilyRepository
                .GetWithIncludeToList(x => x.IdFamily == id &&
                                           x.IdEventNavigation.Type == TypeEvent.Travel,
                                      c => c.IdEventNavigation);
            return _mapperService.Mapper.Map<ActualResult<IEnumerable<TravelFamilyDTO>>>(result);
        }

        public async Task<ActualResult<TravelFamilyDTO>> GetAsync(string hashId)
        {
            var id = _hashIdUtilities.DecryptLong(hashId, Enums.Services.TravelFamily);
            var result = await _database.EventFamilyRepository.GetWithInclude(x => x.Id == id, c => c.IdEventNavigation);
            return _mapperService.Mapper.Map<ActualResult<TravelFamilyDTO>>(result);
        }

        public async Task<ActualResult> CreateAsync(TravelFamilyDTO item)
        {
            await _database.EventFamilyRepository.Create(_mapperService.Mapper.Map<EventFamily>(item));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> UpdateAsync(TravelFamilyDTO item)
        {
            await _database.EventFamilyRepository.Update(_mapperService.Mapper.Map<EventFamily>(item));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> DeleteAsync(string hashId)
        {
            await _database.EventFamilyRepository.Delete(_hashIdUtilities.DecryptLong(hashId, Enums.Services.TravelFamily));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<string> GetHashIdEmployee(string hashIdFamily)
        {
            var id = _hashIdUtilities.DecryptLong(hashIdFamily, Enums.Services.Family);
            var result = await _database.FamilyRepository.GetById(id);
            if (result.IsValid && result.Result != null)
            {
                return _hashIdUtilities.EncryptLong(result.Result.IdEmployee, Enums.Services.Employee);
            }
            return null;
        }

        public void Dispose()
        {
            _database.Dispose();
        }

        //--------------- Business Logic ---------------
    }
}