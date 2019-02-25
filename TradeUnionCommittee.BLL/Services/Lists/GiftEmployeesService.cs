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
    public class GiftEmployeesService : IGiftEmployeesService
    {
        private readonly IUnitOfWork _database;
        private readonly IAutoMapperConfiguration _mapperService;
        private readonly IHashIdConfiguration _hashIdUtilities;

        public GiftEmployeesService(IUnitOfWork database, IAutoMapperConfiguration mapperService, IHashIdConfiguration hashIdUtilities)
        {
            _database = database;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<IEnumerable<GiftEmployeesDTO>>> GetAllAsync(string hashIdEmployee)
        {
            var id = _hashIdUtilities.DecryptLong(hashIdEmployee, Enums.Services.Employee);
            var result = await _database.GiftEmployeesRepository.Find(x => x.IdEmployee == id);
            return _mapperService.Mapper.Map<ActualResult<IEnumerable<GiftEmployeesDTO>>>(result);
        }

        public async Task<ActualResult<GiftEmployeesDTO>> GetAsync(string hashId)
        {
            var id = _hashIdUtilities.DecryptLong(hashId, Enums.Services.GiftEmployees);
            var result = await _database.GiftEmployeesRepository.GetById(id);
            return _mapperService.Mapper.Map<ActualResult<GiftEmployeesDTO>>(result);
        }

        public async Task<ActualResult> CreateAsync(GiftEmployeesDTO item)
        {
            await _database.GiftEmployeesRepository.Create(_mapperService.Mapper.Map<GiftEmployees>(item));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> UpdateAsync(GiftEmployeesDTO item)
        {
            await _database.GiftEmployeesRepository.Update(_mapperService.Mapper.Map<GiftEmployees>(item));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> DeleteAsync(string hashId)
        {
            await _database.GiftEmployeesRepository.Delete(_hashIdUtilities.DecryptLong(hashId, Enums.Services.GiftEmployees));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public void Dispose()
        {
            _database.Dispose();
        }

        //--------------- Business Logic ---------------
    }
}