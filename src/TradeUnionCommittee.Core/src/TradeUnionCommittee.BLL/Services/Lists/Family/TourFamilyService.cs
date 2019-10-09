using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.ActualResults;
using TradeUnionCommittee.BLL.DTO.Family;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.BLL.Helpers;
using TradeUnionCommittee.BLL.Interfaces.Lists.Family;
using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Enums;

namespace TradeUnionCommittee.BLL.Services.Lists.Family
{
    internal class TourFamilyService : ITourFamilyService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly IMapper _mapper;

        public TourFamilyService(TradeUnionCommitteeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActualResult<IEnumerable<TourFamilyDTO>>> GetAllAsync(string hashIdFamily)
        {
            try
            {
                var id = HashHelper.DecryptLong(hashIdFamily);
                var tour = await _context.EventFamily
                    .Include(x => x.IdEventNavigation)
                    .Where(x => x.IdFamily == id && x.IdEventNavigation.Type == TypeEvent.Tour)
                    .OrderByDescending(x => x.StartDate)
                    .ToListAsync();
                var result = _mapper.Map<IEnumerable<TourFamilyDTO>>(tour);
                return new ActualResult<IEnumerable<TourFamilyDTO>> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<TourFamilyDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<TourFamilyDTO>> GetAsync(string hashId)
        {
            try
            {
                var id = HashHelper.DecryptLong(hashId);
                var tour = await _context.EventFamily
                    .Include(x => x.IdEventNavigation)
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (tour == null)
                {
                    return new ActualResult<TourFamilyDTO>(Errors.TupleDeleted);
                }
                var result = _mapper.Map<TourFamilyDTO>(tour);
                return new ActualResult<TourFamilyDTO> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<TourFamilyDTO>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<string>> CreateAsync(TourFamilyDTO item)
        {
            try
            {
                var tourFamily = _mapper.Map<EventFamily>(item);
                await _context.EventFamily.AddAsync(tourFamily);
                await _context.SaveChangesAsync();
                var hashId = HashHelper.EncryptLong(tourFamily.Id);
                return new ActualResult<string> { Result = hashId };
            }
            catch (Exception exception)
            {
                return new ActualResult<string>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> UpdateAsync(TourFamilyDTO item)
        {
            try
            {
                _context.Entry(_mapper.Map<EventFamily>(item)).State = EntityState.Modified;
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
                var result = await _context.EventFamily.FindAsync(id);
                if (result != null)
                {
                    _context.EventFamily.Remove(result);
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