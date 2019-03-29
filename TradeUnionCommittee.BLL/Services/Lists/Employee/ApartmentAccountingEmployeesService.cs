using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.DTO.Employee;
using TradeUnionCommittee.BLL.Interfaces.Lists.Employee;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.Common.Enums;
using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.BLL.Services.Lists.Employee
{
    public class ApartmentAccountingEmployeesService : IApartmentAccountingEmployeesService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly IAutoMapperConfiguration _mapperService;
        private readonly IHashIdConfiguration _hashIdUtilities;

        public ApartmentAccountingEmployeesService(TradeUnionCommitteeContext context, IAutoMapperConfiguration mapperService, IHashIdConfiguration hashIdUtilities)
        {
            _context = context;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<IEnumerable<ApartmentAccountingEmployeesDTO>>> GetAllAsync(string hashIdEmployee)
        {
            try
            {
                var id = _hashIdUtilities.DecryptLong(hashIdEmployee);
                var apartmentAccounting = await _context.ApartmentAccountingEmployees
                    .Where(x => x.IdEmployee == id)
                    .OrderByDescending(x => x.DateAdoption)
                    .ToListAsync();
                var result = _mapperService.Mapper.Map<IEnumerable<ApartmentAccountingEmployeesDTO>>(apartmentAccounting);
                return new ActualResult<IEnumerable<ApartmentAccountingEmployeesDTO>> { Result = result };
            }
            catch (Exception)
            {
                return new ActualResult<IEnumerable<ApartmentAccountingEmployeesDTO>>(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult<ApartmentAccountingEmployeesDTO>> GetAsync(string hashId)
        {
            try
            {
                var id = _hashIdUtilities.DecryptLong(hashId);
                var apartmentAccounting = await _context.ApartmentAccountingEmployees.FindAsync(id);
                var result = _mapperService.Mapper.Map<ApartmentAccountingEmployeesDTO>(apartmentAccounting);
                return new ActualResult<ApartmentAccountingEmployeesDTO> { Result = result };
            }
            catch (Exception)
            {
                return new ActualResult<ApartmentAccountingEmployeesDTO>(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult> CreateAsync(ApartmentAccountingEmployeesDTO item)
        {
            try
            {
                var mapping = _mapperService.Mapper.Map<ApartmentAccountingEmployees>(item);
                await _context.ApartmentAccountingEmployees.AddAsync(mapping);
                await _context.SaveChangesAsync();
                return new ActualResult();
            }
            catch (Exception)
            {
                return new ActualResult(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult> UpdateAsync(ApartmentAccountingEmployeesDTO item)
        {
            try
            {
                var mapping = _mapperService.Mapper.Map<ApartmentAccountingEmployees>(item);
                _context.Entry(mapping).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return new ActualResult();
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
                var id = _hashIdUtilities.DecryptLong(hashId);
                var result = await _context.ApartmentAccountingEmployees.FindAsync(id);
                if (result != null)
                {
                    _context.ApartmentAccountingEmployees.Remove(result);
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
    }
}