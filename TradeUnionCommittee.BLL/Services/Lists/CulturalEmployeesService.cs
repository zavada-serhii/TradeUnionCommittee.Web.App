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
    public class CulturalEmployeesService : ICulturalEmployeesService
    {
        private readonly IUnitOfWork _database;
        private readonly IAutoMapperConfiguration _mapperService;
        private readonly IHashIdConfiguration _hashIdUtilities;

        public CulturalEmployeesService(IUnitOfWork database, IAutoMapperConfiguration mapperService, IHashIdConfiguration hashIdUtilities)
        {
            _database = database;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<IEnumerable<CulturalEmployeesDTO>>> GetAllAsync(string hashIdEmployee)
        {
            var id = _hashIdUtilities.DecryptLong(hashIdEmployee, Enums.Services.Employee);
            var result = await _database.CulturalEmployeesRepository.GetWithIncludeToList(x => x.IdEmployee == id, c => c.IdCulturalNavigation);
            return _mapperService.Mapper.Map<ActualResult<IEnumerable<CulturalEmployeesDTO>>>(result);
        }

        public async Task<ActualResult<CulturalEmployeesDTO>> GetAsync(string hashId)
        {
            var id = _hashIdUtilities.DecryptLong(hashId, Enums.Services.CulturalEmployees);
            var result = await _database.CulturalEmployeesRepository.GetWithInclude(x => x.Id == id, c => c.IdCulturalNavigation);
            return _mapperService.Mapper.Map<ActualResult<CulturalEmployeesDTO>>(result);
        }

        public async Task<ActualResult> CreateAsync(CulturalEmployeesDTO item)
        {
            await _database.CulturalEmployeesRepository.Create(_mapperService.Mapper.Map<CulturalEmployees>(item));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> UpdateAsync(CulturalEmployeesDTO item)
        {
            await _database.CulturalEmployeesRepository.Update(_mapperService.Mapper.Map<CulturalEmployees>(item));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> DeleteAsync(string hashId)
        {
            await _database.CulturalEmployeesRepository.Delete(_hashIdUtilities.DecryptLong(hashId, Enums.Services.CulturalEmployees));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public void Dispose()
        {
            _database.Dispose();
        }

        //--------------- Business Logic ---------------
    }
}