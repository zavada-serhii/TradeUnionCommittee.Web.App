using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.DTO.Employee;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.BLL.Interfaces.Lists.Employee;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.Common.Enums;
using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.BLL.Services.Lists.Employee
{
    public class EmployeeService : IEmployeeService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly IAutoMapperConfiguration _mapperService;
        private readonly IHashIdConfiguration _hashIdUtilities;

        public EmployeeService(TradeUnionCommitteeContext context, IAutoMapperConfiguration mapperService, IHashIdConfiguration hashIdUtilities)
        {
            _context = context;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult> AddEmployeeAsync(CreateEmployeeDTO dto)
        {
            try
            {
                var employee = _mapperService.Mapper.Map<DAL.Entities.Employee>(dto);
                await _context.Employee.AddAsync(employee);

                if (await _context.SaveChangesAsync() > 0)
                {
                    dto.IdEmployee = employee.Id;

                    await _context.PositionEmployees.AddAsync(_mapperService.Mapper.Map<PositionEmployees>(dto));

                    if (dto.TypeAccommodation == AccommodationType.PrivateHouse || dto.TypeAccommodation == AccommodationType.FromUniversity)
                    {
                        await _context.PrivateHouseEmployees.AddAsync(_mapperService.Mapper.Map<PrivateHouseEmployees>(dto));
                    }

                    if (dto.TypeAccommodation == AccommodationType.Dormitory || dto.TypeAccommodation == AccommodationType.Departmental)
                    {
                        await _context.PublicHouseEmployees.AddAsync(_mapperService.Mapper.Map<PublicHouseEmployees>(dto));
                    }

                    if (dto.SocialActivity)
                    {
                        await _context.SocialActivityEmployees.AddAsync(_mapperService.Mapper.Map<SocialActivityEmployees>(dto));
                    }

                    if (dto.Privileges)
                    {
                        await _context.PrivilegeEmployees.AddAsync(_mapperService.Mapper.Map<PrivilegeEmployees>(dto));
                    }
                }

                if (await _context.SaveChangesAsync() > 0)
                {
                    return new ActualResult();
                }
                await DeleteAsync(dto.IdEmployee);
                return new ActualResult(Errors.DataBaseError);
            }
            catch (Exception )
            {
                return new ActualResult(Errors.DataBaseError);
            }
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<ActualResult<GeneralInfoEmployeeDTO>> GetMainInfoEmployeeAsync(string hashId)
        {
            try
            {
                var id = _hashIdUtilities.DecryptLong(hashId, Enums.Services.Employee);
                var result = await _context.Employee.FindAsync(id);
                var mapping = _mapperService.Mapper.Map<GeneralInfoEmployeeDTO>(result);
                return new ActualResult<GeneralInfoEmployeeDTO> { Result = mapping };
            }
            catch (Exception)
            {
                return new ActualResult<GeneralInfoEmployeeDTO>(Errors.DataBaseError);
            }
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<ActualResult> UpdateMainInfoEmployeeAsync(GeneralInfoEmployeeDTO dto)
        {
            try
            {
                _context.Entry(_mapperService.Mapper.Map<DAL.Entities.Employee>(dto)).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return new ActualResult();
            }
            catch (Exception)
            {
                return new ActualResult(Errors.DataBaseError);
            }
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<ActualResult> DeleteAsync(string hashId)
        {
            return _mapperService.Mapper.Map<ActualResult>(await DeleteAsync(_hashIdUtilities.DecryptLong(hashId, Enums.Services.Employee)));
        }

        private async Task<ActualResult> DeleteAsync(long id)
        {
            try
            {
                var result = await _context.Employee.FindAsync(id);
                if (result != null)
                {
                    _context.Employee.Remove(result);
                    await _context.SaveChangesAsync();
                }
                return new ActualResult();
            }
            catch (Exception)
            {
               return new ActualResult(Errors.DataBaseError);
            }
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<bool> CheckIdentificationСode(string identificationСode)
        {
            return await _context.Employee.AnyAsync(p => p.IdentificationСode == identificationСode);
        }

        public async Task<bool> CheckMechnikovCard(string mechnikovCard)
        {
            return await _context.Employee.AnyAsync(p => p.MechnikovCard == mechnikovCard);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}