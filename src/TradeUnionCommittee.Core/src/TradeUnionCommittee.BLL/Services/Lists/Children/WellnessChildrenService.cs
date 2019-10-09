using AutoMapper;
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
    internal class WellnessChildrenService : IWellnessChildrenService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly IMapper _mapper;

        public WellnessChildrenService(TradeUnionCommitteeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActualResult<IEnumerable<WellnessChildrenDTO>>> GetAllAsync(string hashIdChildren)
        {
            try
            {
                var id = HashId.DecryptLong(hashIdChildren);
                var wellness = await _context.EventChildrens
                    .Include(x => x.IdEventNavigation)
                    .Where(x => x.IdChildren == id && x.IdEventNavigation.Type == TypeEvent.Wellness)
                    .OrderByDescending(x => x.StartDate)
                    .ToListAsync();
                var result = _mapper.Map<IEnumerable<WellnessChildrenDTO>>(wellness);
                return new ActualResult<IEnumerable<WellnessChildrenDTO>> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<WellnessChildrenDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<WellnessChildrenDTO>> GetAsync(string hashId)
        {
            try
            {
                var id = HashId.DecryptLong(hashId);
                var wellness = await _context.EventChildrens
                    .Include(x => x.IdEventNavigation)
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (wellness == null)
                {
                    return new ActualResult<WellnessChildrenDTO>(Errors.TupleDeleted);
                }
                var result = _mapper.Map<WellnessChildrenDTO>(wellness);
                return new ActualResult<WellnessChildrenDTO> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<WellnessChildrenDTO>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<string>> CreateAsync(WellnessChildrenDTO item)
        {
            try
            {
                var wellnessChildren = _mapper.Map<EventChildrens>(item);
                await _context.EventChildrens.AddAsync(wellnessChildren);
                await _context.SaveChangesAsync();
                var hashId = HashId.EncryptLong(wellnessChildren.Id);
                return new ActualResult<string> { Result = hashId };
            }
            catch (Exception exception)
            {
                return new ActualResult<string>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> UpdateAsync(WellnessChildrenDTO item)
        {
            try
            {
                _context.Entry(_mapper.Map<EventChildrens>(item)).State = EntityState.Modified;
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