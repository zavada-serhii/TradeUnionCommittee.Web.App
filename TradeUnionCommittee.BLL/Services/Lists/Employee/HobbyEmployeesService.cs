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
    public class HobbyEmployeesService : IHobbyEmployeesService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly IAutoMapperConfiguration _mapperService;
        private readonly IHashIdConfiguration _hashIdUtilities;

        public HobbyEmployeesService(TradeUnionCommitteeContext context, IAutoMapperConfiguration mapperService, IHashIdConfiguration hashIdUtilities)
        {
            _context = context;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<IEnumerable<HobbyEmployeesDTO>>> GetAllAsync(string hashIdEmployee)
        {
            try
            {
                var id = _hashIdUtilities.DecryptLong(hashIdEmployee);
                var hobby = await _context.HobbyEmployees
                    .Include(x => x.IdHobbyNavigation)
                    .Where(x => x.IdEmployee == id)
                    .OrderBy(x => x.IdHobbyNavigation.Name)
                    .ToListAsync();
                var result = _mapperService.Mapper.Map<IEnumerable<HobbyEmployeesDTO>>(hobby);
                return new ActualResult<IEnumerable<HobbyEmployeesDTO>> { Result = result };
            }
            catch (Exception)
            {
                return new ActualResult<IEnumerable<HobbyEmployeesDTO>>(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult<HobbyEmployeesDTO>> GetAsync(string hashId)
        {
            try
            {
                var id = _hashIdUtilities.DecryptLong(hashId);
                var hobby = await _context.HobbyEmployees
                    .Include(x => x.IdHobbyNavigation)
                    .FirstOrDefaultAsync(x => x.Id == id);
                var result = _mapperService.Mapper.Map<HobbyEmployeesDTO>(hobby);
                return new ActualResult<HobbyEmployeesDTO> { Result = result };
            }
            catch (Exception)
            {
                return new ActualResult<HobbyEmployeesDTO>(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult> CreateAsync(HobbyEmployeesDTO item)
        {
            try
            {
                await _context.HobbyEmployees.AddAsync(_mapperService.Mapper.Map<HobbyEmployees>(item));
                await _context.SaveChangesAsync();
                return new ActualResult();
            }
            catch (Exception)
            {
                return new ActualResult(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult> UpdateAsync(HobbyEmployeesDTO item)
        {
            try
            {
                _context.Entry(_mapperService.Mapper.Map<HobbyEmployees>(item)).State = EntityState.Modified;
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
                var result = await _context.HobbyEmployees.FindAsync(id);
                if (result != null)
                {
                    _context.HobbyEmployees.Remove(result);
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
    }
}