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
    public class FluorographyEmployeesService : IFluorographyEmployeesService
    {
        private readonly IUnitOfWork _database;
        private readonly IAutoMapperConfiguration _mapperService;
        private readonly IHashIdConfiguration _hashIdUtilities;

        public FluorographyEmployeesService(IUnitOfWork database, IAutoMapperConfiguration mapperService, IHashIdConfiguration hashIdUtilities)
        {
            _database = database;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<IEnumerable<FluorographyEmployeesDTO>>> GetAllAsync(string hashIdEmployee)
        {
            var id = _hashIdUtilities.DecryptLong(hashIdEmployee, Enums.Services.Employee);
            var result = await _database.FluorographyEmployeesRepository.Find(x => x.IdEmployee == id);
            return _mapperService.Mapper.Map<ActualResult<IEnumerable<FluorographyEmployeesDTO>>>(result);
        }

        public async Task<ActualResult<FluorographyEmployeesDTO>> GetAsync(string hashId)
        {
            var id = _hashIdUtilities.DecryptLong(hashId, Enums.Services.FluorographyEmployees);
            var result = await _database.FluorographyEmployeesRepository.GetByProperty(x => x.Id == id);
            return _mapperService.Mapper.Map<ActualResult<FluorographyEmployeesDTO>>(result);
        }

        public async Task<ActualResult> CreateAsync(FluorographyEmployeesDTO item)
        {
            await _database.FluorographyEmployeesRepository.Create(_mapperService.Mapper.Map<FluorographyEmployees>(item));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> UpdateAsync(FluorographyEmployeesDTO item)
        {
            await _database.FluorographyEmployeesRepository.Update(_mapperService.Mapper.Map<FluorographyEmployees>(item));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> DeleteAsync(string hashId)
        {
            var id = _hashIdUtilities.DecryptLong(hashId, Enums.Services.FluorographyEmployees);
            await _database.FluorographyEmployeesRepository.Delete(id);
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public void Dispose()
        {
            _database.Dispose();
        }

        //--------------- Business Logic ---------------
    }
}