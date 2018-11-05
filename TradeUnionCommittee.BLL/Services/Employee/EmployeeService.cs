using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.BLL.Interfaces.Employee;
using TradeUnionCommittee.BLL.Utilities;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.Common.Enums;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.BLL.Services.Employee
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _database;
        private readonly IAutoMapperUtilities _mapperService;
        private readonly IHashIdUtilities _hashIdUtilities;

        public EmployeeService(IUnitOfWork database, IAutoMapperUtilities mapperService, IHashIdUtilities hashIdUtilities)
        {
            _database = database;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        #region CheckHashIdInDto

        private async Task<ActualResult> CheckHashIdInDto(CreateEmployeeDTO dto)
        {
            var checkPosition = await _hashIdUtilities.CheckDecryptWithId(dto.HashIdPosition, Enums.Services.Position);
            if (checkPosition != null)
            {
                dto.IdPosition = checkPosition.Result;
            }
            else
            {
                return new ActualResult(Errors.InvalidId);
            }

            var checkSubdivision = await _hashIdUtilities.CheckDecryptWithId(dto.HashIdSubdivision, Enums.Services.Subdivision);
            if (checkSubdivision != null)
            {
                dto.IdSubdivision = checkSubdivision.Result;
            }
            else
            {
                return new ActualResult(Errors.InvalidId);
            }

            if (dto.TypeAccommodation == AccommodationType.Dormitory)
            {
                var checkDormitory = await _hashIdUtilities.CheckDecryptWithId(dto.HashIdDormitory, Enums.Services.Dormitory);
                if (checkDormitory != null)
                {
                    dto.IdDormitory = checkDormitory.Result;
                }
                else
                {
                    return new ActualResult(Errors.InvalidId);
                }
            }

            if (dto.TypeAccommodation == AccommodationType.Departmental)
            {
                var checkDepartmental = await _hashIdUtilities.CheckDecryptWithId(dto.HashIdDepartmental, Enums.Services.Departmental);
                if (checkDepartmental != null)
                {
                    dto.IdDepartmental = checkDepartmental.Result;
                }
                else
                {
                    return new ActualResult(Errors.InvalidId);
                }
            }

            if (dto.SocialActivity)
            {
                var checkSocialActivity = await _hashIdUtilities.CheckDecryptWithId(dto.HashIdSocialActivity, Enums.Services.SocialActivity);
                if (checkSocialActivity != null)
                {
                    dto.IdSocialActivity = checkSocialActivity.Result;
                }
                else
                {
                    return new ActualResult(Errors.InvalidId);
                }
            }

            if (dto.Privileges)
            {
                var checkPrivileges = await _hashIdUtilities.CheckDecryptWithId(dto.HashIdPrivileges, Enums.Services.Privileges);
                if (checkPrivileges != null)
                {
                    dto.IdPrivileges = checkPrivileges.Result;
                }
                else
                {
                    return new ActualResult(Errors.InvalidId);
                }
            }

            return new ActualResult();
        }

        #endregion

        public async Task<ActualResult> AddEmployeeAsync(CreateEmployeeDTO dto)
        {
            var checkHashIdInDto = await CheckHashIdInDto(dto);
            if (!checkHashIdInDto.IsValid)
            {
                return new ActualResult(checkHashIdInDto.ErrorsList);
            }

            var employee = _mapperService.Mapper.Map<DAL.Entities.Employee>(dto);
            await _database.EmployeeRepository.Create(employee);
            var createEmployee = await _database.SaveAsync();

            if (createEmployee.IsValid)
            {
                dto.IdEmployee = employee.Id;

                await _database.EducationRepository.Create(_mapperService.Mapper.Map<Education>(dto));
                await _database.PositionEmployeesRepository.Create(_mapperService.Mapper.Map<PositionEmployees>(dto));

                if (dto.TypeAccommodation == AccommodationType.PrivateHouse || dto.TypeAccommodation == AccommodationType.FromUniversity)
                {
                    await _database.PrivateHouseEmployeesRepository.Create(_mapperService.Mapper.Map<PrivateHouseEmployees>(dto));
                }

                if (dto.TypeAccommodation == AccommodationType.Dormitory || dto.TypeAccommodation == AccommodationType.Departmental)
                {
                    await _database.PublicHouseEmployeesRepository.Create(_mapperService.Mapper.Map<PublicHouseEmployees>(dto));
                }

                if (dto.Scientifick)
                {
                    await _database.ScientificRepository.Create(_mapperService.Mapper.Map<Scientific>(dto));
                }

                if (dto.SocialActivity)
                {
                    await _database.SocialActivityEmployeesRepository.Create(_mapperService.Mapper.Map<SocialActivityEmployees>(dto));
                }

                if (dto.Privileges)
                {
                    await _database.PrivilegeEmployeesRepository.Create(_mapperService.Mapper.Map<PrivilegeEmployees>(dto));
                }
            }

            var result = await _database.SaveAsync();
            if (result.IsValid)
            {
                return new ActualResult();
            }
            await DeleteAsync(dto.IdEmployee);
            return new ActualResult(result.ErrorsList);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<ActualResult<GeneralInfoEmployeeDTO>> GetMainInfoEmployeeAsync(string hashId)
        {
            var checkDecrypt = await _hashIdUtilities.CheckDecryptWithId(hashId, Enums.Services.Employee);

            if (checkDecrypt.IsValid)
            {
                var resultSearchByHashId = await _database
                    .EmployeeRepository
                    .GetWithInclude(x => x.Id == checkDecrypt.Result,
                                    p => p.Education,
                                    p => p.Scientific);
                var employee = new ActualResult<DAL.Entities.Employee> { Result = resultSearchByHashId.Result.FirstOrDefault() };
                return _mapperService.Mapper.Map<ActualResult<DAL.Entities.Employee>, ActualResult<GeneralInfoEmployeeDTO>>(employee);
            }
            return new ActualResult<GeneralInfoEmployeeDTO>(checkDecrypt.ErrorsList);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<ActualResult> UpdateMainInfoEmployeeAsync(GeneralInfoEmployeeDTO dto)
        {
            return await Task.Run(() =>
            {
                return new ActualResult();
            });
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<ActualResult> DeleteAsync(string hashId)
        {
            var check = await _hashIdUtilities.CheckDecryptWithId(hashId, Enums.Services.Employee);
            if (check.IsValid)
            {
                return _mapperService.Mapper.Map<ActualResult>(await DeleteAsync(check.Result));
            }
            return new ActualResult(check.ErrorsList);
        }

        private async Task<ActualResult> DeleteAsync(long id)
        {
            await _database.EmployeeRepository.Delete(id);
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<bool> CheckIdentificationСode(string identificationСode)
        {
            var result = await _database.EmployeeRepository.Find(p => p.IdentificationСode == identificationСode);
            return result.Result.Any();
        }

        public async Task<bool> CheckMechnikovCard(string mechnikovCard)
        {
            var result = await _database.EmployeeRepository.Find(p => p.MechnikovCard == mechnikovCard);
            return result.Result.Any();
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}