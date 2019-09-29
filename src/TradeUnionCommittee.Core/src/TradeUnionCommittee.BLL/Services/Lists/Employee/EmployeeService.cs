using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.ActualResults;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.DTO.Employee;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.BLL.Helpers;
using TradeUnionCommittee.BLL.Interfaces.Lists.Employee;
using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.BLL.Services.Lists.Employee
{
    internal class EmployeeService : IEmployeeService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly AutoMapperConfiguration _mapperService;
        private readonly HashIdConfiguration _hashIdUtilities;

        public EmployeeService(TradeUnionCommitteeContext context, AutoMapperConfiguration mapperService, HashIdConfiguration hashIdUtilities)
        {
            _context = context;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<string>> AddEmployeeAsync(CreateEmployeeDTO dto)
        {
            try
            {
                var employee = _mapperService.Mapper.Map<DAL.Entities.Employee>(dto);
                employee.PositionEmployees = _mapperService.Mapper.Map<PositionEmployees>(dto);

                if (dto.TypeAccommodation == AccommodationType.PrivateHouse || dto.TypeAccommodation == AccommodationType.FromUniversity)
                {
                    employee.PrivateHouseEmployees.Add(_mapperService.Mapper.Map<PrivateHouseEmployees>(dto));
                }

                if (dto.TypeAccommodation == AccommodationType.Dormitory || dto.TypeAccommodation == AccommodationType.Departmental)
                {
                    employee.PublicHouseEmployees.Add(_mapperService.Mapper.Map<PublicHouseEmployees>(dto));
                }

                if (dto.SocialActivity)
                {
                    employee.SocialActivityEmployees = _mapperService.Mapper.Map<SocialActivityEmployees>(dto);
                }

                if (dto.Privileges)
                {
                    employee.PrivilegeEmployees = _mapperService.Mapper.Map<PrivilegeEmployees>(dto);
                }

                await _context.Employee.AddAsync(employee);
                await _context.SaveChangesAsync();
                var hashId = _hashIdUtilities.EncryptLong(employee.Id);
                return new ActualResult<string> { Result = hashId };
            }
            catch (Exception exception)
            {
                return new ActualResult<string>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<ActualResult<GeneralInfoEmployeeDTO>> GetMainInfoEmployeeAsync(string hashId)
        {
            try
            {
                var id = _hashIdUtilities.DecryptLong(hashId);
                var employee = await _context.Employee.FindAsync(id);
                if (employee == null)
                {
                    return new ActualResult<GeneralInfoEmployeeDTO>(Errors.TupleDeleted);
                }
                var mapping = _mapperService.Mapper.Map<GeneralInfoEmployeeDTO>(employee);
                return new ActualResult<GeneralInfoEmployeeDTO> { Result = mapping };
            }
            catch (Exception exception)
            {
                return new ActualResult<GeneralInfoEmployeeDTO>(DescriptionExceptionHelper.GetDescriptionError(exception));
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
            catch (Exception exception)
            {
                return new ActualResult(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<ActualResult> DeleteAsync(string hashId)
        {
            try
            {
                var id = _hashIdUtilities.DecryptLong(hashId);
                var result = await _context.Employee.FindAsync(id);
                if (result != null)
                {
                    _context.Employee.Remove(result);
                    await _context.SaveChangesAsync();
                }
                return new ActualResult();
            }
            catch (Exception exception)
            {
                return new ActualResult(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<ActualResult<bool>> CheckIdentificationCode(string identificationCode)
        {
            try
            {
                return new ActualResult<bool> { Result = await _context.Employee.AnyAsync(p => p.IdentificationCode == identificationCode) };
            }
            catch (Exception exception)
            {
                return new ActualResult<bool>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<bool>> CheckMechnikovCard(string mechnikovCard)
        {
            try
            {
                return new ActualResult<bool> { Result = await _context.Employee.AnyAsync(p => p.MechnikovCard == mechnikovCard) };
            }
            catch (Exception exception)
            {
                return new ActualResult<bool>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}