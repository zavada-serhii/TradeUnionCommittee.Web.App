using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.ActualResults;
using TradeUnionCommittee.BLL.DTO.Children;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.BLL.Helpers;
using TradeUnionCommittee.BLL.Interfaces.Lists.Children;
using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.BLL.Services.Lists.Children
{
    internal class GiftChildrenService : IGiftChildrenService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly IMapper _mapper;

        public GiftChildrenService(TradeUnionCommitteeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActualResult<IEnumerable<GiftChildrenDTO>>> GetAllAsync(string hashIdChildren)
        {
            try
            {
                var id = HashHelper.DecryptLong(hashIdChildren);
                var gift = await _context.GiftChildrens
                    .Where(x => x.IdChildren == id)
                    .OrderByDescending(x => x.DateGift)
                    .ToListAsync();
                var result = _mapper.Map<IEnumerable<GiftChildrenDTO>>(gift);
                return new ActualResult<IEnumerable<GiftChildrenDTO>> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<GiftChildrenDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<GiftChildrenDTO>> GetAsync(string hashId)
        {
            try
            {
                var id = HashHelper.DecryptLong(hashId);
                var gift = await _context.GiftChildrens.FindAsync(id);
                if (gift == null)
                {
                    return new ActualResult<GiftChildrenDTO>(Errors.TupleDeleted);
                }
                var result = _mapper.Map<GiftChildrenDTO>(gift);
                return new ActualResult<GiftChildrenDTO> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<GiftChildrenDTO>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<string>> CreateAsync(GiftChildrenDTO item)
        {
            try
            {
                var giftChildren = _mapper.Map<GiftChildrens>(item);
                await _context.GiftChildrens.AddAsync(giftChildren);
                await _context.SaveChangesAsync();
                var hashId = HashHelper.EncryptLong(giftChildren.Id);
                return new ActualResult<string> { Result = hashId };
            }
            catch (Exception exception)
            {
                return new ActualResult<string>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> UpdateAsync(GiftChildrenDTO item)
        {
            try
            {
                _context.Entry(_mapper.Map<GiftChildrens>(item)).State = EntityState.Modified;
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
                var result = await _context.GiftChildrens.FindAsync(id);
                if (result != null)
                {
                    _context.GiftChildrens.Remove(result);
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