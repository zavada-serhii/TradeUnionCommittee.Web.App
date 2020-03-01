using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.ActualResults;
using TradeUnionCommittee.BLL.Contracts.Directory;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.BLL.Helpers;
using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Enums;

namespace TradeUnionCommittee.BLL.Services.Directory
{
    internal class TourService : ITourService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly IMapper _mapper;

        public TourService(TradeUnionCommitteeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActualResult<IEnumerable<TourDTO>>> GetAllAsync()
        {
            try
            {
                var tour = await _context.Event.Where(x => x.Type == TypeEvent.Tour).OrderBy(x => x.Name).ToListAsync();
                var result = _mapper.Map<IEnumerable<TourDTO>>(tour);
                return new ActualResult<IEnumerable<TourDTO>> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<TourDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<TourDTO>> GetAsync(string hashId)
        {
            try
            {
                var id = HashHelper.DecryptLong(hashId);
                var tour = await _context.Event.FindAsync(id);
                if (tour == null)
                {
                    return new ActualResult<TourDTO>(Errors.TupleDeleted);
                }
                var result = _mapper.Map<TourDTO>(tour);
                return new ActualResult<TourDTO> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<TourDTO>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<string>> CreateAsync(TourDTO dto)
        {
            try
            {
                var tour = _mapper.Map<Event>(dto);
                await _context.Event.AddAsync(tour);
                await _context.SaveChangesAsync();
                var hashId = HashHelper.EncryptLong(tour.Id);
                return new ActualResult<string> { Result = hashId };
            }
            catch (Exception exception)
            {
                return new ActualResult<string>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> UpdateAsync(TourDTO dto)
        {
            try
            {
                _context.Entry(_mapper.Map<Event>(dto)).State = EntityState.Modified;
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
                var result = await _context.Event.FindAsync(id);
                if (result != null)
                {
                    _context.Event.Remove(result);
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