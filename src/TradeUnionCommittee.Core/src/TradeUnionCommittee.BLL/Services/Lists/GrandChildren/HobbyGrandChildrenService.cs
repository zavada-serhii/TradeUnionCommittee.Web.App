using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.ActualResults;
using TradeUnionCommittee.BLL.Contracts.Lists.GrandChildren;
using TradeUnionCommittee.BLL.DTO.GrandChildren;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.BLL.Helpers;
using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.BLL.Services.Lists.GrandChildren
{
    internal class HobbyGrandChildrenService : IHobbyGrandChildrenService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly IMapper _mapper;

        public HobbyGrandChildrenService(TradeUnionCommitteeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActualResult<IEnumerable<HobbyGrandChildrenDTO>>> GetAllAsync(string hashIdGrandChildren)
        {
            try
            {
                var id = HashHelper.DecryptLong(hashIdGrandChildren);
                var hobby = await _context.HobbyGrandChildrens
                    .Include(x => x.IdHobbyNavigation)
                    .Where(x => x.IdGrandChildren == id)
                    .OrderBy(x => x.IdHobbyNavigation.Name)
                    .ToListAsync();
                var result = _mapper.Map<IEnumerable<HobbyGrandChildrenDTO>>(hobby);
                return new ActualResult<IEnumerable<HobbyGrandChildrenDTO>> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<HobbyGrandChildrenDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<HobbyGrandChildrenDTO>> GetAsync(string hashId)
        {
            try
            {
                var id = HashHelper.DecryptLong(hashId);
                var hobby = await _context.HobbyGrandChildrens
                    .Include(x => x.IdHobbyNavigation)
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (hobby == null)
                {
                    return new ActualResult<HobbyGrandChildrenDTO>(Errors.TupleDeleted);
                }
                var result = _mapper.Map<HobbyGrandChildrenDTO>(hobby);
                return new ActualResult<HobbyGrandChildrenDTO> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<HobbyGrandChildrenDTO>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<string>> CreateAsync(HobbyGrandChildrenDTO item)
        {
            try
            {
                var hobbyGrandChildren = _mapper.Map<HobbyGrandChildrens>(item);
                await _context.HobbyGrandChildrens.AddAsync(hobbyGrandChildren);
                await _context.SaveChangesAsync();
                var hashId = HashHelper.EncryptLong(hobbyGrandChildren.Id);
                return new ActualResult<string> { Result = hashId };
            }
            catch (Exception exception)
            {
                return new ActualResult<string>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> UpdateAsync(HobbyGrandChildrenDTO item)
        {
            try
            {
                _context.Entry(_mapper.Map<HobbyGrandChildrens>(item)).State = EntityState.Modified;
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
                var result = await _context.HobbyGrandChildrens.FindAsync(id);
                if (result != null)
                {
                    _context.HobbyGrandChildrens.Remove(result);
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