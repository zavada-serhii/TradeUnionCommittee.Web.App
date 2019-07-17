using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.ActualResults;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.DTO.Children;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.BLL.Helpers;
using TradeUnionCommittee.BLL.Interfaces.Lists.Children;
using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Enums;

namespace TradeUnionCommittee.BLL.Services.Lists.Children
{
    internal class TravelChildrenService : ITravelChildrenService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly AutoMapperConfiguration _mapperService;
        private readonly HashIdConfiguration _hashIdUtilities;

        public TravelChildrenService(TradeUnionCommitteeContext context, AutoMapperConfiguration mapperService, HashIdConfiguration hashIdUtilities)
        {
            _context = context;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<IEnumerable<TravelChildrenDTO>>> GetAllAsync(string hashIdChildren)
        {
            try
            {
                var id = _hashIdUtilities.DecryptLong(hashIdChildren);
                var travel = await _context.EventChildrens
                    .Include(x => x.IdEventNavigation)
                    .Where(x => x.IdChildren == id && x.IdEventNavigation.Type == TypeEvent.Travel)
                    .OrderByDescending(x => x.StartDate)
                    .ToListAsync();
                var result = _mapperService.Mapper.Map<IEnumerable<TravelChildrenDTO>>(travel);
                return new ActualResult<IEnumerable<TravelChildrenDTO>> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<TravelChildrenDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<TravelChildrenDTO>> GetAsync(string hashId)
        {
            try
            {
                var id = _hashIdUtilities.DecryptLong(hashId);
                var travel = await _context.EventChildrens
                    .Include(x => x.IdEventNavigation)
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (travel == null)
                {
                    return new ActualResult<TravelChildrenDTO>(Errors.TupleDeleted);
                }
                var result = _mapperService.Mapper.Map<TravelChildrenDTO>(travel);
                return new ActualResult<TravelChildrenDTO> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<TravelChildrenDTO>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<string>> CreateAsync(TravelChildrenDTO item)
        {
            try
            {
                var travelChildren = _mapperService.Mapper.Map<EventChildrens>(item);
                await _context.EventChildrens.AddAsync(travelChildren);
                await _context.SaveChangesAsync();
                var hashId = _hashIdUtilities.EncryptLong(travelChildren.Id);
                return new ActualResult<string> { Result = hashId };
            }
            catch (Exception exception)
            {
                return new ActualResult<string>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> UpdateAsync(TravelChildrenDTO item)
        {
            try
            {
                _context.Entry(_mapperService.Mapper.Map<EventChildrens>(item)).State = EntityState.Modified;
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
                var result = await _context.EventChildrens.FindAsync(id);
                if (result != null)
                {
                    _context.EventChildrens.Remove(result);
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