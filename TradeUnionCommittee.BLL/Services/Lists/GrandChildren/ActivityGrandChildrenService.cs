using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.DTO.GrandChildren;
using TradeUnionCommittee.BLL.Interfaces.Lists.GrandChildren;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.Common.Enums;
using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.BLL.Services.Lists.GrandChildren
{
    internal class ActivityGrandChildrenService : IActivityGrandChildrenService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly AutoMapperConfiguration _mapperService;
        private readonly HashIdConfiguration _hashIdUtilities;

        public ActivityGrandChildrenService(TradeUnionCommitteeContext context, AutoMapperConfiguration mapperService, HashIdConfiguration hashIdUtilities)
        {
            _context = context;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<IEnumerable<ActivityGrandChildrenDTO>>> GetAllAsync(string hashIdGrandChildren)
        {
            try
            {
                var id = _hashIdUtilities.DecryptLong(hashIdGrandChildren);
                var activity = await _context.ActivityGrandChildrens
                    .Include(x => x.IdActivitiesNavigation)
                    .Where(x => x.IdGrandChildren == id)
                    .OrderByDescending(x => x.DateEvent)
                    .ToListAsync();
                var result = _mapperService.Mapper.Map<IEnumerable<ActivityGrandChildrenDTO>>(activity);
                return new ActualResult<IEnumerable<ActivityGrandChildrenDTO>> { Result = result };
            }
            catch (Exception)
            {
                return new ActualResult<IEnumerable<ActivityGrandChildrenDTO>>(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult<ActivityGrandChildrenDTO>> GetAsync(string hashId)
        {
            try
            {
                var id = _hashIdUtilities.DecryptLong(hashId);
                var activity = await _context.ActivityGrandChildrens
                    .Include(x => x.IdActivitiesNavigation)
                    .FirstOrDefaultAsync(x => x.Id == id);
                var result = _mapperService.Mapper.Map<ActivityGrandChildrenDTO>(activity);
                return new ActualResult<ActivityGrandChildrenDTO> { Result = result };
            }
            catch (Exception)
            {
                return new ActualResult<ActivityGrandChildrenDTO>(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult> CreateAsync(ActivityGrandChildrenDTO item)
        {
            try
            {
                await _context.ActivityGrandChildrens.AddAsync(_mapperService.Mapper.Map<ActivityGrandChildrens>(item));
                await _context.SaveChangesAsync();
                return new ActualResult();
            }
            catch (Exception)
            {
                return new ActualResult(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult> UpdateAsync(ActivityGrandChildrenDTO item)
        {
            try
            {
                _context.Entry(_mapperService.Mapper.Map<ActivityGrandChildrens>(item)).State = EntityState.Modified;
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
                var result = await _context.ActivityGrandChildrens.FindAsync(id);
                if (result != null)
                {
                    _context.ActivityGrandChildrens.Remove(result);
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