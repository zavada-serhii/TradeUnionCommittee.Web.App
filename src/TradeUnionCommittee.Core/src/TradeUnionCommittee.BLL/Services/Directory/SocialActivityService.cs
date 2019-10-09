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

namespace TradeUnionCommittee.BLL.Services.Directory
{
    internal class SocialActivityService : ISocialActivityService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly IMapper _mapper;

        public SocialActivityService(TradeUnionCommitteeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            
        }

        public async Task<ActualResult<IEnumerable<DirectoryDTO>>> GetAllAsync()
        {
            try
            {
                var socialActivity = await _context.SocialActivity.OrderBy(x => x.Name).ToListAsync();
                var result = _mapper.Map<IEnumerable<DirectoryDTO>>(socialActivity);
                return new ActualResult<IEnumerable<DirectoryDTO>> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<DirectoryDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<DirectoryDTO>> GetAsync(string hashId)
        {
            try
            {
                var id = HashHelper.DecryptLong(hashId);
                var socialActivity = await _context.SocialActivity.FindAsync(id);
                if (socialActivity == null)
                {
                    return new ActualResult<DirectoryDTO>(Errors.TupleDeleted);
                }
                var result = _mapper.Map<DirectoryDTO>(socialActivity);
                return new ActualResult<DirectoryDTO> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<DirectoryDTO>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<string>> CreateAsync(DirectoryDTO dto)
        {
            try
            {
                var socialActivity = _mapper.Map<SocialActivity>(dto);
                await _context.SocialActivity.AddAsync(socialActivity);
                await _context.SaveChangesAsync();
                var hashId = HashHelper.EncryptLong(socialActivity.Id);
                return new ActualResult<string> { Result = hashId };
            }
            catch (Exception exception)
            {
                return new ActualResult<string>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> UpdateAsync(DirectoryDTO dto)
        {
            try
            {
                _context.Entry(_mapper.Map<SocialActivity>(dto)).State = EntityState.Modified;
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
                var result = await _context.SocialActivity.FindAsync(id);
                if (result != null)
                {
                    _context.SocialActivity.Remove(result);
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