using AutoMapper;
using System;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.BL;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Interfaces.Employee;
using TradeUnionCommittee.BLL.Utilities;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.BLL.Services.Employee
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _database;
        private readonly IAutoMapperUtilities _mapperService;
        private readonly ICheckerService _checkerService;

        public EmployeeService(IUnitOfWork database, IAutoMapperUtilities mapperService, ICheckerService checkerService)
        {
            _database = database;
            _mapperService = mapperService;
            _checkerService = checkerService;
        }

        #region CheckHashIdInDto

        private async Task<ActualResult> CheckHashIdInDto(CreateEmployeeDTO dto)
        {
            var checkPosition = await _checkerService.CheckDecryptAndTupleInDbWithId(dto.HashIdPosition, Enums.Services.Position);
            if (checkPosition != null)
            {
                dto.IdPosition = checkPosition.Result;
            }
            else
            {
                return new ActualResult(Errors.InvalidId);
            }

            var checkSubdivision = await _checkerService.CheckDecryptAndTupleInDbWithId(dto.HashIdSubdivision, Enums.Services.Subdivision);
            if (checkSubdivision != null)
            {
                dto.IdSubdivision = checkSubdivision.Result;
            }
            else
            {
                return new ActualResult(Errors.InvalidId);
            }

            if (dto.TypeAccommodation == "dormitory")
            {
                var checkDormitory = await _checkerService.CheckDecryptAndTupleInDbWithId(dto.HashIdDormitory, Enums.Services.Dormitory);
                if (checkDormitory != null)
                {
                    dto.IdDormitory = checkDormitory.Result;
                }
                else
                {
                    return new ActualResult(Errors.InvalidId);
                }
            }

            if (dto.TypeAccommodation == "departmental")
            {
                var checkDepartmental = await _checkerService.CheckDecryptAndTupleInDbWithId(dto.HashIdDepartmental, Enums.Services.Departmental);
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
                var checkSocialActivity = await _checkerService.CheckDecryptAndTupleInDbWithId(dto.HashIdSocialActivity, Enums.Services.SocialActivity);
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
                var checkPrivileges = await _checkerService.CheckDecryptAndTupleInDbWithId(dto.HashIdPrivileges, Enums.Services.Privileges);
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
            _database.EmployeeRepository.Create(employee);
            var createEmployee = await _database.SaveAsync();

            if (createEmployee.IsValid)
            {
                dto.IdEmployee = employee.Id;

                _database.EducationRepository.Create(_mapperService.Mapper.Map<Education>(dto));
                _database.PositionEmployeesRepository.Create(_mapperService.Mapper.Map<PositionEmployees>(dto));

                if (dto.TypeAccommodation == "privateHouse" || dto.TypeAccommodation == "fromUniversity")
                {
                    _database.PrivateHouseEmployeesRepository.Create(_mapperService.Mapper.Map<PrivateHouseEmployees>(dto));
                }

                if (dto.TypeAccommodation == "dormitory" || dto.TypeAccommodation == "departmental")
                {
                    _database.PublicHouseEmployeesRepository.Create(_mapperService.Mapper.Map<PublicHouseEmployees>(dto));
                }

                if (dto.Scientifick)
                {
                    _database.ScientificRepository.Create(_mapperService.Mapper.Map<Scientific>(dto));
                }

                if (dto.SocialActivity)
                {
                    _database.SocialActivityEmployeesRepository.Create(_mapperService.Mapper.Map<SocialActivityEmployees>(dto));
                }

                if (dto.Privileges)
                {
                    _database.PrivilegeEmployeesRepository.Create(_mapperService.Mapper.Map<PrivilegeEmployees>(dto));
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

        public async Task<ActualResult<GeneralInfoEmployeeDTO>> GetMainInfoEmployeeAsync(long id)
        {
            return await Task.Run(() =>
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<DAL.Entities.Employee, GeneralInfoEmployeeDTO>()
                .ForMember("IdEmployee", opt => opt.MapFrom(c => c.Id))
                .ForMember("CountYear", opt => opt.MapFrom(c => CalculateAge(c.BirthDate)))
                .ForMember("Sex", opt => opt.MapFrom(c => ConvertToUkraine(c.Sex)))
                ).CreateMapper();
                var employee =  mapper.Map<ActualResult<DAL.Entities.Employee>, ActualResult<GeneralInfoEmployeeDTO>>(_database.EmployeeRepository.Get(id));

                var education = _database.EducationRepository.Get(id).Result;
                var scientifick = _database.ScientificRepository.Get(id).Result;

                employee.Result.LevelEducation = education.LevelEducation;
                employee.Result.NameInstitution = education.NameInstitution;
                employee.Result.YearReceiving = education.YearReceiving;

                if (scientifick != null)
                {
                    employee.Result.ScientifickDegree = scientifick.ScientificDegree;
                    employee.Result.ScientifickTitle = scientifick.ScientificTitle;
                }
                return employee;
            });
        }

        private string ConvertToUkraine(string sex)
        {
            switch (sex)
            {
                case "Male":
                    return new string("Чоловіча");
                case "Female":
                    return new string("Жіноча");
                default:
                    return sex;
            }
        }

        private int CalculateAge(DateTime birthDate)
        {
            var yearsPassed = DateTime.Now.Year - birthDate.Year;
            if (DateTime.Now.Month < birthDate.Month || (DateTime.Now.Month == birthDate.Month && DateTime.Now.Day < birthDate.Day))
            {
                yearsPassed--;
            }
            return yearsPassed;
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
            var check = await _checkerService.CheckDecryptAndTupleInDbWithId(hashId, Enums.Services.Employee, false);
            if (check.IsValid)
            {
                return _mapperService.Mapper.Map<ActualResult>(await DeleteAsync(check.Result));
            }
            return new ActualResult(check.ErrorsList);
        }

        private async Task<ActualResult> DeleteAsync(long id)
        {
            _database.EmployeeRepository.Delete(id);
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<bool> CheckIdentificationСode(string identificationСode) =>
            await Task.Run(() => _database.EmployeeRepository.Find(p => p.IdentificationСode == identificationСode).Result.Any());

        public async Task<bool> CheckMechnikovCard(string mechnikovCard) =>
            await Task.Run(() => _database.EmployeeRepository.Find(p => p.MechnikovCard == mechnikovCard).Result.Any());

        //------------------------------------------------------------------------------------------------------------------------------------------

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}