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
    public class TravelChildrenService : ITravelChildrenService
    {
        private readonly IUnitOfWork _database;
        private readonly IAutoMapperConfiguration _mapperService;
        private readonly IHashIdConfiguration _hashIdUtilities;

        public TravelChildrenService(IUnitOfWork database, IAutoMapperConfiguration mapperService, IHashIdConfiguration hashIdUtilities)
        {
            _database = database;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<IEnumerable<TravelChildrenDTO>>> GetAllAsync(string hashIdChildren)
        {
            var id = _hashIdUtilities.DecryptLong(hashIdChildren, Enums.Services.Children);
            var result = await _database.EventChildrensRepository
                .GetWithIncludeToList(x => x.IdChildren == id &&
                                           x.IdEventNavigation.Type == TypeEvent.Travel,
                                      c => c.IdEventNavigation);
            return _mapperService.Mapper.Map<ActualResult<IEnumerable<TravelChildrenDTO>>>(result);
        }

        public async Task<ActualResult<TravelChildrenDTO>> GetAsync(string hashId)
        {
            var id = _hashIdUtilities.DecryptLong(hashId, Enums.Services.TravelChildren);
            var result = await _database.EventChildrensRepository.GetWithInclude(x => x.Id == id, c => c.IdEventNavigation);
            return _mapperService.Mapper.Map<ActualResult<TravelChildrenDTO>>(result);
        }

        public async Task<ActualResult> CreateAsync(TravelChildrenDTO item)
        {
            await _database.EventChildrensRepository.Create(_mapperService.Mapper.Map<EventChildrens>(item));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> UpdateAsync(TravelChildrenDTO item)
        {
            await _database.EventChildrensRepository.Update(_mapperService.Mapper.Map<EventChildrens>(item));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> DeleteAsync(string hashId)
        {
            await _database.EventChildrensRepository.Delete(_hashIdUtilities.DecryptLong(hashId, Enums.Services.TravelChildren));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public void Dispose()
        {
            _database.Dispose();
        }

        public async Task<string> GetHashIdEmployee(string hashIdHeir)
        {
            throw new System.NotImplementedException();
        }
    }
}