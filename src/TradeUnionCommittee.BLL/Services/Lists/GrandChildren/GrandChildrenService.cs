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

namespace TradeUnionCommittee.BLL.Services.Lists.GrandChildren
{
    internal class GrandChildrenService : IGrandChildrenService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly AutoMapperConfiguration _mapperService;
        private readonly HashIdConfiguration _hashIdUtilities;

        public GrandChildrenService(TradeUnionCommitteeContext context, AutoMapperConfiguration mapperService, HashIdConfiguration hashIdUtilities)
        {
            _context = context;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<IEnumerable<GrandChildrenDTO>>> GetAllAsync(string hashIdEmployee)
        {
            try
            {
                var id = _hashIdUtilities.DecryptLong(hashIdEmployee);
                var grandChildren = await _context.GrandChildren
                    .Where(x => x.IdEmployee == id)
                    .OrderByDescending(x => x.BirthDate)
                    .ToListAsync();
                var result = _mapperService.Mapper.Map<IEnumerable<GrandChildrenDTO>>(grandChildren);
                return new ActualResult<IEnumerable<GrandChildrenDTO>> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<GrandChildrenDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<GrandChildrenDTO>> GetAsync(string hashId)
        {
            try
            {
                var id = _hashIdUtilities.DecryptLong(hashId);
                var grandChildren = await _context.GrandChildren.FindAsync(id);
                if (grandChildren == null)
                {
                    return new ActualResult<GrandChildrenDTO>(Errors.TupleDeleted);
                }
                var result = _mapperService.Mapper.Map<GrandChildrenDTO>(grandChildren);
                return new ActualResult<GrandChildrenDTO> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<GrandChildrenDTO>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<string>> CreateAsync(GrandChildrenDTO item)
        {
            try
            {
                var grandChildren = _mapperService.Mapper.Map<DAL.Entities.GrandChildren>(item);
                await _context.GrandChildren.AddAsync(grandChildren);
                await _context.SaveChangesAsync();
                var hashId = _hashIdUtilities.EncryptLong(grandChildren.Id);
                return new ActualResult<string> { Result = hashId };
            }
            catch (Exception exception)
            {
                return new ActualResult<string>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> UpdateAsync(GrandChildrenDTO item)
        {
            try
            {
                _context.Entry(_mapperService.Mapper.Map<DAL.Entities.GrandChildren>(item)).State = EntityState.Modified;
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
                var result = await _context.GrandChildren.FindAsync(id);
                if (result != null)
                {
                    _context.GrandChildren.Remove(result);
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