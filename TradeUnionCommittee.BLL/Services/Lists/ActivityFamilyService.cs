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
    public class ActivityFamilyService : IActivityFamilyService
    {
        private readonly IUnitOfWork _database;
        private readonly IAutoMapperConfiguration _mapperService;
        private readonly IHashIdConfiguration _hashIdUtilities;

        public ActivityFamilyService(IUnitOfWork database, IAutoMapperConfiguration mapperService, IHashIdConfiguration hashIdUtilities)
        {
            _database = database;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<IEnumerable<ActivityFamilyDTO>>> GetAllAsync(string hashIdFamily)
        {
            var id = _hashIdUtilities.DecryptLong(hashIdFamily, Enums.Services.Family);
            var result = await _database.ActivityFamilyRepository.GetWithIncludeToList(x => x.IdFamily == id, c => c.IdActivitiesNavigation);
            return _mapperService.Mapper.Map<ActualResult<IEnumerable<ActivityFamilyDTO>>>(result);
        }

        public async Task<ActualResult<ActivityFamilyDTO>> GetAsync(string hashId)
        {
            var id = _hashIdUtilities.DecryptLong(hashId, Enums.Services.ActivityFamily);
            var result = await _database.ActivityFamilyRepository.GetWithInclude(x => x.Id == id, c => c.IdActivitiesNavigation);
            return _mapperService.Mapper.Map<ActualResult<ActivityFamilyDTO>>(result);
        }

        public async Task<ActualResult> CreateAsync(ActivityFamilyDTO item)
        {
            await _database.ActivityFamilyRepository.Create(_mapperService.Mapper.Map<ActivityFamily>(item));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> UpdateAsync(ActivityFamilyDTO item)
        {
            await _database.ActivityFamilyRepository.Update(_mapperService.Mapper.Map<ActivityFamily>(item));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> DeleteAsync(string hashId)
        {
            await _database.ActivityFamilyRepository.Delete(_hashIdUtilities.DecryptLong(hashId, Enums.Services.ActivityFamily));
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