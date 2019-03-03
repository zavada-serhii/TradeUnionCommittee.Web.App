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
    public class ActivityGrandChildrenService : IActivityGrandChildrenService
    {
        private readonly IUnitOfWork _database;
        private readonly IAutoMapperConfiguration _mapperService;
        private readonly IHashIdConfiguration _hashIdUtilities;

        public ActivityGrandChildrenService(IUnitOfWork database, IAutoMapperConfiguration mapperService, IHashIdConfiguration hashIdUtilities)
        {
            _database = database;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<IEnumerable<ActivityGrandChildrenDTO>>> GetAllAsync(string hashIdGrandChildren)
        {
            var id = _hashIdUtilities.DecryptLong(hashIdGrandChildren, Enums.Services.GrandChildren);
            var result = await _database.ActivityGrandChildrensRepository.GetWithIncludeToList(x => x.IdGrandChildren == id, c => c.IdActivitiesNavigation);
            return _mapperService.Mapper.Map<ActualResult<IEnumerable<ActivityGrandChildrenDTO>>>(result);
        }

        public async Task<ActualResult<ActivityGrandChildrenDTO>> GetAsync(string hashId)
        {
            var id = _hashIdUtilities.DecryptLong(hashId, Enums.Services.ActivityGrandChildren);
            var result = await _database.ActivityGrandChildrensRepository.GetWithInclude(x => x.Id == id, c => c.IdActivitiesNavigation);
            return _mapperService.Mapper.Map<ActualResult<ActivityGrandChildrenDTO>>(result);
        }

        public async Task<ActualResult> CreateAsync(ActivityGrandChildrenDTO item)
        {
            await _database.ActivityGrandChildrensRepository.Create(_mapperService.Mapper.Map<ActivityGrandChildrens>(item));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> UpdateAsync(ActivityGrandChildrenDTO item)
        {
            await _database.ActivityGrandChildrensRepository.Update(_mapperService.Mapper.Map<ActivityGrandChildrens>(item));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> DeleteAsync(string hashId)
        {
            await _database.ActivityGrandChildrensRepository.Delete(_hashIdUtilities.DecryptLong(hashId, Enums.Services.ActivityGrandChildren));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}