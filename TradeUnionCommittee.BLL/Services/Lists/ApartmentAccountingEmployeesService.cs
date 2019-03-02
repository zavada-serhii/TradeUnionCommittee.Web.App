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
    public class ApartmentAccountingEmployeesService : IApartmentAccountingEmployeesService
    {
        private readonly IUnitOfWork _database;
        private readonly IAutoMapperConfiguration _mapperService;
        private readonly IHashIdConfiguration _hashIdUtilities;

        public ApartmentAccountingEmployeesService(IUnitOfWork database, IAutoMapperConfiguration mapperService, IHashIdConfiguration hashIdUtilities)
        {
            _database = database;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<IEnumerable<ApartmentAccountingEmployeesDTO>>> GetAllAsync(string hashIdEmployee)
        {
            var id = _hashIdUtilities.DecryptLong(hashIdEmployee, Enums.Services.Employee);
            var result = await _database.ApartmentAccountingEmployeesRepository.Find(x => x.IdEmployee == id);
            return _mapperService.Mapper.Map<ActualResult<IEnumerable<ApartmentAccountingEmployeesDTO>>>(result);
        }

        public async Task<ActualResult<ApartmentAccountingEmployeesDTO>> GetAsync(string hashId)
        {
            var id = _hashIdUtilities.DecryptLong(hashId, Enums.Services.ApartmentAccountingEmployees);
            var result = await _database.ApartmentAccountingEmployeesRepository.GetByProperty(x => x.Id == id);
            return _mapperService.Mapper.Map<ActualResult<ApartmentAccountingEmployeesDTO>>(result);
        }

        public async Task<ActualResult> CreateAsync(ApartmentAccountingEmployeesDTO item)
        {
            await _database.ApartmentAccountingEmployeesRepository.Create(_mapperService.Mapper.Map<ApartmentAccountingEmployees>(item));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> UpdateAsync(ApartmentAccountingEmployeesDTO item)
        {
            await _database.ApartmentAccountingEmployeesRepository.Update(_mapperService.Mapper.Map<ApartmentAccountingEmployees>(item));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> DeleteAsync(string hashId)
        {
            var id = _hashIdUtilities.DecryptLong(hashId, Enums.Services.ApartmentAccountingEmployees);
            await _database.ApartmentAccountingEmployeesRepository.Delete(id);
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public void Dispose()
        {
            _database.Dispose();
        }

        //--------------- Business Logic ---------------
    }
}