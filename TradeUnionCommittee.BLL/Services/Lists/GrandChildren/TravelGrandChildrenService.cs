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
    public class TravelGrandChildrenService : ITravelGrandChildrenService
    {
        private readonly IUnitOfWork _database;
        private readonly IAutoMapperConfiguration _mapperService;
        private readonly IHashIdConfiguration _hashIdUtilities;

        public TravelGrandChildrenService(IUnitOfWork database, IAutoMapperConfiguration mapperService, IHashIdConfiguration hashIdUtilities)
        {
            _database = database;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<IEnumerable<TravelGrandChildrenDTO>>> GetAllAsync(string hashIdGrandChildren)
        {
            var id = _hashIdUtilities.DecryptLong(hashIdGrandChildren, Enums.Services.GrandChildren);
            var result = await _database.EventGrandChildrensRepository
                .GetWithIncludeToList(x => x.IdGrandChildren == id &&
                                           x.IdEventNavigation.Type == TypeEvent.Travel,
                                      c => c.IdEventNavigation);
            return _mapperService.Mapper.Map<ActualResult<IEnumerable<TravelGrandChildrenDTO>>>(result);
        }

        public async Task<ActualResult<TravelGrandChildrenDTO>> GetAsync(string hashId)
        {
            var id = _hashIdUtilities.DecryptLong(hashId, Enums.Services.TravelGrandChildren);
            var result = await _database.EventGrandChildrensRepository.GetWithInclude(x => x.Id == id, c => c.IdEventNavigation);
            return _mapperService.Mapper.Map<ActualResult<TravelGrandChildrenDTO>>(result);
        }

        public async Task<ActualResult> CreateAsync(TravelGrandChildrenDTO item)
        {
            await _database.EventGrandChildrensRepository.Create(_mapperService.Mapper.Map<EventGrandChildrens>(item));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> UpdateAsync(TravelGrandChildrenDTO item)
        {
            await _database.EventGrandChildrensRepository.Update(_mapperService.Mapper.Map<EventGrandChildrens>(item));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> DeleteAsync(string hashId)
        {
            await _database.EventGrandChildrensRepository.Delete(_hashIdUtilities.DecryptLong(hashId, Enums.Services.TravelGrandChildren));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}