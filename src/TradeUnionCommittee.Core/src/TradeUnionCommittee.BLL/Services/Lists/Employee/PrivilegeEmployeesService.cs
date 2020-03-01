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
    internal class PrivilegeEmployeesService : IPrivilegeEmployeesService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly IMapper _mapper;

        public PrivilegeEmployeesService(TradeUnionCommitteeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActualResult<PrivilegeEmployeesDTO>> GetAsync(string hashIdEmployee)
        {
            try
            {
                var id = HashHelper.DecryptLong(hashIdEmployee);
                var privilege = await _context.PrivilegeEmployees
                    .Include(x => x.IdPrivilegesNavigation)
                    .FirstOrDefaultAsync(x => x.IdEmployee == id);
                if (privilege == null)
                {
                    return new ActualResult<PrivilegeEmployeesDTO>();
                }
                var result = _mapper.Map<PrivilegeEmployeesDTO>(privilege);
                return new ActualResult<PrivilegeEmployeesDTO> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<PrivilegeEmployeesDTO>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<string>> CreateAsync(PrivilegeEmployeesDTO dto)
        {
            try
            {
                dto.CheckPrivileges = true;
                var privilegeEmployees = _mapper.Map<PrivilegeEmployees>(dto);
                await _context.PrivilegeEmployees.AddAsync(privilegeEmployees);
                await _context.SaveChangesAsync();
                var hashId = HashHelper.EncryptLong(privilegeEmployees.Id);
                return new ActualResult<string> { Result = hashId };
            }
            catch (Exception exception)
            {
                return new ActualResult<string>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> UpdateAsync(PrivilegeEmployeesDTO dto)
        {
            try
            {
                _context.Entry(_mapper.Map<PrivilegeEmployees>(dto)).State = EntityState.Modified;
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