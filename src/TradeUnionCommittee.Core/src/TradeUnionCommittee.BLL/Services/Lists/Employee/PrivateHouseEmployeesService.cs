using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.ActualResults;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.DTO.Employee;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.BLL.Helpers;
using TradeUnionCommittee.BLL.Interfaces.Lists.Employee;
using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.BLL.Services.Lists.Employee
{
    internal class PrivateHouseEmployeesService : IPrivateHouseEmployeesService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly AutoMapperConfiguration _mapperService;
        private readonly HashIdConfiguration _hashIdUtilities;

        public PrivateHouseEmployeesService(TradeUnionCommitteeContext context, AutoMapperConfiguration mapperService, HashIdConfiguration hashIdUtilities)
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
                var idEmployee = _hashIdUtilities.DecryptLong(hashIdEmployee);
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
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<PrivateHouseEmployeesDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<PrivateHouseEmployeesDTO>> GetAsync(string hashId)
        {
            try
            {
                var id = _hashIdUtilities.DecryptLong(hashId);
                var privateHouse = await _context.PrivateHouseEmployees.FindAsync(id);
                if (privateHouse == null)
                {
                    return new ActualResult<PrivateHouseEmployeesDTO>(Errors.TupleDeleted);
                }
                var result = _mapperService.Mapper.Map<PrivateHouseEmployeesDTO>(privateHouse);
                return new ActualResult<PrivateHouseEmployeesDTO> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<PrivateHouseEmployeesDTO>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> CreateAsync(PrivateHouseEmployeesDTO item, PrivateHouse type)
        {
            try
            {
                switch (type)
                {
                    case PrivateHouse.PrivateHouse:
                    {
                        var privateHouse = _mapperService.Mapper.Map<PrivateHouseEmployees>(item);
                        await _context.PrivateHouseEmployees.AddAsync(privateHouse);
                        await _context.SaveChangesAsync();
                        var hashId = _hashIdUtilities.EncryptLong(privateHouse.Id);
                        return new ActualResult<string> { Result = hashId };
                    }
                    case PrivateHouse.UniversityHouse:
                    {
                        var check = await CheckDate(item);
                        if (check.IsValid)
                        {
                            var universityHouse = _mapperService.Mapper.Map<PrivateHouseEmployees>(item);
                            await _context.PrivateHouseEmployees.AddAsync(universityHouse);
                            await _context.SaveChangesAsync();
                            var hashId = _hashIdUtilities.EncryptLong(universityHouse.Id);
                            return new ActualResult<string> { Result = hashId };
                        }
                        return check;
                    }
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type), type, null);
                }
            }
            catch (Exception exception)
            {
                return new ActualResult(DescriptionExceptionHelper.GetDescriptionError(exception));
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
            catch (Exception exception)
            {
                return new ActualResult(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> DeleteAsync(string hashId)
        {
            try
            {
                var id = _hashIdUtilities.DecryptLong(hashId);
                var result = await _context.PrivateHouseEmployees.FindAsync(id);
                if (result != null)
                {
                    _context.PrivateHouseEmployees.Remove(result);
                    await _context.SaveChangesAsync();
                }
                return new ActualResult();
            }
            catch (Exception exception)
            {
                return new ActualResult(DescriptionExceptionHelper.GetDescriptionError(exception));
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
                var id = _hashIdUtilities.DecryptLong(dto.HashIdEmployee);
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
            catch (Exception exception)
            {
                return new ActualResult(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }
    }
}