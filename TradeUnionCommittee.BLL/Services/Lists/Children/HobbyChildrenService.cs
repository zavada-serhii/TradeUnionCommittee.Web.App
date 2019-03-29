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

namespace TradeUnionCommittee.BLL.Services.Lists.Children
{
    public class HobbyChildrenService : IHobbyChildrenService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly IAutoMapperConfiguration _mapperService;
        private readonly IHashIdConfiguration _hashIdUtilities;

        public HobbyChildrenService(TradeUnionCommitteeContext context, IAutoMapperConfiguration mapperService, IHashIdConfiguration hashIdUtilities)
        {
            _context = context;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<IEnumerable<HobbyChildrenDTO>>> GetAllAsync(string hashIdChildren)
        {
            try
            {
                var id = _hashIdUtilities.DecryptLong(hashIdChildren);
                var hobby = await _context.HobbyChildrens
                    .Include(x => x.IdHobbyNavigation)
                    .Where(x => x.IdChildren == id)
                    .OrderBy(x => x.IdHobbyNavigation.Name)
                    .ToListAsync();
                var result = _mapperService.Mapper.Map<IEnumerable<HobbyChildrenDTO>>(hobby);
                return new ActualResult<IEnumerable<HobbyChildrenDTO>> { Result = result };
            }
            catch (Exception)
            {
                return new ActualResult<IEnumerable<HobbyChildrenDTO>>(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult<HobbyChildrenDTO>> GetAsync(string hashId)
        {
            try
            {
                var id = _hashIdUtilities.DecryptLong(hashId);
                var hobby = await _context.HobbyChildrens
                    .Include(x => x.IdHobbyNavigation)
                    .FirstOrDefaultAsync(x => x.Id == id);
                var result = _mapperService.Mapper.Map<HobbyChildrenDTO>(hobby);
                return new ActualResult<HobbyChildrenDTO> { Result = result };
            }
            catch (Exception)
            {
                return new ActualResult<HobbyChildrenDTO>(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult> CreateAsync(HobbyChildrenDTO item)
        {
            try
            {
                await _context.HobbyChildrens.AddAsync(_mapperService.Mapper.Map<HobbyChildrens>(item));
                await _context.SaveChangesAsync();
                return new ActualResult();
            }
            catch (Exception)
            {
                return new ActualResult(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult> UpdateAsync(HobbyChildrenDTO item)
        {
            try
            {
                _context.Entry(_mapperService.Mapper.Map<HobbyChildrens>(item)).State = EntityState.Modified;
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
                var result = await _context.HobbyChildrens.FindAsync(id);
                if (result != null)
                {
                    _context.HobbyChildrens.Remove(result);
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