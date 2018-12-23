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
    public class DepartmentalEmployeesService : IDepartmentalEmployeesService
    {
        private readonly IUnitOfWork _database;
        private readonly IAutoMapperUtilities _mapperService;
        private readonly IHashIdUtilities _hashIdUtilities;

        public DepartmentalEmployeesService(IUnitOfWork database, IAutoMapperUtilities mapperService, IHashIdUtilities hashIdUtilities)
        {
            _database = database;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<IEnumerable<DepartmentalEmployeesDTO>>> GetAllAsync(string hashIdEmployee)
        {
            var idEmployee = _hashIdUtilities.DecryptLong(hashIdEmployee, Enums.Services.Employee);
            var result = await _database.PublicHouseEmployeesRepository
                                        .GetWithInclude(x => x.IdEmployee == idEmployee && x.IdAddressPublicHouseNavigation.Type == TypeHouse.Departmental,
                                                        c => c.IdAddressPublicHouseNavigation);
            return _mapperService.Mapper.Map<ActualResult<IEnumerable<DepartmentalEmployeesDTO>>>(result);
        }

        public async Task<ActualResult<DepartmentalEmployeesDTO>> GetAsync(string hashId, string hashIdEmployee)
        {
            var id = _hashIdUtilities.DecryptLong(hashId, Enums.Services.PublicHouseEmployees);
            var idEmployee = _hashIdUtilities.DecryptLong(hashIdEmployee, Enums.Services.Employee);
            return _mapperService.Mapper.Map<ActualResult<DepartmentalEmployeesDTO>>(await _database.PublicHouseEmployeesRepository.GetByProperty(x => x.IdEmployee == idEmployee && x.IdAddressPublicHouse == id));
        }

        public async Task<ActualResult> CreateAsync(DepartmentalEmployeesDTO item)
        {
            await _database.PublicHouseEmployeesRepository.Create(_mapperService.Mapper.Map<PublicHouseEmployees>(item));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> UpdateAsync(DepartmentalEmployeesDTO item)
        {
            await _database.PublicHouseEmployeesRepository.Update(_mapperService.Mapper.Map<PublicHouseEmployees>(item));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> DeleteAsync(string hashId)
        {
            await _database.PublicHouseEmployeesRepository.Delete(_hashIdUtilities.DecryptLong(hashId, Enums.Services.PublicHouseEmployees));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}