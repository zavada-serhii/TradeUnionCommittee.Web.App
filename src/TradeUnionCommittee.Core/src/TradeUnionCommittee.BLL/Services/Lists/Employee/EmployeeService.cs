using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.ActualResults;
using TradeUnionCommittee.BLL.Contracts.Lists.Employee;
using TradeUnionCommittee.BLL.DTO.Employee;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.BLL.Helpers;
using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.BLL.Services.Lists.Employee
{
    internal class EmployeeService : IEmployeeService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly IMapper _mapper;

        public EmployeeService(TradeUnionCommitteeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActualResult<string>> AddEmployeeAsync(CreateEmployeeDTO dto)
        {
            try
            {
                var employee = _mapper.Map<DAL.Entities.Employee>(dto);
                employee.PositionEmployees = _mapper.Map<PositionEmployees>(dto);

                switch (dto.TypeAccommodation)
                {
                    case AccommodationType.PrivateHouse:
                    case AccommodationType.FromUniversity:
                        employee.PrivateHouseEmployees.Add(_mapper.Map<PrivateHouseEmployees>(dto));
                        break;
                    case AccommodationType.Dormitory:
                    case AccommodationType.Departmental:
                        employee.PublicHouseEmployees.Add(_mapper.Map<PublicHouseEmployees>(dto));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                if (dto.SocialActivity)
                {
                    employee.SocialActivityEmployees = _mapper.Map<SocialActivityEmployees>(dto);
                }

                if (dto.Privileges)
                {
                    employee.PrivilegeEmployees = _mapper.Map<PrivilegeEmployees>(dto);
                }

                await _context.Employee.AddAsync(employee);
                await _context.SaveChangesAsync();
                var hashId = HashHelper.EncryptLong(employee.Id);
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
                var id = HashHelper.DecryptLong(hashId);
                var employee = await _context.Employee.FindAsync(id);
                if (employee == null)
                {
                    return new ActualResult<GeneralInfoEmployeeDTO>(Errors.TupleDeleted);
                }
                var mapping = _mapper.Map<GeneralInfoEmployeeDTO>(employee);
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
                _context.Entry(_mapper.Map<DAL.Entities.Employee>(dto)).State = EntityState.Modified;
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
                var id = HashHelper.DecryptLong(hashId);
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