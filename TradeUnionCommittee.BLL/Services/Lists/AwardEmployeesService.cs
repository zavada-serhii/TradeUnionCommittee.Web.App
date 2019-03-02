using System.Collections.Generic;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.DTO.Employee;
using TradeUnionCommittee.BLL.Interfaces.Lists;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.BLL.Services.Lists
{
    public class AwardEmployeesService : IAwardEmployeesService
    {
        private readonly IUnitOfWork _database;
        private readonly IAutoMapperConfiguration _mapperService;
        private readonly IHashIdConfiguration _hashIdUtilities;

        public AwardEmployeesService(IUnitOfWork database, IAutoMapperConfiguration mapperService, IHashIdConfiguration hashIdUtilities)
        {
            _database = database;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<IEnumerable<AwardEmployeesDTO>>> GetAllAsync(string hashIdEmployee)
        {
            var id = _hashIdUtilities.DecryptLong(hashIdEmployee, Enums.Services.Employee);
            var result = await _database.AwardEmployeesRepository.GetWithIncludeToList(x => x.IdEmployee == id, c => c.IdAwardNavigation);
            return _mapperService.Mapper.Map<ActualResult<IEnumerable<AwardEmployeesDTO>>>(result);
        }

        public async Task<ActualResult<AwardEmployeesDTO>> GetAsync(string hashId)
        {
            var id = _hashIdUtilities.DecryptLong(hashId, Enums.Services.AwardEmployees);
            var result = await _database.AwardEmployeesRepository.GetWithInclude(x => x.Id == id, c => c.IdAwardNavigation);
            return _mapperService.Mapper.Map<ActualResult<AwardEmployeesDTO>>(result);
        }

        public async Task<ActualResult> CreateAsync(AwardEmployeesDTO item)
        {
            await _database.AwardEmployeesRepository.Create(_mapperService.Mapper.Map<AwardEmployees>(item));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> UpdateAsync(AwardEmployeesDTO item)
        {
            await _database.AwardEmployeesRepository.Update(_mapperService.Mapper.Map<AwardEmployees>(item));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> DeleteAsync(string hashId)
        {
            await _database.AwardEmployeesRepository.Delete(_hashIdUtilities.DecryptLong(hashId, Enums.Services.AwardEmployees));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public void Dispose()
        {
            _database.Dispose();
        }

        //--------------- Business Logic ---------------
    }
}