using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.DTO.GrandChildren;
using TradeUnionCommittee.BLL.Helpers;
using TradeUnionCommittee.BLL.Interfaces.Lists.GrandChildren;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.Common.Enums;
using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.BLL.Services.Lists.GrandChildren
{
    internal class GiftGrandChildrenService : IGiftGrandChildrenService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly AutoMapperConfiguration _mapperService;
        private readonly HashIdConfiguration _hashIdUtilities;

        public GiftGrandChildrenService(TradeUnionCommitteeContext context, AutoMapperConfiguration mapperService, HashIdConfiguration hashIdUtilities)
        {
            _context = context;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<IEnumerable<GiftGrandChildrenDTO>>> GetAllAsync(string hashIdGrandChildren)
        {
            try
            {
                var id = _hashIdUtilities.DecryptLong(hashIdGrandChildren);
                var gift = await _context.GiftGrandChildrens
                    .Where(x => x.IdGrandChildren == id)
                    .OrderByDescending(x => x.DateGift)
                    .ToListAsync();
                var result = _mapperService.Mapper.Map<IEnumerable<GiftGrandChildrenDTO>>(gift);
                return new ActualResult<IEnumerable<GiftGrandChildrenDTO>> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<GiftGrandChildrenDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<GiftGrandChildrenDTO>> GetAsync(string hashId)
        {
            try
            {
                var id = _hashIdUtilities.DecryptLong(hashId);
                var gift = await _context.GiftGrandChildrens.FindAsync(id);
                if (gift == null)
                {
                    return new ActualResult<GiftGrandChildrenDTO>(Errors.TupleDeleted);
                }
                var result = _mapperService.Mapper.Map<GiftGrandChildrenDTO>(gift);
                return new ActualResult<GiftGrandChildrenDTO> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<GiftGrandChildrenDTO>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> CreateAsync(GiftGrandChildrenDTO item)
        {
            try
            {
                await _context.GiftGrandChildrens.AddAsync(_mapperService.Mapper.Map<GiftGrandChildrens>(item));
                await _context.SaveChangesAsync();
                return new ActualResult();
            }
            catch (Exception exception)
            {
                return new ActualResult(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> UpdateAsync(GiftGrandChildrenDTO item)
        {
            try
            {
                _context.Entry(_mapperService.Mapper.Map<GiftGrandChildrens>(item)).State = EntityState.Modified;
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
                var result = await _context.GiftGrandChildrens.FindAsync(id);
                if (result != null)
                {
                    _context.GiftGrandChildrens.Remove(result);
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