using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.DTO.Employee;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.BLL.Interfaces.Lists;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.Common.Enums;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.BLL.Services.Lists
{
    public class PrivateHouseEmployeesService : IPrivateHouseEmployeesService
    {
        private readonly IUnitOfWork _database;
        private readonly IAutoMapperConfiguration _mapperService;
        private readonly IHashIdConfiguration _hashIdUtilities;

        public PrivateHouseEmployeesService(IUnitOfWork database, IAutoMapperConfiguration mapperService, IHashIdConfiguration hashIdUtilities)
        {
            _database = database;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<IEnumerable<PrivateHouseEmployeesDTO>>> GetAllAsync(string hashIdEmployee, PrivateHouse type)
        {
            ActualResult<IEnumerable<PrivateHouseEmployees>> result;
            var idEmployee = _hashIdUtilities.DecryptLong(hashIdEmployee, Enums.Services.Employee);
            switch (type)
            {
                case PrivateHouse.PrivateHouse:
                    result = await _database.PrivateHouseEmployeesRepository.Find(x => x.IdEmployee == idEmployee && x.DateReceiving == null);
                    break;
                case PrivateHouse.UniversityHouse:
                    result = await _database.PrivateHouseEmployeesRepository.Find(x => x.IdEmployee == idEmployee && x.DateReceiving != null);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
            return _mapperService.Mapper.Map<ActualResult<IEnumerable<PrivateHouseEmployeesDTO>>>(result);
        }

        public async Task<ActualResult<PrivateHouseEmployeesDTO>> GetAsync(string hashId)
        {
            var id = _hashIdUtilities.DecryptLong(hashId, Enums.Services.PrivateHouseEmployees);
            return _mapperService.Mapper.Map<ActualResult<PrivateHouseEmployeesDTO>>(await _database.PrivateHouseEmployeesRepository.GetById(id));
        }

        public async Task<ActualResult> CreateAsync(PrivateHouseEmployeesDTO item, PrivateHouse type)
        {
            switch (type)
            {
                case PrivateHouse.PrivateHouse:
                    await _database.PrivateHouseEmployeesRepository.Create(_mapperService.Mapper.Map<PrivateHouseEmployees>(item));
                    return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
                case PrivateHouse.UniversityHouse:
                    var check = await CheckDate(item);
                    if (check.IsValid)
                    {
                        await _database.PrivateHouseEmployeesRepository.Create(_mapperService.Mapper.Map<PrivateHouseEmployees>(item));
                        return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
                    }
                    return check;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public async Task<ActualResult> UpdateAsync(PrivateHouseEmployeesDTO item, PrivateHouse type)
        {
            switch (type)
            {
                case PrivateHouse.PrivateHouse:
                    await _database.PrivateHouseEmployeesRepository.Update(_mapperService.Mapper.Map<PrivateHouseEmployees>(item));
                    return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
                case PrivateHouse.UniversityHouse:
                    var check = await CheckDate(item);
                    if (check.IsValid)
                    {
                        await _database.PrivateHouseEmployeesRepository.Update(_mapperService.Mapper.Map<PrivateHouseEmployees>(item));
                        return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
                    }
                    return check;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public async Task<ActualResult> DeleteAsync(string hashId)
        {
            await _database.PrivateHouseEmployeesRepository.Delete(_hashIdUtilities.DecryptLong(hashId, Enums.Services.PrivateHouseEmployees));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public void Dispose()
        {
            _database.Dispose();
        }

        //--------------- Business Logic ---------------

        private async Task<ActualResult> CheckDate(PrivateHouseEmployeesDTO dto)
        {
            var employee = await _database.EmployeeRepository.GetById(_hashIdUtilities.DecryptLong(dto.HashIdEmployee, Enums.Services.Employee));
            if (employee.IsValid)
            {
                var listErrors = new List<string>();
                if (employee.Result.StartDateTradeUnion > dto.DateReceiving)
                {
                    listErrors.Add("Дата вступу в профспілку більша за дату розподілу!");
                }
                if (employee.Result.EndDateTradeUnion != null)
                {
                    if (employee.Result.EndDateTradeUnion < dto.DateReceiving)
                    {
                        listErrors.Add("Дата виходу з профспілки не може бути меншою, ніж дата розподілу!");
                    }
                }
                return listErrors.Any() ? new ActualResult(listErrors) : new ActualResult();
            }
            return new ActualResult(Errors.TupleDeleted);
        }
    }
}