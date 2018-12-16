using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Interfaces.Lists;
using TradeUnionCommittee.BLL.Utilities;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.Common.Enums;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.BLL.Services.Lists
{
    public class UniversityHouseEmployeesService : IUniversityHouseEmployeesService
    {
        private readonly IUnitOfWork _database;
        private readonly IAutoMapperUtilities _mapperService;
        private readonly IHashIdUtilities _hashIdUtilities;

        public UniversityHouseEmployeesService(IUnitOfWork database, IAutoMapperUtilities mapperService, IHashIdUtilities hashIdUtilities)
        {
            _database = database;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<IEnumerable<UniversityHouseEmployeesDTO>>> GetAllAsync(string hashIdEmployee)
        {
            var idEmployee = _hashIdUtilities.DecryptLong(hashIdEmployee, Enums.Services.Employee);
            var result = await _database.PrivateHouseEmployeesRepository.Find(x => x.IdEmployee == idEmployee && x.DateReceiving != null);
            return _mapperService.Mapper.Map<ActualResult<IEnumerable<UniversityHouseEmployeesDTO>>>(result);
        }

        public async Task<ActualResult<UniversityHouseEmployeesDTO>> GetAsync(string hashId)
        {
            var id = _hashIdUtilities.DecryptLong(hashId, Enums.Services.PrivateHouseEmployees);
            return _mapperService.Mapper.Map<ActualResult<UniversityHouseEmployeesDTO>>(await _database.PrivateHouseEmployeesRepository.GetById(id));
        }

        public async Task<ActualResult> CreateAsync(UniversityHouseEmployeesDTO item)
        {
            var check = await CheckDate(item);
            if (check.IsValid)
            {
                await _database.PrivateHouseEmployeesRepository.Create(_mapperService.Mapper.Map<PrivateHouseEmployees>(item));
                return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
            }
            return check;
        }

        public async Task<ActualResult> UpdateAsync(UniversityHouseEmployeesDTO item)
        {
            var check = await CheckDate(item);
            if (check.IsValid)
            {
                await _database.PrivateHouseEmployeesRepository.Update(_mapperService.Mapper.Map<PrivateHouseEmployees>(item));
                return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
            }
            return check;
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

        private async Task<ActualResult> CheckDate(UniversityHouseEmployeesDTO dto)
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