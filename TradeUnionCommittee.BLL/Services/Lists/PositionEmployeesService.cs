using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.DTO.Employee;
using TradeUnionCommittee.BLL.Interfaces.Lists;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.Common.Enums;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.BLL.Services.Lists
{
    public class PositionEmployeesService : IPositionEmployeesService
    {
        private readonly IUnitOfWork _database;
        private readonly IAutoMapperConfiguration _mapperService;
        private readonly IHashIdConfiguration _hashIdUtilities;

        public PositionEmployeesService(IUnitOfWork database, IAutoMapperConfiguration mapperService, IHashIdConfiguration hashIdUtilities)
        {
            _database = database;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<PositionEmployeesDTO>> GetAsync(string hashIdEmployee)
        {
            var id = _hashIdUtilities.DecryptLong(hashIdEmployee, Enums.Services.Employee);
            return _mapperService.Mapper.Map<ActualResult<PositionEmployeesDTO>>(await _database.PositionEmployeesRepository.GetByProperty(x => x.IdEmployee == id));
        }

        public async Task<ActualResult> UpdateAsync(PositionEmployeesDTO dto)
        {
            var check = await CheckDate(dto);
            if (check.IsValid)
            {
                await _database.PositionEmployeesRepository.Update(_mapperService.Mapper.Map<PositionEmployees>(dto));
                return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
            }
            return check;
        }

        public void Dispose()
        {
            _database.Dispose();
        }

        //--------------- Business Logic ---------------

        private async Task<ActualResult> CheckDate(PositionEmployeesDTO dto)
        {
            var employee = await _database.EmployeeRepository.GetById(_hashIdUtilities.DecryptLong(dto.HashIdEmployee, Enums.Services.Employee));
            if (employee.IsValid)
            {
                var listErrors = new List<string>();
                if (dto.StartDate.Year < employee.Result.StartYearWork)
                {
                    listErrors.Add($"Рік дати початку менший року початку роботи в ОНУ - {employee.Result.StartYearWork}!");
                }
                return listErrors.Any() ? new ActualResult(listErrors) : new ActualResult();
            }
            return new ActualResult(Errors.TupleDeleted);
        }
    }
}