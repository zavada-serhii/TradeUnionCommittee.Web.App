using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.ActualResults;
using TradeUnionCommittee.BLL.DTO.Children;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.BLL.Helpers;
using TradeUnionCommittee.BLL.Interfaces.Lists.Children;
using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.BLL.Services.Lists.Children
{
    internal class ActivityChildrenService : IActivityChildrenService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly IMapper _mapper;

        public ActivityChildrenService(TradeUnionCommitteeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActualResult<IEnumerable<ActivityChildrenDTO>>> GetAllAsync(string hashIdChildren)
        {
            try
            {
                var id = HashHelper.DecryptLong(hashIdChildren);
                var activity = await _context.ActivityChildrens
                    .Include(x => x.IdActivitiesNavigation)
                    .Where(x => x.IdChildren == id)
                    .OrderByDescending(x => x.DateEvent)
                    .ToListAsync();
                var result = _mapper.Map<IEnumerable<ActivityChildrenDTO>>(activity);
                return new ActualResult<IEnumerable<ActivityChildrenDTO>> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<ActivityChildrenDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<ActivityChildrenDTO>> GetAsync(string hashId)
        {
            try
            {
                var id = HashHelper.DecryptLong(hashId);
                var activity = await _context.ActivityChildrens
                    .Include(x => x.IdActivitiesNavigation)
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (activity == null)
                {
                    return new ActualResult<ActivityChildrenDTO>(Errors.TupleDeleted);
                }
                var result = _mapper.Map<ActivityChildrenDTO>(activity);
                return new ActualResult<ActivityChildrenDTO> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<ActivityChildrenDTO>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<string>> CreateAsync(ActivityChildrenDTO item)
        {
            try
            {
                var activityChildren = _mapper.Map<ActivityChildrens>(item);
                await _context.ActivityChildrens.AddAsync(activityChildren);
                await _context.SaveChangesAsync();
                var hashId = HashHelper.EncryptLong(activityChildren.Id);
                return new ActualResult<string> { Result = hashId };
            }
            catch (Exception exception)
            {
                return new ActualResult<string>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> UpdateAsync(ActivityChildrenDTO item)
        {
            try
            {
                _context.Entry(_mapper.Map<ActivityChildrens>(item)).State = EntityState.Modified;
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
                var result = await _context.ActivityChildrens.FindAsync(id);
                if (result != null)
                {
                    _context.ActivityChildrens.Remove(result);
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
    }
}