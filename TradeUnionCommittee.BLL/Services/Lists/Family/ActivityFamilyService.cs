using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.DTO.Family;
using TradeUnionCommittee.BLL.Interfaces.Lists.Family;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.Common.Enums;
using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.BLL.Services.Lists.Family
{
    public class ActivityFamilyService : IActivityFamilyService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly IAutoMapperConfiguration _mapperService;
        private readonly IHashIdConfiguration _hashIdUtilities;

        public ActivityFamilyService(TradeUnionCommitteeContext context, IAutoMapperConfiguration mapperService, IHashIdConfiguration hashIdUtilities)
        {
            _context = context;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<IEnumerable<ActivityFamilyDTO>>> GetAllAsync(string hashIdFamily)
        {
            try
            {
                var id = _hashIdUtilities.DecryptLong(hashIdFamily, Enums.Services.Family);
                var activity = await _context.ActivityFamily
                    .Include(x => x.IdActivitiesNavigation)
                    .Where(x => x.IdFamily == id)
                    .OrderByDescending(x => x.DateEvent)
                    .ToListAsync();
                var result = _mapperService.Mapper.Map<IEnumerable<ActivityFamilyDTO>>(activity);
                return new ActualResult<IEnumerable<ActivityFamilyDTO>> { Result = result };
            }
            catch (Exception)
            {
                return new ActualResult<IEnumerable<ActivityFamilyDTO>>(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult<ActivityFamilyDTO>> GetAsync(string hashId)
        {
            try
            {
                var id = _hashIdUtilities.DecryptLong(hashId, Enums.Services.ActivityFamily);
                var activity = await _context.ActivityFamily
                    .Include(x => x.IdActivitiesNavigation)
                    .FirstOrDefaultAsync(x => x.Id == id);
                var result = _mapperService.Mapper.Map<ActivityFamilyDTO>(activity);
                return new ActualResult<ActivityFamilyDTO> { Result = result };
            }
            catch (Exception)
            {
                return new ActualResult<ActivityFamilyDTO>(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult> CreateAsync(ActivityFamilyDTO item)
        {
            try
            {
                await _context.ActivityFamily.AddAsync(_mapperService.Mapper.Map<ActivityFamily>(item));
                await _context.SaveChangesAsync();
                return new ActualResult();
            }
            catch (Exception)
            {
                return new ActualResult(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult> UpdateAsync(ActivityFamilyDTO item)
        {
            try
            {
                _context.Entry(_mapperService.Mapper.Map<ActivityFamily>(item)).State = EntityState.Modified;
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
                var id = _hashIdUtilities.DecryptLong(hashId, Enums.Services.ActivityFamily);
                var result = await _context.ActivityFamily.FindAsync(id);
                if (result != null)
                {
                    _context.ActivityFamily.Remove(result);
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