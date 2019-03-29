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
    public class GiftEmployeesService : IGiftEmployeesService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly IAutoMapperConfiguration _mapperService;
        private readonly IHashIdConfiguration _hashIdUtilities;

        public GiftEmployeesService(TradeUnionCommitteeContext context, IAutoMapperConfiguration mapperService, IHashIdConfiguration hashIdUtilities)
        {
            _context = context;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<IEnumerable<GiftEmployeesDTO>>> GetAllAsync(string hashIdEmployee)
        {
            try
            {
                var id = _hashIdUtilities.DecryptLong(hashIdEmployee);
                var gift = await _context.GiftEmployees
                    .Where(x => x.IdEmployee == id)
                    .OrderByDescending(x => x.DateGift)
                    .ToListAsync();
                var result = _mapperService.Mapper.Map<IEnumerable<GiftEmployeesDTO>>(gift);
                return new ActualResult<IEnumerable<GiftEmployeesDTO>> { Result = result };
            }
            catch (Exception)
            {
                return new ActualResult<IEnumerable<GiftEmployeesDTO>>(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult<GiftEmployeesDTO>> GetAsync(string hashId)
        {
            try
            {
                var id = _hashIdUtilities.DecryptLong(hashId);
                var gift = await _context.GiftEmployees.FindAsync(id);
                var result = _mapperService.Mapper.Map<GiftEmployeesDTO>(gift);
                return new ActualResult<GiftEmployeesDTO> { Result = result };
            }
            catch (Exception)
            {
                return new ActualResult<GiftEmployeesDTO>(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult> CreateAsync(GiftEmployeesDTO item)
        {
            try
            {
                await _context.GiftEmployees.AddAsync(_mapperService.Mapper.Map<GiftEmployees>(item));
                await _context.SaveChangesAsync();
                return new ActualResult();
            }
            catch (Exception)
            {
                return new ActualResult(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult> UpdateAsync(GiftEmployeesDTO item)
        {
            try
            {
                _context.Entry(_mapperService.Mapper.Map<GiftEmployees>(item)).State = EntityState.Modified;
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
                var result = await _context.GiftEmployees.FindAsync(id);
                if (result != null)
                {
                    _context.GiftEmployees.Remove(result);
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