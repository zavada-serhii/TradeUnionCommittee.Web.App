using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.DTO.Employee;
using TradeUnionCommittee.BLL.Interfaces.Lists.Employee;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.Common.Enums;
using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.BLL.Services.Lists.Employee
{
    internal class SocialActivityEmployeesService : ISocialActivityEmployeesService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly AutoMapperConfiguration _mapperService;
        private readonly HashIdConfiguration _hashIdUtilities;

        public SocialActivityEmployeesService(TradeUnionCommitteeContext context, AutoMapperConfiguration mapperService, HashIdConfiguration hashIdUtilities)
        {
            _context = context;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<SocialActivityEmployeesDTO>> GetAsync(string hashIdEmployee)
        {
            try
            {
                var id = _hashIdUtilities.DecryptLong(hashIdEmployee);
                var socialActivity = await _context.SocialActivityEmployees
                    .Include(x => x.IdSocialActivityNavigation)
                    .FirstOrDefaultAsync(x => x.IdEmployee == id);
                var result = _mapperService.Mapper.Map<SocialActivityEmployeesDTO>(socialActivity);
                return new ActualResult<SocialActivityEmployeesDTO> { Result = result };
            }
            catch (Exception)
            {
                return new ActualResult<SocialActivityEmployeesDTO>(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult> CreateAsync(SocialActivityEmployeesDTO dto)
        {
            try
            {
                dto.CheckSocialActivity = true;
                await _context.SocialActivityEmployees.AddAsync(_mapperService.Mapper.Map<SocialActivityEmployees>(dto));
                await _context.SaveChangesAsync();
                return new ActualResult();
            }
            catch (Exception)
            {
                return new ActualResult(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult> UpdateAsync(SocialActivityEmployeesDTO dto)
        {
            try
            {
                _context.Entry(_mapperService.Mapper.Map<SocialActivityEmployees>(dto)).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return new ActualResult();
            }
            catch (DbUpdateConcurrencyException)
            {
                return new ActualResult(Errors.TupleDeletedOrUpdated);
            }
            catch (Exception)
            {
                return new ActualResult(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult> DeleteAsync(string hashId)
        {
            try
            {
                var id = _hashIdUtilities.DecryptLong(hashId);
                var result = await _context.SocialActivityEmployees.FindAsync(id);
                if (result != null)
                {
                    _context.SocialActivityEmployees.Remove(result);
                    await _context.SaveChangesAsync();
                }
                return new ActualResult();
            }
            catch (Exception)
            {
                return new ActualResult(Errors.DataBaseError);
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}