using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.ActualResults;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.DTO.GrandChildren;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.BLL.Helpers;
using TradeUnionCommittee.BLL.Interfaces.Lists.GrandChildren;
using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Enums;

namespace TradeUnionCommittee.BLL.Services.Lists.GrandChildren
{
    internal class TourGrandChildrenService : ITourGrandChildrenService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly IMapper _mapper;

        public TourGrandChildrenService(TradeUnionCommitteeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActualResult<IEnumerable<TourGrandChildrenDTO>>> GetAllAsync(string hashIdGrandChildren)
        {
            try
            {
                var id = HashId.DecryptLong(hashIdGrandChildren);
                var tour = await _context.EventGrandChildrens
                    .Include(x => x.IdEventNavigation)
                    .Where(x => x.IdGrandChildren == id && x.IdEventNavigation.Type == TypeEvent.Tour)
                    .OrderByDescending(x => x.StartDate)
                    .ToListAsync();
                var result = _mapper.Map<IEnumerable<TourGrandChildrenDTO>>(tour);
                return new ActualResult<IEnumerable<TourGrandChildrenDTO>> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<TourGrandChildrenDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<TourGrandChildrenDTO>> GetAsync(string hashId)
        {
            try
            {
                var id = HashId.DecryptLong(hashId);
                var tour = await _context.EventGrandChildrens
                    .Include(x => x.IdEventNavigation)
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (tour == null)
                {
                    return new ActualResult<TourGrandChildrenDTO>(Errors.TupleDeleted);
                }
                var result = _mapper.Map<TourGrandChildrenDTO>(tour);
                return new ActualResult<TourGrandChildrenDTO> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<TourGrandChildrenDTO>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<string>> CreateAsync(TourGrandChildrenDTO item)
        {
            try
            {
                var tourGrandChildren = _mapper.Map<EventGrandChildrens>(item);
                await _context.EventGrandChildrens.AddAsync(tourGrandChildren);
                await _context.SaveChangesAsync();
                var hashId = HashId.EncryptLong(tourGrandChildren.Id);
                return new ActualResult<string> { Result = hashId };
            }
            catch (Exception exception)
            {
                return new ActualResult<string>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> UpdateAsync(TourGrandChildrenDTO item)
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
                var id = HashId.DecryptLong(hashId);
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