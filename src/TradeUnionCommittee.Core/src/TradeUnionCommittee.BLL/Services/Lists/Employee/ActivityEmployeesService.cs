using AutoMapper;
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
    internal class ActivityEmployeesService : IActivityEmployeesService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly IMapper _mapper;

        public ActivityEmployeesService(TradeUnionCommitteeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActualResult<IEnumerable<ActivityEmployeesDTO>>> GetAllAsync(string hashIdEmployee)
        {
            try
            {
                var id = HashId.DecryptLong(hashIdEmployee);
                var activity = await _context.ActivityEmployees
                    .Include(x => x.IdActivitiesNavigation)
                    .Where(x => x.IdEmployee == id)
                    .OrderByDescending(x => x.DateEvent)
                    .ToListAsync();
                var result = _mapper.Map<IEnumerable<ActivityEmployeesDTO>>(activity);
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
                var id = HashId.DecryptLong(hashId);
                var activity = await _context.ActivityEmployees
                    .Include(x => x.IdActivitiesNavigation)
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (activity == null)
                {
                    return new ActualResult<ActivityEmployeesDTO>(Errors.TupleDeleted);
                }
                var result = _mapper.Map<ActivityEmployeesDTO>(activity);
                return new ActualResult<ActivityEmployeesDTO> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<ActivityEmployeesDTO>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<string>> CreateAsync(ActivityEmployeesDTO item)
        {
            try
            {
                var activityEmployees = _mapper.Map<ActivityEmployees>(item);
                await _context.ActivityEmployees.AddAsync(activityEmployees);
                await _context.SaveChangesAsync();
                var hashId = HashId.EncryptLong(activityEmployees.Id);
                return new ActualResult<string> { Result = hashId };
            }
            catch (Exception exception)
            {
                return new ActualResult<string>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> UpdateAsync(ActivityEmployeesDTO item)
        {
            try
            {
                _context.Entry(_mapper.Map<ActivityEmployees>(item)).State = EntityState.Modified;
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
                var id = HashId.DecryptLong(hashId);
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