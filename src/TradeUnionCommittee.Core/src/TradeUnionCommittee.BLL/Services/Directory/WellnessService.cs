using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.ActualResults;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.BLL.Helpers;
using TradeUnionCommittee.BLL.Interfaces.Directory;
using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Enums;

namespace TradeUnionCommittee.BLL.Services.Directory
{
    internal class WellnessService : IWellnessService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly IMapper _mapper;

        public WellnessService(TradeUnionCommitteeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActualResult<IEnumerable<WellnessDTO>>> GetAllAsync()
        {
            try
            {
                var wellness = await _context.Event.Where(x => x.Type == TypeEvent.Wellness).OrderBy(x => x.Name).ToListAsync();
                var result = _mapper.Map<IEnumerable<WellnessDTO>>(wellness);
                return new ActualResult<IEnumerable<WellnessDTO>> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<WellnessDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<WellnessDTO>> GetAsync(string hashId)
        {
            try
            {
                var id = HashHelper.DecryptLong(hashId);
                var wellness = await _context.Event.FindAsync(id);
                if (wellness == null)
                {
                    return new ActualResult<WellnessDTO>(Errors.TupleDeleted);
                }
                var result = _mapper.Map<WellnessDTO>(wellness);
                return new ActualResult<WellnessDTO> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<WellnessDTO>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<string>> CreateAsync(WellnessDTO dto)
        {
            try
            {
                var wellness = _mapper.Map<Event>(dto);
                await _context.Event.AddAsync(wellness);
                await _context.SaveChangesAsync();
                var hashId = HashHelper.EncryptLong(wellness.Id);
                return new ActualResult<string> { Result = hashId };
            }
            catch (Exception exception)
            {
                return new ActualResult<string>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> UpdateAsync(WellnessDTO dto)
        {
            try
            {
                _context.Entry(_mapper.Map<Event>(dto)).State = EntityState.Modified;
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
                var result = await _context.Event.FindAsync(id);
                if (result != null)
                {
                    _context.Event.Remove(result);
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