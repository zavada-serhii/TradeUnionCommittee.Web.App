using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.DTO.Employee;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.BLL.Interfaces.Lists.Employee;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.BLL.Services.Lists.Employee
{
    public class PublicHouseEmployeesService : IPublicHouseEmployeesService
    {
        private readonly IUnitOfWork _database;
        private readonly IAutoMapperConfiguration _mapperService;
        private readonly IHashIdConfiguration _hashIdUtilities;

        public PublicHouseEmployeesService(IUnitOfWork database, IAutoMapperConfiguration mapperService, IHashIdConfiguration hashIdUtilities)
        {
            _database = database;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<IEnumerable<PublicHouseEmployeesDTO>>> GetAllAsync(string hashIdEmployee, PublicHouse type)
        {
            var idEmployee = _hashIdUtilities.DecryptLong(hashIdEmployee, Enums.Services.Employee);
            var result = await _database.PublicHouseEmployeesRepository
                                        .GetWithIncludeToList(x => x.IdEmployee == idEmployee && x.IdAddressPublicHouseNavigation.Type == Converter(type),
                                                        c => c.IdAddressPublicHouseNavigation);
            return _mapperService.Mapper.Map<ActualResult<IEnumerable<PublicHouseEmployeesDTO>>>(result);
        }

        public async Task<ActualResult<PublicHouseEmployeesDTO>> GetAsync(string hashId)
        {
            var id = _hashIdUtilities.DecryptLong(hashId, Enums.Services.PublicHouseEmployees);
            var result = await _database.PublicHouseEmployeesRepository.GetWithInclude(x => x.Id == id, c => c.IdAddressPublicHouseNavigation);
            return _mapperService.Mapper.Map<ActualResult<PublicHouseEmployeesDTO>>(result);
        }

        public async Task<ActualResult> CreateAsync(PublicHouseEmployeesDTO item)
        {
            await _database.PublicHouseEmployeesRepository.Create(_mapperService.Mapper.Map<PublicHouseEmployees>(item));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> UpdateAsync(PublicHouseEmployeesDTO item)
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

        private TypeHouse Converter(PublicHouse type)
        {
            switch (type)
            {
                case PublicHouse.Dormitory:
                    return TypeHouse.Dormitory;
                case PublicHouse.Departmental:
                    return TypeHouse.Departmental;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}