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
    internal class HobbyGrandChildrenService : IHobbyGrandChildrenService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly AutoMapperConfiguration _mapperService;
        private readonly HashIdConfiguration _hashIdUtilities;

        public HobbyGrandChildrenService(TradeUnionCommitteeContext context, AutoMapperConfiguration mapperService, HashIdConfiguration hashIdUtilities)
        {
            _context = context;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<IEnumerable<HobbyGrandChildrenDTO>>> GetAllAsync(string hashIdGrandChildren)
        {
            try
            {
                var id = _hashIdUtilities.DecryptLong(hashIdGrandChildren);
                var hobby = await _context.HobbyGrandChildrens
                    .Include(x => x.IdHobbyNavigation)
                    .Where(x => x.IdGrandChildren == id)
                    .OrderBy(x => x.IdHobbyNavigation.Name)
                    .ToListAsync();
                var result = _mapperService.Mapper.Map<IEnumerable<HobbyGrandChildrenDTO>>(hobby);
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
                var id = _hashIdUtilities.DecryptLong(hashId);
                var hobby = await _context.HobbyGrandChildrens
                    .Include(x => x.IdHobbyNavigation)
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (hobby == null)
                {
                    return new ActualResult<HobbyGrandChildrenDTO>(Errors.TupleDeleted);
                }
                var result = _mapperService.Mapper.Map<HobbyGrandChildrenDTO>(hobby);
                return new ActualResult<HobbyGrandChildrenDTO> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<HobbyGrandChildrenDTO>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> CreateAsync(HobbyGrandChildrenDTO item)
        {
            try
            {
                await _context.HobbyGrandChildrens.AddAsync(_mapperService.Mapper.Map<HobbyGrandChildrens>(item));
                await _context.SaveChangesAsync();
                return new ActualResult();
            }
            catch (Exception exception)
            {
                return new ActualResult(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> UpdateAsync(HobbyGrandChildrenDTO item)
        {
            try
            {
                _context.Entry(_mapperService.Mapper.Map<HobbyGrandChildrens>(item)).State = EntityState.Modified;
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