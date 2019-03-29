using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.DTO.Children;
using TradeUnionCommittee.BLL.Interfaces.Lists.Children;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.Common.Enums;
using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.BLL.Services.Lists.Children
{
    public class ActivityChildrenService : IActivityChildrenService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly IAutoMapperConfiguration _mapperService;
        private readonly IHashIdConfiguration _hashIdUtilities;

        public ActivityChildrenService(TradeUnionCommitteeContext context, IAutoMapperConfiguration mapperService, IHashIdConfiguration hashIdUtilities)
        {
            _context = context;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<IEnumerable<ActivityChildrenDTO>>> GetAllAsync(string hashIdChildren)
        {
            try
            {
                var id = _hashIdUtilities.DecryptLong(hashIdChildren);
                var activity = await _context.ActivityChildrens
                    .Include(x => x.IdActivitiesNavigation)
                    .Where(x => x.IdChildren == id)
                    .OrderByDescending(x => x.DateEvent)
                    .ToListAsync();
                var result = _mapperService.Mapper.Map<IEnumerable<ActivityChildrenDTO>>(activity);
                return new ActualResult<IEnumerable<ActivityChildrenDTO>> { Result = result };
            }
            catch (Exception)
            {
                return new ActualResult<IEnumerable<ActivityChildrenDTO>>(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult<ActivityChildrenDTO>> GetAsync(string hashId)
        {
            try
            {
                var id = _hashIdUtilities.DecryptLong(hashId);
                var activity = await _context.ActivityChildrens
                    .Include(x => x.IdActivitiesNavigation)
                    .FirstOrDefaultAsync(x => x.Id == id);
                var result = _mapperService.Mapper.Map<ActivityChildrenDTO>(activity);
                return new ActualResult<ActivityChildrenDTO> { Result = result };
            }
            catch (Exception)
            {
                return new ActualResult<ActivityChildrenDTO>(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult> CreateAsync(ActivityChildrenDTO item)
        {
            try
            {
                await _context.ActivityChildrens.AddAsync(_mapperService.Mapper.Map<ActivityChildrens>(item));
                await _context.SaveChangesAsync();
                return new ActualResult();
            }
            catch (Exception)
            {
                return new ActualResult(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult> UpdateAsync(ActivityChildrenDTO item)
        {
            try
            {
                _context.Entry(_mapperService.Mapper.Map<ActivityChildrens>(item)).State = EntityState.Modified;
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
                var result = await _context.ActivityChildrens.FindAsync(id);
                if (result != null)
                {
                    _context.ActivityChildrens.Remove(result);
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