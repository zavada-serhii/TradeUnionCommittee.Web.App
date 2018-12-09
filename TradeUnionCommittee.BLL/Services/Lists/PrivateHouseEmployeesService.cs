using System.Collections.Generic;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Interfaces.Lists;
using TradeUnionCommittee.BLL.Utilities;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.BLL.Services.Lists
{
    public class PrivateHouseEmployeesService : IPrivateHouseEmployeesService
    {
        private readonly IUnitOfWork _database;
        private readonly IAutoMapperUtilities _mapperService;
        private readonly IHashIdUtilities _hashIdUtilities;

        public PrivateHouseEmployeesService(IUnitOfWork database, IAutoMapperUtilities mapperService, IHashIdUtilities hashIdUtilities)
        {
            _database = database;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<IEnumerable<PrivateHouseEmployeesDTO>>> GetAllAsync(string hashIdEmployee)
        {
            var idEmployee = _hashIdUtilities.DecryptLong(hashIdEmployee, Enums.Services.Employee);
            var result = await _database.PrivateHouseEmployeesRepository.Find(x => x.IdEmployee == idEmployee && x.DateReceiving == null);
            return _mapperService.Mapper.Map<ActualResult<IEnumerable<PrivateHouseEmployeesDTO>>>(result);
        }

        public async Task<ActualResult<PrivateHouseEmployeesDTO>> GetAsync(string hashId)
        {
            var id = _hashIdUtilities.DecryptLong(hashId, Enums.Services.PrivateHouseEmployees);
            return _mapperService.Mapper.Map<ActualResult<PrivateHouseEmployeesDTO>>(await _database.PrivateHouseEmployeesRepository.GetById(id));
        }

        public Task<ActualResult> CreateAsync(PrivateHouseEmployeesDTO item)
        {
            throw new System.NotImplementedException();
        }

        public Task<ActualResult> UpdateAsync(PrivateHouseEmployeesDTO item)
        {
            throw new System.NotImplementedException();
        }

        public Task<ActualResult> DeleteAsync(string hashId)
        {
            throw new System.NotImplementedException();
        }
        public void Dispose()
        {
            _database.Dispose();
        }
    }
}