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
    public class TourGrandChildrenService : ITourGrandChildrenService
    {
        private readonly IUnitOfWork _database;
        private readonly IAutoMapperConfiguration _mapperService;
        private readonly IHashIdConfiguration _hashIdUtilities;

        public TourGrandChildrenService(IUnitOfWork database, IAutoMapperConfiguration mapperService, IHashIdConfiguration hashIdUtilities)
        {
            _database = database;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<IEnumerable<TourGrandChildrenDTO>>> GetAllAsync(string hashIdGrandChildren)
        {
            var id = _hashIdUtilities.DecryptLong(hashIdGrandChildren, Enums.Services.GrandChildren);
            var result = await _database.EventGrandChildrensRepository
                .GetWithIncludeToList(x => x.IdGrandChildren == id &&
                                           x.IdEventNavigation.Type == TypeEvent.Tour,
                                      c => c.IdEventNavigation);
            return _mapperService.Mapper.Map<ActualResult<IEnumerable<TourGrandChildrenDTO>>>(result);
        }

        public async Task<ActualResult<TourGrandChildrenDTO>> GetAsync(string hashId)
        {
            var id = _hashIdUtilities.DecryptLong(hashId, Enums.Services.TourGrandChildren);
            var result = await _database.EventGrandChildrensRepository.GetWithInclude(x => x.Id == id, c => c.IdEventNavigation);
            return _mapperService.Mapper.Map<ActualResult<TourGrandChildrenDTO>>(result);
        }

        public async Task<ActualResult> CreateAsync(TourGrandChildrenDTO item)
        {
            await _database.EventGrandChildrensRepository.Create(_mapperService.Mapper.Map<EventGrandChildrens>(item));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> UpdateAsync(TourGrandChildrenDTO item)
        {
            await _database.EventGrandChildrensRepository.Update(_mapperService.Mapper.Map<EventGrandChildrens>(item));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> DeleteAsync(string hashId)
        {
            await _database.EventChildrensRepository.Delete(_hashIdUtilities.DecryptLong(hashId, Enums.Services.TourGrandChildren));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}