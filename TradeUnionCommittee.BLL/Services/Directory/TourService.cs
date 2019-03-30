using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.DTO;
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
        private readonly IAutoMapperConfiguration _mapperService;
        private readonly HashIdConfiguration _hashIdUtilities;

        public TourService(TradeUnionCommitteeContext context, IAutoMapperConfiguration mapperService, HashIdConfiguration hashIdUtilities)
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
            catch (Exception)
            {
                return new ActualResult<IEnumerable<TourDTO>>(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult<TourDTO>> GetAsync(string hashId)
        {
            try
            {
                var id = _hashIdUtilities.DecryptLong(hashId);
                var tour = await _context.Event.FindAsync(id);
                var result = _mapperService.Mapper.Map<TourDTO>(tour);
                return new ActualResult<TourDTO> { Result = result };
            }
            catch (Exception)
            {
                return new ActualResult<TourDTO>(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult> CreateAsync(TourDTO dto)
        {
            try
            {
                if (!await CheckNameAsync(dto.Name))
                {
                    await _context.Event.AddAsync(_mapperService.Mapper.Map<Event>(dto));
                    await _context.SaveChangesAsync();
                    return new ActualResult();
                }
                return new ActualResult(Errors.DuplicateData);
            }
            catch (Exception)
            {
                return new ActualResult(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult> UpdateAsync(TourDTO dto)
        {
            try
            {
                if (!await CheckNameAsync(dto.Name))
                {
                    _context.Entry(_mapperService.Mapper.Map<Event>(dto)).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return new ActualResult();
                }
                return new ActualResult(Errors.DuplicateData);
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
                var result = await _context.Event.FindAsync(id);
                if (result != null)
                {
                    _context.Event.Remove(result);
                    await _context.SaveChangesAsync();
                }
                return new ActualResult();
            }
            catch (Exception)
            {
                return new ActualResult(Errors.DataBaseError);
            }
        }

        public async Task<bool> CheckNameAsync(string name)
        {
            return await _context.Event.AnyAsync(p => p.Name == name && p.Type == TypeEvent.Tour);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}