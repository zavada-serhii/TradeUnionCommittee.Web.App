using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Helpers;
using TradeUnionCommittee.BLL.Interfaces.Directory;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.Common.Enums;
using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Enums;

namespace TradeUnionCommittee.BLL.Services.Directory
{
    internal class TourService : ITourService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly AutoMapperConfiguration _mapperService;
        private readonly HashIdConfiguration _hashIdUtilities;

        public TourService(TradeUnionCommitteeContext context, AutoMapperConfiguration mapperService, HashIdConfiguration hashIdUtilities)
        {
            _context = context;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<IEnumerable<TourDTO>>> GetAllAsync()
        {
            try
            {
                var tour = await _context.Event.Where(x => x.Type == TypeEvent.Tour).OrderBy(x => x.Name).ToListAsync();
                var result = _mapperService.Mapper.Map<IEnumerable<TourDTO>>(tour);
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
                var id = _hashIdUtilities.DecryptLong(hashId);
                var tour = await _context.Event.FindAsync(id);
                if (tour == null)
                {
                    return new ActualResult<TourDTO>(Errors.TupleDeleted);
                }
                var result = _mapperService.Mapper.Map<TourDTO>(tour);
                return new ActualResult<TourDTO> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<TourDTO>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> CreateAsync(TourDTO dto)
        {
            try
            {
                await _context.Event.AddAsync(_mapperService.Mapper.Map<Event>(dto));
                await _context.SaveChangesAsync();
                return new ActualResult();
            }
            catch (Exception exception)
            {
                return new ActualResult<DirectoryDTO>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> UpdateAsync(TourDTO dto)
        {
            try
            {
                _context.Entry(_mapperService.Mapper.Map<Event>(dto)).State = EntityState.Modified;
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