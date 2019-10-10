using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.ActualResults;
using TradeUnionCommittee.BLL.DTO.GrandChildren;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.BLL.Helpers;
using TradeUnionCommittee.BLL.Interfaces.Lists.GrandChildren;
using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.BLL.Services.Lists.GrandChildren
{
    internal class GiftGrandChildrenService : IGiftGrandChildrenService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly IMapper _mapper;

        public GiftGrandChildrenService(TradeUnionCommitteeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActualResult<IEnumerable<GiftGrandChildrenDTO>>> GetAllAsync(string hashIdGrandChildren)
        {
            try
            {
                var id = HashHelper.DecryptLong(hashIdGrandChildren);
                var gift = await _context.GiftGrandChildrens
                    .Where(x => x.IdGrandChildren == id)
                    .OrderByDescending(x => x.DateGift)
                    .ToListAsync();
                var result = _mapper.Map<IEnumerable<GiftGrandChildrenDTO>>(gift);
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
                var id = HashHelper.DecryptLong(hashId);
                var gift = await _context.GiftGrandChildrens.FindAsync(id);
                if (gift == null)
                {
                    return new ActualResult<GiftGrandChildrenDTO>(Errors.TupleDeleted);
                }
                var result = _mapper.Map<GiftGrandChildrenDTO>(gift);
                return new ActualResult<GiftGrandChildrenDTO> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<GiftGrandChildrenDTO>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<string>> CreateAsync(GiftGrandChildrenDTO item)
        {
            try
            {
                var giftGrandChildren = _mapper.Map<GiftGrandChildrens>(item);
                await _context.GiftGrandChildrens.AddAsync(giftGrandChildren);
                await _context.SaveChangesAsync();
                var hashId = HashHelper.EncryptLong(giftGrandChildren.Id);
                return new ActualResult<string> { Result = hashId };
            }
            catch (Exception exception)
            {
                return new ActualResult<string>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> UpdateAsync(GiftGrandChildrenDTO item)
        {
            try
            {
                _context.Entry(_mapper.Map<GiftGrandChildrens>(item)).State = EntityState.Modified;
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
                var id = HashHelper.DecryptLong(hashId);
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