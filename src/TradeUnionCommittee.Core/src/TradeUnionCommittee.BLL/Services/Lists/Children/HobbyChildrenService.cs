using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.ActualResults;
using TradeUnionCommittee.BLL.Contracts.Lists.Children;
using TradeUnionCommittee.BLL.DTO.Children;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.BLL.Helpers;
using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.BLL.Services.Lists.Children
{
    internal class HobbyChildrenService : IHobbyChildrenService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly IMapper _mapper;

        public HobbyChildrenService(TradeUnionCommitteeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActualResult<IEnumerable<HobbyChildrenDTO>>> GetAllAsync(string hashIdChildren)
        {
            try
            {
                var id = HashHelper.DecryptLong(hashIdChildren);
                var hobby = await _context.HobbyChildrens
                    .Include(x => x.IdHobbyNavigation)
                    .Where(x => x.IdChildren == id)
                    .OrderBy(x => x.IdHobbyNavigation.Name)
                    .ToListAsync();
                var result = _mapper.Map<IEnumerable<HobbyChildrenDTO>>(hobby);
                return new ActualResult<IEnumerable<HobbyChildrenDTO>> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<HobbyChildrenDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<HobbyChildrenDTO>> GetAsync(string hashId)
        {
            try
            {
                var id = HashHelper.DecryptLong(hashId);
                var hobby = await _context.HobbyChildrens
                    .Include(x => x.IdHobbyNavigation)
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (hobby == null)
                {
                    return new ActualResult<HobbyChildrenDTO>(Errors.TupleDeleted);
                }
                var result = _mapper.Map<HobbyChildrenDTO>(hobby);
                return new ActualResult<HobbyChildrenDTO> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<HobbyChildrenDTO>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<string>> CreateAsync(HobbyChildrenDTO item)
        {
            try
            {
                var hobbyChildren = _mapper.Map<HobbyChildrens>(item);
                await _context.HobbyChildrens.AddAsync(hobbyChildren);
                await _context.SaveChangesAsync();
                var hashId = HashHelper.EncryptLong(hobbyChildren.Id);
                return new ActualResult<string> { Result = hashId };
            }
            catch (Exception exception)
            {
                return new ActualResult<string>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> UpdateAsync(HobbyChildrenDTO item)
        {
            try
            {
                _context.Entry(_mapper.Map<HobbyChildrens>(item)).State = EntityState.Modified;
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
                var result = await _context.HobbyChildrens.FindAsync(id);
                if (result != null)
                {
                    _context.HobbyChildrens.Remove(result);
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