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

        public async Task<ActualResult<IEnumerable<WellnessChildrenDTO>>> GetAllAsync(string hashIdChildren)
        {
            var id = _hashIdUtilities.DecryptLong(hashIdChildren, Enums.Services.Children);
            var result = await _database.EventChildrensRepository
                .GetWithIncludeToList(x => x.IdChildren == id &&
                                           x.IdEventNavigation.Type == TypeEvent.Wellness,
                                      c => c.IdEventNavigation);
            return _mapperService.Mapper.Map<ActualResult<IEnumerable<WellnessChildrenDTO>>>(result);
        }

        public async Task<ActualResult<WellnessChildrenDTO>> GetAsync(string hashId)
        {
            var id = _hashIdUtilities.DecryptLong(hashId, Enums.Services.WellnessChildren);
            var result = await _database.EventChildrensRepository.GetWithInclude(x => x.Id == id, c => c.IdEventNavigation);
            return _mapperService.Mapper.Map<ActualResult<WellnessChildrenDTO>>(result);
        }

        public async Task<ActualResult> CreateAsync(WellnessChildrenDTO item)
        {
            await _database.EventChildrensRepository.Create(_mapperService.Mapper.Map<EventChildrens>(item));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> UpdateAsync(WellnessChildrenDTO item)
        {
            await _database.EventChildrensRepository.Update(_mapperService.Mapper.Map<EventChildrens>(item));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> DeleteAsync(string hashId)
        {
            await _database.EventChildrensRepository.Delete(_hashIdUtilities.DecryptLong(hashId, Enums.Services.WellnessChildren));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}