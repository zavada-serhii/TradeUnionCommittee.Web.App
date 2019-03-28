using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.DTO.Employee;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.BLL.Interfaces.Lists.Employee;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.Common.Enums;
using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.BLL.Services.Lists.Employee
{
    public class PrivateHouseEmployeesService : IPrivateHouseEmployeesService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly IAutoMapperConfiguration _mapperService;
        private readonly IHashIdConfiguration _hashIdUtilities;

        public PrivateHouseEmployeesService(TradeUnionCommitteeContext context, IAutoMapperConfiguration mapperService, IHashIdConfiguration hashIdUtilities)
        {
            _context = context;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<IEnumerable<PrivateHouseEmployeesDTO>>> GetAllAsync(string hashIdEmployee, PrivateHouse type)
        {
            try
            {
                IEnumerable<PrivateHouseEmployees> result;
                var idEmployee = _hashIdUtilities.DecryptLong(hashIdEmployee, Enums.Services.Employee);
                switch (type)
                {
                    case PrivateHouse.PrivateHouse:
                        result = await _context.PrivateHouseEmployees.Where(x => x.IdEmployee == idEmployee && x.DateReceiving == null).ToListAsync();
                        break;
                    case PrivateHouse.UniversityHouse:
                        result = await _context.PrivateHouseEmployees.Where(x => x.IdEmployee == idEmployee && x.DateReceiving != null).ToListAsync();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type), type, null);
                }
                var mapping =_mapperService.Mapper.Map<IEnumerable<PrivateHouseEmployeesDTO>>(result);
                return new ActualResult<IEnumerable<PrivateHouseEmployeesDTO>> { Result = mapping };
            }
            catch (Exception)
            {
                return new ActualResult<IEnumerable<PrivateHouseEmployeesDTO>>(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult<PrivateHouseEmployeesDTO>> GetAsync(string hashId)
        {
            try
            {
                var id = _hashIdUtilities.DecryptLong(hashId, Enums.Services.PrivateHouseEmployees);
                var privateHouse = await _context.PrivateHouseEmployees.FindAsync(id);
                var result = _mapperService.Mapper.Map<PrivateHouseEmployeesDTO>(privateHouse);
                return new ActualResult<PrivateHouseEmployeesDTO> { Result = result };
            }
            catch (Exception)
            {
                return new ActualResult<PrivateHouseEmployeesDTO>(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult> CreateAsync(PrivateHouseEmployeesDTO item, PrivateHouse type)
        {
            try
            {
                switch (type)
                {
                    case PrivateHouse.PrivateHouse:
                        await _context.PrivateHouseEmployees.AddAsync(_mapperService.Mapper.Map<PrivateHouseEmployees>(item));
                        await _context.SaveChangesAsync();
                        return new ActualResult();
                    case PrivateHouse.UniversityHouse:
                        var check = await CheckDate(item);
                        if (check.IsValid)
                        {
                            await _context.PrivateHouseEmployees.AddAsync(_mapperService.Mapper.Map<PrivateHouseEmployees>(item));
                            await _context.SaveChangesAsync();
                            return new ActualResult();
                        }
                        return check;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type), type, null);
                }
            }
            catch (Exception)
            {
                return new ActualResult(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult> UpdateAsync(PrivateHouseEmployeesDTO item, PrivateHouse type)
        {
            try
            {
                switch (type)
                {
                    case PrivateHouse.PrivateHouse:
                        _context.Entry(_mapperService.Mapper.Map<PrivateHouseEmployees>(item)).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                        return new ActualResult();
                    case PrivateHouse.UniversityHouse:
                        var check = await CheckDate(item);
                        if (check.IsValid)
                        {
                            _context.Entry(_mapperService.Mapper.Map<PrivateHouseEmployees>(item)).State = EntityState.Modified;
                            await _context.SaveChangesAsync();
                            return new ActualResult();
                        }
                        return check;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type), type, null);
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                return new ActualResult(Errors.TupleDeletedOrUpdated);
            }
            catch (Exception)
            {
                return new ActualResult(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult> DeleteAsync(string hashId)
        {
            try
            {
                var id = _hashIdUtilities.DecryptLong(hashId, Enums.Services.PrivateHouseEmployees);
                var result = await _context.PrivateHouseEmployees.FindAsync(id);
                if (result != null)
                {
                    _context.PrivateHouseEmployees.Remove(result);
                    await _context.SaveChangesAsync();
                }
                return new ActualResult();
            }
            catch (Exception)
            {
                return new ActualResult(Errors.DataBaseError);
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        //--------------- Business Logic ---------------

        private async Task<ActualResult> CheckDate(PrivateHouseEmployeesDTO dto)
        {
            try
            {
                var id = _hashIdUtilities.DecryptLong(dto.HashIdEmployee, Enums.Services.Employee);
                var employee = await _context.Employee.FindAsync(id);
                if (employee != null)
                {
                    var listErrors = new List<string>();
                    if (employee.StartDateTradeUnion > dto.DateReceiving)
                    {
                        listErrors.Add("Дата вступу в профспілку більша за дату розподілу!");
                    }
                    if (employee.EndDateTradeUnion != null)
                    {
                        if (employee.EndDateTradeUnion < dto.DateReceiving)
                        {
                            listErrors.Add("Дата виходу з профспілки не може бути меншою, ніж дата розподілу!");
                        }
                    }
                    return listErrors.Any() ? new ActualResult(listErrors) : new ActualResult();
                }
                return new ActualResult(Errors.TupleDeleted);
            }
            catch (Exception)
            {
               return new ActualResult(Errors.DataBaseError);
            }
        }
    }
}