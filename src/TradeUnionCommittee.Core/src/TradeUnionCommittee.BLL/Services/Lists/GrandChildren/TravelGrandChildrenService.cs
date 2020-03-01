using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.ActualResults;
using TradeUnionCommittee.BLL.Contracts.Lists.GrandChildren;
using TradeUnionCommittee.BLL.DTO.GrandChildren;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.BLL.Helpers;
using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Enums;

namespace TradeUnionCommittee.BLL.Services.Lists.GrandChildren
{
    internal class TravelGrandChildrenService : ITravelGrandChildrenService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly IMapper _mapper;

        public TravelGrandChildrenService(TradeUnionCommitteeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActualResult<IEnumerable<TravelGrandChildrenDTO>>> GetAllAsync(string hashIdGrandChildren)
        {
            try
            {
                var id = HashHelper.DecryptLong(hashIdGrandChildren);
                var travel = await _context.EventGrandChildrens
                    .Include(x => x.IdEventNavigation)
                    .Where(x => x.IdGrandChildren == id && x.IdEventNavigation.Type == TypeEvent.Travel)
                    .OrderByDescending(x => x.StartDate)
                    .ToListAsync();
                var result = _mapper.Map<IEnumerable<TravelGrandChildrenDTO>>(travel);
                return new ActualResult<IEnumerable<TravelGrandChildrenDTO>> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<TravelGrandChildrenDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<TravelGrandChildrenDTO>> GetAsync(string hashId)
        {
            try
            {
                var id = HashHelper.DecryptLong(hashId);
                var travel = await _context.EventGrandChildrens
                    .Include(x => x.IdEventNavigation)
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (travel == null)
                {
                    return new ActualResult<TravelGrandChildrenDTO>(Errors.TupleDeleted);
                }
                var result = _mapper.Map<TravelGrandChildrenDTO>(travel);
                return new ActualResult<TravelGrandChildrenDTO> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<TravelGrandChildrenDTO>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<string>> CreateAsync(TravelGrandChildrenDTO item)
        {
            try
            {
                var travelGrandChildren = _mapper.Map<EventGrandChildrens>(item);
                await _context.EventGrandChildrens.AddAsync(travelGrandChildren);
                await _context.SaveChangesAsync();
                var hashId = HashHelper.EncryptLong(travelGrandChildren.Id);
                return new ActualResult<string> { Result = hashId };
            }
            catch (Exception exception)
            {
                return new ActualResult<string>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> UpdateAsync(TravelGrandChildrenDTO item)
        {
            try
            {
                _context.Entry(_mapper.Map<EventGrandChildrens>(item)).State = EntityState.Modified;
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
                var result = await _context.EventGrandChildrens.FindAsync(id);
                if (result != null)
                {
                    _context.EventGrandChildrens.Remove(result);
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