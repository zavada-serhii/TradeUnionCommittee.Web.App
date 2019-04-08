using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.DTO.Employee;
using TradeUnionCommittee.BLL.Helpers;
using TradeUnionCommittee.BLL.Interfaces.Lists.Employee;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.Common.Enums;
using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.BLL.Services.Lists.Employee
{
    internal class ActivityEmployeesService : IActivityEmployeesService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly AutoMapperConfiguration _mapperService;
        private readonly HashIdConfiguration _hashIdUtilities;

        public ActivityEmployeesService(TradeUnionCommitteeContext context, AutoMapperConfiguration mapperService, HashIdConfiguration hashIdUtilities)
        {
            _context = context;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<IEnumerable<ActivityEmployeesDTO>>> GetAllAsync(string hashIdEmployee)
        {
            try
            {
                var id = _hashIdUtilities.DecryptLong(hashIdEmployee);
                var activity = await _context.ActivityEmployees
                    .Include(x => x.IdActivitiesNavigation)
                    .Where(x => x.IdEmployee == id)
                    .OrderByDescending(x => x.DateEvent)
                    .ToListAsync();
                var result = _mapperService.Mapper.Map<IEnumerable<ActivityEmployeesDTO>>(activity);
                return new ActualResult<IEnumerable<ActivityEmployeesDTO>> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<ActivityEmployeesDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<ActivityEmployeesDTO>> GetAsync(string hashId)
        {
            try
            {
                var id = _hashIdUtilities.DecryptLong(hashId);
                var activity = await _context.ActivityEmployees
                    .Include(x => x.IdActivitiesNavigation)
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (activity == null)
                {
                    return new ActualResult<ActivityEmployeesDTO>(Errors.TupleDeleted);
                }
                var result = _mapperService.Mapper.Map<ActivityEmployeesDTO>(activity);
                return new ActualResult<ActivityEmployeesDTO> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<ActivityEmployeesDTO>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> CreateAsync(ActivityEmployeesDTO item)
        {
            try
            {
                await _context.ActivityEmployees.AddAsync(_mapperService.Mapper.Map<ActivityEmployees>(item));
                await _context.SaveChangesAsync();
                return new ActualResult();
            }
            catch (Exception exception)
            {
                return new ActualResult(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> UpdateAsync(ActivityEmployeesDTO item)
        {
            try
            {
                _context.Entry(_mapperService.Mapper.Map<ActivityEmployees>(item)).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return new ActualResult();
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
                var result = await _context.ActivityEmployees.FindAsync(id);
                if (result != null)
                {
                    _context.ActivityEmployees.Remove(result);
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
    }
}