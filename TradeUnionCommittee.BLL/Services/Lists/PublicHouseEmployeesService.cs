using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.BLL.Interfaces.Lists;
using TradeUnionCommittee.BLL.Utilities;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.BLL.Services.Lists
{
    public class PublicHouseEmployeesService : IPublicHouseEmployeesService
    {
        private readonly IUnitOfWork _database;
        private readonly IAutoMapperUtilities _mapperService;
        private readonly IHashIdUtilities _hashIdUtilities;

        public PublicHouseEmployeesService(IUnitOfWork database, IAutoMapperUtilities mapperService, IHashIdUtilities hashIdUtilities)
        {
            _database = database;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<IEnumerable<PublicHouseEmployeesDTO>>> GetAllAsync(string hashIdEmployee, PublicHouse type)
        {
            var idEmployee = _hashIdUtilities.DecryptLong(hashIdEmployee, Enums.Services.Employee);
            var result = await _database.PublicHouseEmployeesRepository
                                        .GetWithInclude(x => x.IdEmployee == idEmployee && x.IdAddressPublicHouseNavigation.Type == Converter(type),
                                                        c => c.IdAddressPublicHouseNavigation);
            return _mapperService.Mapper.Map<ActualResult<IEnumerable<PublicHouseEmployeesDTO>>>(result);
        }

        public async Task<ActualResult<PublicHouseEmployeesDTO>> GetAsync(string hashId)
        {
            var id = _hashIdUtilities.DecryptLong(hashId, Enums.Services.PublicHouseEmployees);
            var result = await _database.PublicHouseEmployeesRepository
                .GetWithInclude(x => x.Id == id,
                                c => c.IdAddressPublicHouseNavigation);
            return result.IsValid
                ? _mapperService.Mapper.Map<ActualResult<PublicHouseEmployeesDTO>>(new ActualResult<PublicHouseEmployees> { Result = result.Result.FirstOrDefault() })
                : new ActualResult<PublicHouseEmployeesDTO>(result.ErrorsList);
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