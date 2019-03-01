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
    public class ActivityChildrenService : IActivityChildrenService
    {
        private readonly IUnitOfWork _database;
        private readonly IAutoMapperConfiguration _mapperService;
        private readonly IHashIdConfiguration _hashIdUtilities;

        public ActivityChildrenService(IUnitOfWork database, IAutoMapperConfiguration mapperService, IHashIdConfiguration hashIdUtilities)
        {
            _database = database;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<IEnumerable<ActivityChildrenDTO>>> GetAllAsync(string hashIdChildren)
        {
            var id = _hashIdUtilities.DecryptLong(hashIdChildren, Enums.Services.Children);
            var result = await _database.ActivityChildrensRepository.GetWithIncludeToList(x => x.IdChildren == id, c => c.IdActivitiesNavigation);
            return _mapperService.Mapper.Map<ActualResult<IEnumerable<ActivityChildrenDTO>>>(result);
        }

        public async Task<ActualResult<ActivityChildrenDTO>> GetAsync(string hashId)
        {
            var id = _hashIdUtilities.DecryptLong(hashId, Enums.Services.ActivityChildren);
            var result = await _database.ActivityChildrensRepository.GetWithInclude(x => x.Id == id, c => c.IdActivitiesNavigation);
            return _mapperService.Mapper.Map<ActualResult<ActivityChildrenDTO>>(result);
        }

        public async Task<ActualResult> CreateAsync(ActivityChildrenDTO item)
        {
            await _database.ActivityChildrensRepository.Create(_mapperService.Mapper.Map<ActivityChildrens>(item));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> UpdateAsync(ActivityChildrenDTO item)
        {
            await _database.ActivityChildrensRepository.Update(_mapperService.Mapper.Map<ActivityChildrens>(item));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> DeleteAsync(string hashId)
        {
            await _database.ActivityChildrensRepository.Delete(_hashIdUtilities.DecryptLong(hashId, Enums.Services.ActivityChildren));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}