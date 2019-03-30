using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.DTO.Family;
using TradeUnionCommittee.BLL.Interfaces.Lists.Family;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.Common.Enums;
using TradeUnionCommittee.DAL.EF;

namespace TradeUnionCommittee.BLL.Services.Lists.Family
{
    internal class FamilyService : IFamilyService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly IAutoMapperConfiguration _mapperService;
        private readonly HashIdConfiguration _hashIdUtilities;

        public FamilyService(TradeUnionCommitteeContext context, IAutoMapperConfiguration mapperService, HashIdConfiguration hashIdUtilities)
        {
            _context = context;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<IEnumerable<FamilyDTO>>> GetAllAsync(string hashIdEmployee)
        {
            try
            {
                var id = _hashIdUtilities.DecryptLong(hashIdEmployee);
                var family = await _context.Family
                    .Where(x => x.IdEmployee == id)
                    .ToListAsync();
                var result = _mapperService.Mapper.Map<IEnumerable<FamilyDTO>>(family);
                return new ActualResult<IEnumerable<FamilyDTO>> { Result = result };
            }
            catch (Exception)
            {
                return new ActualResult<IEnumerable<FamilyDTO>>(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult<FamilyDTO>> GetAsync(string hashId)
        {
            try
            {
                var id = _hashIdUtilities.DecryptLong(hashId);
                var family = await _context.Family.FindAsync(id);
                var result = _mapperService.Mapper.Map<FamilyDTO>(family);
                return new ActualResult<FamilyDTO> { Result = result };
            }
            catch (Exception)
            {
                return new ActualResult<FamilyDTO>(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult> CreateAsync(FamilyDTO item)
        {
            try
            {
                await _context.Family.AddAsync(_mapperService.Mapper.Map<DAL.Entities.Family>(item));
                await _context.SaveChangesAsync();
                return new ActualResult();
            }
            catch (Exception)
            {
                return new ActualResult(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult> UpdateAsync(FamilyDTO item)
        {
            try
            {
                _context.Entry(_mapperService.Mapper.Map<DAL.Entities.Family>(item)).State = EntityState.Modified;
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
                var result = await _context.Family.FindAsync(id);
                if (result != null)
                {
                    _context.Family.Remove(result);
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