using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.ActualResults;
using TradeUnionCommittee.BLL.Contracts.Lists.Employee;
using TradeUnionCommittee.BLL.DTO.Employee;
using TradeUnionCommittee.BLL.Helpers;
using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.BLL.Services.Lists.Employee
{
    internal class SocialActivityEmployeesService : ISocialActivityEmployeesService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly IMapper _mapper;

        public SocialActivityEmployeesService(TradeUnionCommitteeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActualResult<SocialActivityEmployeesDTO>> GetAsync(string hashIdEmployee)
        {
            try
            {
                var id = HashHelper.DecryptLong(hashIdEmployee);
                var socialActivity = await _context.SocialActivityEmployees
                    .Include(x => x.IdSocialActivityNavigation)
                    .FirstOrDefaultAsync(x => x.IdEmployee == id);
                if (socialActivity == null)
                {
                    return new ActualResult<SocialActivityEmployeesDTO>();
                }
                var result = _mapper.Map<SocialActivityEmployeesDTO>(socialActivity);
                return new ActualResult<SocialActivityEmployeesDTO> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<SocialActivityEmployeesDTO>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<string>> CreateAsync(SocialActivityEmployeesDTO dto)
        {
            try
            {
                dto.CheckSocialActivity = true;
                var socialActivityEmployees = _mapper.Map<SocialActivityEmployees>(dto);
                await _context.SocialActivityEmployees.AddAsync(socialActivityEmployees);
                await _context.SaveChangesAsync();
                var hashId = HashHelper.EncryptLong(socialActivityEmployees.Id);
                return new ActualResult<string> { Result = hashId };
            }
            catch (Exception exception)
            {
                return new ActualResult<string>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> UpdateAsync(SocialActivityEmployeesDTO dto)
        {
            try
            {
                _context.Entry(_mapper.Map<SocialActivityEmployees>(dto)).State = EntityState.Modified;
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
                var result = await _context.SocialActivityEmployees.FindAsync(id);
                if (result != null)
                {
                    _context.SocialActivityEmployees.Remove(result);
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