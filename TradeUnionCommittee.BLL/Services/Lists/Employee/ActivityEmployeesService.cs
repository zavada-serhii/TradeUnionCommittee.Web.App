using System.Collections.Generic;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.DTO.Employee;
using TradeUnionCommittee.BLL.Interfaces.Lists.Employee;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.BLL.Services.Lists.Employee
{
    public class ActivityEmployeesService : IActivityEmployeesService
    {
        private readonly IUnitOfWork _database;
        private readonly IAutoMapperConfiguration _mapperService;
        private readonly IHashIdConfiguration _hashIdUtilities;

        public ActivityEmployeesService(IUnitOfWork database, IAutoMapperConfiguration mapperService, IHashIdConfiguration hashIdUtilities)
        {
            _database = database;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<IEnumerable<ActivityEmployeesDTO>>> GetAllAsync(string hashIdEmployee)
        {
            var id = _hashIdUtilities.DecryptLong(hashIdEmployee, Enums.Services.Employee);
            var result = await _database.ActivityEmployeesRepository.GetWithIncludeToList(x => x.IdEmployee == id, c => c.IdActivitiesNavigation);
            return _mapperService.Mapper.Map<ActualResult<IEnumerable<ActivityEmployeesDTO>>>(result);
        }

        public async Task<ActualResult<ActivityEmployeesDTO>> GetAsync(string hashId)
        {
            var id = _hashIdUtilities.DecryptLong(hashId, Enums.Services.ActivityEmployees);
            var result = await _database.ActivityEmployeesRepository.GetWithInclude(x => x.Id == id, c => c.IdActivitiesNavigation);
            return _mapperService.Mapper.Map<ActualResult<ActivityEmployeesDTO>>(result);
        }

        public async Task<ActualResult> CreateAsync(ActivityEmployeesDTO item)
        {
            await _database.ActivityEmployeesRepository.Create(_mapperService.Mapper.Map<ActivityEmployees>(item));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> UpdateAsync(ActivityEmployeesDTO item)
        {
            await _database.ActivityEmployeesRepository.Update(_mapperService.Mapper.Map<ActivityEmployees>(item));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> DeleteAsync(string hashId)
        {
            await _database.ActivityEmployeesRepository.Delete(_hashIdUtilities.DecryptLong(hashId, Enums.Services.ActivityEmployees));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public void Dispose()
        {
            _database.Dispose();
        }

        //--------------- Business Logic ---------------
    }
}