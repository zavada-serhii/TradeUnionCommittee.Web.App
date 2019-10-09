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

namespace TradeUnionCommittee.BLL.Services.Lists.Family
{
    internal class FamilyService : IFamilyService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly IMapper _mapper;

        public FamilyService(TradeUnionCommitteeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActualResult<IEnumerable<FamilyDTO>>> GetAllAsync(string hashIdEmployee)
        {
            try
            {
                var id = HashId.DecryptLong(hashIdEmployee);
                var family = await _context.Family
                    .Where(x => x.IdEmployee == id)
                    .ToListAsync();
                var result = _mapper.Map<IEnumerable<FamilyDTO>>(family);
                return new ActualResult<IEnumerable<FamilyDTO>> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<FamilyDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<FamilyDTO>> GetAsync(string hashId)
        {
            try
            {
                var id = HashId.DecryptLong(hashId);
                var family = await _context.Family.FindAsync(id);
                if (family == null)
                {
                    return new ActualResult<FamilyDTO>(Errors.TupleDeleted);
                }
                var result = _mapper.Map<FamilyDTO>(family);
                return new ActualResult<FamilyDTO> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<FamilyDTO>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<string>> CreateAsync(FamilyDTO item)
        {
            try
            {
                var family = _mapper.Map<DAL.Entities.Family>(item);
                await _context.Family.AddAsync(family);
                await _context.SaveChangesAsync();
                var hashId = HashId.EncryptLong(family.Id);
                return new ActualResult<string> { Result = hashId };
            }
            catch (Exception exception)
            {
                return new ActualResult<string>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> UpdateAsync(FamilyDTO item)
        {
            try
            {
                _context.Entry(_mapper.Map<DAL.Entities.Family>(item)).State = EntityState.Modified;
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
                var result = await _context.Family.FindAsync(id);
                if (result != null)
                {
                    _context.Family.Remove(result);
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