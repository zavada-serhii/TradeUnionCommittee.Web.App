using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.DTO.Children;
using TradeUnionCommittee.BLL.Interfaces.Lists.Children;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.Common.Enums;
using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Enums;

namespace TradeUnionCommittee.BLL.Services.Lists.Children
{
    public class TravelChildrenService : ITravelChildrenService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly IAutoMapperConfiguration _mapperService;
        private readonly IHashIdConfiguration _hashIdUtilities;

        public TravelChildrenService(TradeUnionCommitteeContext context, IAutoMapperConfiguration mapperService, IHashIdConfiguration hashIdUtilities)
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
            catch (Exception)
            {
                return new ActualResult<IEnumerable<TravelChildrenDTO>>(Errors.DataBaseError);
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
                var result = _mapperService.Mapper.Map<TravelChildrenDTO>(travel);
                return new ActualResult<TravelChildrenDTO> { Result = result };
            }
            catch (Exception)
            {
                return new ActualResult<TravelChildrenDTO>(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult> CreateAsync(TravelChildrenDTO item)
        {
            try
            {
                await _context.EventChildrens.AddAsync(_mapperService.Mapper.Map<EventChildrens>(item));
                await _context.SaveChangesAsync();
                return new ActualResult();
            }
            catch (Exception)
            {
                return new ActualResult(Errors.DataBaseError);
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
                var result = await _context.EventChildrens.FindAsync(id);
                if (result != null)
                {
                    _context.EventChildrens.Remove(result);
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