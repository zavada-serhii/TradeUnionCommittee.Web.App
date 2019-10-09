using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.ActualResults;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.DTO.GrandChildren;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.BLL.Helpers;
using TradeUnionCommittee.BLL.Interfaces.Lists.GrandChildren;
using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.BLL.Services.Lists.GrandChildren
{
    internal class CulturalGrandChildrenService : ICulturalGrandChildrenService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly IMapper _mapper;

        public CulturalGrandChildrenService(TradeUnionCommitteeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActualResult<IEnumerable<CulturalGrandChildrenDTO>>> GetAllAsync(string hashIdGrandChildren)
        {
            try
            {
                var id = HashId.DecryptLong(hashIdGrandChildren);
                var cultural = await _context.CulturalGrandChildrens
                    .Include(x => x.IdCulturalNavigation)
                    .Where(x => x.IdGrandChildren == id)
                    .OrderByDescending(x => x.DateVisit)
                    .ToListAsync();
                var result = _mapper.Map<IEnumerable<CulturalGrandChildrenDTO>>(cultural);
                return new ActualResult<IEnumerable<CulturalGrandChildrenDTO>> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<CulturalGrandChildrenDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<CulturalGrandChildrenDTO>> GetAsync(string hashId)
        {
            try
            {
                var id = HashId.DecryptLong(hashId);
                var cultural = await _context.CulturalGrandChildrens
                    .Include(x => x.IdCulturalNavigation)
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (cultural == null)
                {
                    return new ActualResult<CulturalGrandChildrenDTO>(Errors.TupleDeleted);
                }
                var result = _mapper.Map<CulturalGrandChildrenDTO>(cultural);
                return new ActualResult<CulturalGrandChildrenDTO> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<CulturalGrandChildrenDTO>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<string>> CreateAsync(CulturalGrandChildrenDTO item)
        {
            try
            {
                var culturalGrandChildren = _mapper.Map<CulturalGrandChildrens>(item);
                await _context.CulturalGrandChildrens.AddAsync(culturalGrandChildren);
                await _context.SaveChangesAsync();
                var hashId = HashId.EncryptLong(culturalGrandChildren.Id);
                return new ActualResult<string> { Result = hashId };
            }
            catch (Exception exception)
            {
                return new ActualResult<string>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> UpdateAsync(CulturalGrandChildrenDTO item)
        {
            try
            {
                _context.Entry(_mapper.Map<CulturalGrandChildrens>(item)).State = EntityState.Modified;
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
                var result = await _context.CulturalGrandChildrens.FindAsync(id);
                if (result != null)
                {
                    _context.CulturalGrandChildrens.Remove(result);
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