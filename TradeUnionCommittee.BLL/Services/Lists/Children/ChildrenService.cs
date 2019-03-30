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

namespace TradeUnionCommittee.BLL.Services.Lists.Children
{
    internal class ChildrenService : IChildrenService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly AutoMapperConfiguration _mapperService;
        private readonly HashIdConfiguration _hashIdUtilities;

        public ChildrenService(TradeUnionCommitteeContext context, AutoMapperConfiguration mapperService, HashIdConfiguration hashIdUtilities)
        {
            _context = context;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<IEnumerable<ChildrenDTO>>> GetAllAsync(string hashIdEmployee)
        {
            try
            {
                var id = _hashIdUtilities.DecryptLong(hashIdEmployee);
                var children = await _context.Children
                    .Where(x => x.IdEmployee == id)
                    .OrderByDescending(x => x.BirthDate)
                    .ToListAsync();
                var result = _mapperService.Mapper.Map<IEnumerable<ChildrenDTO>>(children);
                return new ActualResult<IEnumerable<ChildrenDTO>> { Result = result };
            }
            catch (Exception)
            {
                return new ActualResult<IEnumerable<ChildrenDTO>>(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult<ChildrenDTO>> GetAsync(string hashId)
        {
            try
            {
                var id = _hashIdUtilities.DecryptLong(hashId);
                var children = await _context.Children.FindAsync(id);
                var result = _mapperService.Mapper.Map<ChildrenDTO>(children);
                return new ActualResult<ChildrenDTO> { Result = result };
            }
            catch (Exception)
            {
                return new ActualResult<ChildrenDTO>(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult> CreateAsync(ChildrenDTO item)
        {
            try
            {
                await _context.Children.AddAsync(_mapperService.Mapper.Map<DAL.Entities.Children>(item));
                await _context.SaveChangesAsync();
                return new ActualResult();
            }
            catch (Exception)
            {
                return new ActualResult(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult> UpdateAsync(ChildrenDTO item)
        {
            try
            {
                _context.Entry(_mapperService.Mapper.Map<DAL.Entities.Children>(item)).State = EntityState.Modified;
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
                var result = await _context.Children.FindAsync(id);
                if (result != null)
                {
                    _context.Children.Remove(result);
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