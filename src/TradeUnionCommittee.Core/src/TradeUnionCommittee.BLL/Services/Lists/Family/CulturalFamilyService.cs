using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.ActualResults;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.DTO.Family;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.BLL.Helpers;
using TradeUnionCommittee.BLL.Interfaces.Lists.Family;
using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.BLL.Services.Lists.Family
{
    internal class CulturalFamilyService : ICulturalFamilyService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly IMapper _mapper;

        public CulturalFamilyService(TradeUnionCommitteeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActualResult<IEnumerable<CulturalFamilyDTO>>> GetAllAsync(string hashIdFamily)
        {
            try
            {
                var id = HashId.DecryptLong(hashIdFamily);
                var cultural = await _context.CulturalFamily
                    .Include(x => x.IdCulturalNavigation)
                    .Where(x => x.IdFamily == id)
                    .OrderByDescending(x => x.DateVisit)
                    .ToListAsync();
                var result = _mapper.Map<IEnumerable<CulturalFamilyDTO>>(cultural);
                return new ActualResult<IEnumerable<CulturalFamilyDTO>> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<CulturalFamilyDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<CulturalFamilyDTO>> GetAsync(string hashId)
        {
            try
            {
                var id = HashId.DecryptLong(hashId);
                var cultural = await _context.CulturalFamily
                    .Include(x => x.IdCulturalNavigation)
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (cultural == null)
                {
                    return new ActualResult<CulturalFamilyDTO>(Errors.TupleDeleted);
                }
                var result = _mapper.Map<CulturalFamilyDTO>(cultural);
                return new ActualResult<CulturalFamilyDTO> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<CulturalFamilyDTO>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<string>> CreateAsync(CulturalFamilyDTO item)
        {
            try
            {
                var culturalFamily = _mapper.Map<CulturalFamily>(item);
                await _context.CulturalFamily.AddAsync(culturalFamily);
                await _context.SaveChangesAsync();
                var hashId = HashId.EncryptLong(culturalFamily.Id);
                return new ActualResult<string> { Result = hashId };
            }
            catch (Exception exception)
            {
                return new ActualResult<string>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> UpdateAsync(CulturalFamilyDTO item)
        {
            try
            {
                _context.Entry(_mapper.Map<CulturalFamily>(item)).State = EntityState.Modified;
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
                var result = await _context.CulturalFamily.FindAsync(id);
                if (result != null)
                {
                    _context.CulturalFamily.Remove(result);
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