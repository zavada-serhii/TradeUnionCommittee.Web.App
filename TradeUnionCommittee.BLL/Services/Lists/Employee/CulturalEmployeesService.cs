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
    public class CulturalEmployeesService : ICulturalEmployeesService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly IAutoMapperConfiguration _mapperService;
        private readonly IHashIdConfiguration _hashIdUtilities;

        public CulturalEmployeesService(TradeUnionCommitteeContext context, IAutoMapperConfiguration mapperService, IHashIdConfiguration hashIdUtilities)
        {
            _context = context;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<IEnumerable<CulturalEmployeesDTO>>> GetAllAsync(string hashIdEmployee)
        {
            try
            {
                var id = _hashIdUtilities.DecryptLong(hashIdEmployee, Enums.Services.Employee);
                var cultural = await _context.CulturalEmployees
                    .Include(x => x.IdCulturalNavigation)
                    .Where(x => x.IdEmployee == id)
                    .OrderByDescending(x => x.DateVisit)
                    .ToListAsync();
                var result = _mapperService.Mapper.Map<IEnumerable<CulturalEmployeesDTO>>(cultural);
                return new ActualResult<IEnumerable<CulturalEmployeesDTO>> { Result = result };
            }
            catch (Exception)
            {
                return new ActualResult<IEnumerable<CulturalEmployeesDTO>>(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult<CulturalEmployeesDTO>> GetAsync(string hashId)
        {
            try
            {
                var id = _hashIdUtilities.DecryptLong(hashId, Enums.Services.CulturalEmployees);
                var cultural = await _context.CulturalEmployees
                    .Include(x => x.IdCulturalNavigation)
                    .FirstOrDefaultAsync(x => x.Id == id);
                var result = _mapperService.Mapper.Map<CulturalEmployeesDTO>(cultural);
                return new ActualResult<CulturalEmployeesDTO> { Result = result };
            }
            catch (Exception)
            {
                return new ActualResult<CulturalEmployeesDTO>(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult> CreateAsync(CulturalEmployeesDTO item)
        {
            try
            {
                await _context.CulturalEmployees.AddAsync(_mapperService.Mapper.Map<CulturalEmployees>(item));
                await _context.SaveChangesAsync();
                return new ActualResult();
            }
            catch (Exception)
            {
                return new ActualResult(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult> UpdateAsync(CulturalEmployeesDTO item)
        {
            try
            {
                _context.Entry(_mapperService.Mapper.Map<CulturalEmployees>(item)).State = EntityState.Modified;
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
                var id = _hashIdUtilities.DecryptLong(hashId, Enums.Services.CulturalEmployees);
                var result = await _context.CulturalEmployees.FindAsync(id);
                if (result != null)
                {
                    _context.CulturalEmployees.Remove(result);
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