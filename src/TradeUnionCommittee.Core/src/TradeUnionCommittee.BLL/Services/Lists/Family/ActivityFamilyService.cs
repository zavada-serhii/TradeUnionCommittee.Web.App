using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.ActualResults;
using TradeUnionCommittee.BLL.Contracts.Lists.Family;
using TradeUnionCommittee.BLL.DTO.Family;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.BLL.Helpers;
using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.BLL.Services.Lists.Family
{
    internal class ActivityFamilyService : IActivityFamilyService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly IMapper _mapper;

        public ActivityFamilyService(TradeUnionCommitteeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActualResult<IEnumerable<ActivityFamilyDTO>>> GetAllAsync(string hashIdFamily)
        {
            try
            {
                var id = HashHelper.DecryptLong(hashIdFamily);
                var activity = await _context.ActivityFamily
                    .Include(x => x.IdActivitiesNavigation)
                    .Where(x => x.IdFamily == id)
                    .OrderByDescending(x => x.DateEvent)
                    .ToListAsync();
                var result = _mapper.Map<IEnumerable<ActivityFamilyDTO>>(activity);
                return new ActualResult<IEnumerable<ActivityFamilyDTO>> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<ActivityFamilyDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<ActivityFamilyDTO>> GetAsync(string hashId)
        {
            try
            {
                var id = HashHelper.DecryptLong(hashId);
                var activity = await _context.ActivityFamily
                    .Include(x => x.IdActivitiesNavigation)
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (activity == null)
                {
                    return new ActualResult<ActivityFamilyDTO>(Errors.TupleDeleted);
                }
                var result = _mapper.Map<ActivityFamilyDTO>(activity);
                return new ActualResult<ActivityFamilyDTO> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<ActivityFamilyDTO>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<string>> CreateAsync(ActivityFamilyDTO item)
        {
            try
            {
                var activityFamily = _mapper.Map<ActivityFamily>(item);
                await _context.ActivityFamily.AddAsync(activityFamily);
                await _context.SaveChangesAsync();
                var hashId = HashHelper.EncryptLong(activityFamily.Id);
                return new ActualResult<string> { Result = hashId };
            }
            catch (Exception exception)
            {
                return new ActualResult<string>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> UpdateAsync(ActivityFamilyDTO item)
        {
            try
            {
                _context.Entry(_mapper.Map<ActivityFamily>(item)).State = EntityState.Modified;
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
                var id = HashHelper.DecryptLong(hashId);
                var result = await _context.ActivityFamily.FindAsync(id);
                if (result != null)
                {
                    _context.ActivityFamily.Remove(result);
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