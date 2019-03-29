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
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.BLL.Services.Lists.Family
{
    public class CulturalFamilyService : ICulturalFamilyService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly IAutoMapperConfiguration _mapperService;
        private readonly IHashIdConfiguration _hashIdUtilities;

        public CulturalFamilyService(TradeUnionCommitteeContext context, IAutoMapperConfiguration mapperService, IHashIdConfiguration hashIdUtilities)
        {
            _context = context;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<IEnumerable<CulturalFamilyDTO>>> GetAllAsync(string hashIdFamily)
        {
            try
            {
                var id = _hashIdUtilities.DecryptLong(hashIdFamily, Enums.Services.Family);
                var cultural = await _context.CulturalFamily
                    .Include(x => x.IdCulturalNavigation)
                    .Where(x => x.IdFamily == id)
                    .OrderByDescending(x => x.DateVisit)
                    .ToListAsync();
                var result = _mapperService.Mapper.Map<IEnumerable<CulturalFamilyDTO>>(cultural);
                return new ActualResult<IEnumerable<CulturalFamilyDTO>> { Result = result };
            }
            catch (Exception)
            {
                return new ActualResult<IEnumerable<CulturalFamilyDTO>>(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult<CulturalFamilyDTO>> GetAsync(string hashId)
        {
            try
            {
                var id = _hashIdUtilities.DecryptLong(hashId, Enums.Services.CulturalFamily);
                var cultural = await _context.CulturalFamily
                    .Include(x => x.IdCulturalNavigation)
                    .FirstOrDefaultAsync(x => x.Id == id);
                var result = _mapperService.Mapper.Map<CulturalFamilyDTO>(cultural);
                return new ActualResult<CulturalFamilyDTO> { Result = result };
            }
            catch (Exception)
            {
                return new ActualResult<CulturalFamilyDTO>(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult> CreateAsync(CulturalFamilyDTO item)
        {
            try
            {
                await _context.CulturalFamily.AddAsync(_mapperService.Mapper.Map<CulturalFamily>(item));
                await _context.SaveChangesAsync();
                return new ActualResult();
            }
            catch (Exception)
            {
                return new ActualResult(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult> UpdateAsync(CulturalFamilyDTO item)
        {
            try
            {
                _context.Entry(_mapperService.Mapper.Map<CulturalFamily>(item)).State = EntityState.Modified;
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
                var id = _hashIdUtilities.DecryptLong(hashId, Enums.Services.CulturalFamily);
                var result = await _context.CulturalFamily.FindAsync(id);
                if (result != null)
                {
                    _context.CulturalFamily.Remove(result);
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

        //--------------- Business Logic ---------------
    }
}