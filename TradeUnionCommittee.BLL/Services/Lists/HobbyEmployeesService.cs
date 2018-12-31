using System.Collections.Generic;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Interfaces.Lists;
using TradeUnionCommittee.BLL.Utilities;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.BLL.Services.Lists
{
    public class HobbyEmployeesService : IHobbyEmployeesService
    {
        private readonly IUnitOfWork _database;
        private readonly IAutoMapperUtilities _mapperService;
        private readonly IHashIdUtilities _hashIdUtilities;

        public HobbyEmployeesService(IUnitOfWork database, IAutoMapperUtilities mapperService, IHashIdUtilities hashIdUtilities)
        {
            _database = database;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<IEnumerable<HobbyEmployeesDTO>>> GetAllAsync(string hashIdEmployee)
        {
            var id = _hashIdUtilities.DecryptLong(hashIdEmployee, Enums.Services.Employee);
            var result = await _database.HobbyEmployeesRepository.GetWithIncludeToList(x => x.IdEmployee == id, c => c.IdHobbyNavigation);
            return _mapperService.Mapper.Map<ActualResult<IEnumerable<HobbyEmployeesDTO>>>(result);
        }

        public async Task<ActualResult<HobbyEmployeesDTO>> GetAsync(string hashId)
        {
            var id = _hashIdUtilities.DecryptLong(hashId, Enums.Services.HobbyEmployees);
            var result = await _database.HobbyEmployeesRepository.GetWithInclude(x => x.Id == id, c => c.IdHobbyNavigation);
            return _mapperService.Mapper.Map<ActualResult<HobbyEmployeesDTO>>(result);
        }

        public async Task<ActualResult> CreateAsync(HobbyEmployeesDTO item)
        {
            await _database.HobbyEmployeesRepository.Create(_mapperService.Mapper.Map<HobbyEmployees>(item));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> UpdateAsync(HobbyEmployeesDTO item)
        {
            await _database.HobbyEmployeesRepository.Update(_mapperService.Mapper.Map<HobbyEmployees>(item));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> DeleteAsync(string hashId)
        {
            await _database.HobbyEmployeesRepository.Delete(_hashIdUtilities.DecryptLong(hashId, Enums.Services.HobbyEmployees));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}