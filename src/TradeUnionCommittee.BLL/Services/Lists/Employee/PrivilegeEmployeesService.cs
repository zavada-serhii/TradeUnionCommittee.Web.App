using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.ActualResults;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.DTO.Employee;
using TradeUnionCommittee.BLL.Helpers;
using TradeUnionCommittee.BLL.Interfaces.Lists.Employee;
using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.BLL.Services.Lists.Employee
{
    internal class PrivilegeEmployeesService : IPrivilegeEmployeesService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly AutoMapperConfiguration _mapperService;
        private readonly HashIdConfiguration _hashIdUtilities;

        public PrivilegeEmployeesService(TradeUnionCommitteeContext context, AutoMapperConfiguration mapperService, HashIdConfiguration hashIdUtilities)
        {
            _context = context;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<PrivilegeEmployeesDTO>> GetAsync(string hashIdEmployee)
        {
            try
            {
                var id = _hashIdUtilities.DecryptLong(hashIdEmployee);
                var privilege = await _context.PrivilegeEmployees
                    .Include(x => x.IdPrivilegesNavigation)
                    .FirstOrDefaultAsync(x => x.IdEmployee == id);
                if (privilege == null)
                {
                    return new ActualResult<PrivilegeEmployeesDTO>();
                }
                var result = _mapperService.Mapper.Map<PrivilegeEmployeesDTO>(privilege);
                return new ActualResult<PrivilegeEmployeesDTO> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<PrivilegeEmployeesDTO>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> CreateAsync(PrivilegeEmployeesDTO dto)
        {
            try
            {
                dto.CheckPrivileges = true;
                await _context.PrivilegeEmployees.AddAsync(_mapperService.Mapper.Map<PrivilegeEmployees>(dto));
                await _context.SaveChangesAsync();
                return new ActualResult();
            }
            catch (Exception exception)
            {
                return new ActualResult(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> UpdateAsync(PrivilegeEmployeesDTO dto)
        {
            try
            {
                _context.Entry(_mapperService.Mapper.Map<PrivilegeEmployees>(dto)).State = EntityState.Modified;
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
                var result = await _context.PrivilegeEmployees.FindAsync(id);
                if (result != null)
                {
                    _context.PrivilegeEmployees.Remove(result);
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