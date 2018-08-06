using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Interfaces.Employee;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.BLL.Services.Employee
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _database;

        public EmployeeService(IUnitOfWork database)
        {
            _database = database;
        }

        public async Task<ActualResult> AddEmployeeAsync(AddEmployeeDTO dto)
        { 
            var employee = new DAL.Entities.Employee
            {
                FirstName = dto.FirstName,
                SecondName = dto.SecondName,
                Patronymic = dto.Patronymic,
                Sex = dto.Sex,
                BasicProfession = dto.BasicProfission,
                BirthDate = dto.BirthDate,
                StartYearWork = dto.StartYearWork,
                StartDateTradeUnion = dto.StartDateTradeUnion,
                IdentificationСode = dto.IdentificationСode,
                MechnikovCard = dto.MechnikovCard,
                MobilePhone = dto.MobilePhone,
                CityPhone = dto.CityPhone,
                Note = dto.Note
            };
            _database.EmployeeRepository.Create(employee);
            var countChanges = await _database.SaveAsync();

            if (countChanges.IsValid)
            {
                dto.IdEmployee = employee.Id;
                AddEducation(dto);
                AddPositionEmployees(dto);
                AddAccommodation(dto);

                if (dto.Scientifick)
                {
                    AddScientifick(dto);
                }

                if (dto.SocialActivity)
                {
                    AddSocialActivity(dto);
                }

                if (dto.Privileges)
                {
                    AddPrivileges(dto);
                }
            }
            var result = await _database.SaveAsync();
            return new ActualResult {IsValid = result.IsValid};
        }

        private void AddEducation(AddEmployeeDTO dto)
        {
            _database.EducationRepository.Create(new Education
            {
                IdEmployee = dto.IdEmployee,
                LevelEducation = dto.LevelEducation,
                NameInstitution = dto.NameInstitution,
                DateReceiving = dto.YearReceiving
            });
        }

        private void AddPositionEmployees(AddEmployeeDTO dto)
        {
            _database.PositionEmployeesRepository.Create(new PositionEmployees
            {
                IdEmployee = dto.IdEmployee,
                IdPosition = dto.Position,
                StartDate = dto.StartDatePosition,
                IdSubdivision = dto.IdSubdivision
            });
        }

        private void AddAccommodation(AddEmployeeDTO dto)
        {
            switch (dto.TypeAccommodation)
            {
                case "privateHouse":
                    _database.PrivateHouseEmployeesRepository.Create(new PrivateHouseEmployees
                    {
                        IdEmployee = dto.IdEmployee,
                        City = dto.CityPrivateHouse,
                        Street = dto.StreetPrivateHouse,
                        NumberHouse = dto.NumberHousePrivateHouse,
                        NumberApartment = dto.NumberApartmentPrivateHouse
                    });
                    break;
                case "fromUniversity":
                    _database.PrivateHouseEmployeesRepository.Create(new PrivateHouseEmployees
                    {
                        IdEmployee = dto.IdEmployee,
                        City = dto.CityHouseUniversity,
                        Street = dto.StreetHouseUniversity,
                        NumberHouse = dto.NumberHouseUniversity,
                        NumberApartment = dto.NumberApartmentHouseUniversity,
                        DateReceiving = dto.DateReceivingHouseFromUniversity
                    });
                    break;
                case "dormitory":
                    _database.PublicHouseEmployeesRepository.Create(new PublicHouseEmployees
                    {
                        IdEmployee = dto.IdEmployee,
                        IdAddressPublicHouse = dto.IdDormitory,
                        NumberRoom = dto.NumberRoomDormitory
                    });
                    break;
                case "departmental":
                    _database.PublicHouseEmployeesRepository.Create(new PublicHouseEmployees
                    {
                        IdEmployee = dto.IdEmployee,
                        IdAddressPublicHouse = dto.IdDepartmental,
                        NumberRoom = dto.NumberRoomDepartmental
                    });
                    break;
            }
        }

        private void AddScientifick(AddEmployeeDTO dto)
        {
            _database.ScientificRepository.Create(new Scientific
            {
                IdEmployee = dto.IdEmployee,
                ScientificDegree = dto.ScientifickDegree,
                ScientificTitle = dto.ScientifickTitle
            });
        }

        private void AddSocialActivity(AddEmployeeDTO dto)
        {
            _database.SocialActivityEmployeesRepository.Create(new SocialActivityEmployees
            {
                IdEmployee = dto.IdEmployee,
                IdSocialActivity = dto.IdSocialActivity,
                Note = dto.NoteSocialActivity,
                CheckSocialActivity = true
            });
        }

        private void AddPrivileges(AddEmployeeDTO dto)
        {
            _database.PrivilegeEmployeesRepository.Create(new PrivilegeEmployees
            {
                IdEmployee = dto.IdEmployee,
                IdPrivileges = dto.IdPrivileges,
                Note = dto.NotePrivileges,
                CheckPrivileges = true
            });
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<ActualResult<MainInfoEmployeeDTO>> GetMainInfoEmployeeAsync(long id)
        {
            return await Task.Run(() =>
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<DAL.Entities.Employee, MainInfoEmployeeDTO>()
                .ForMember("IdEmployee", opt => opt.MapFrom(c => c.Id))
                .ForMember("CountYear", opt => opt.MapFrom(c => CalculateAge(c.BirthDate)))
                .ForMember("Sex", opt => opt.MapFrom(c => ConvertToUkraine(c.Sex)))
                ).CreateMapper();
                var employee =  mapper.Map<ActualResult<DAL.Entities.Employee>, ActualResult<MainInfoEmployeeDTO>>(_database.EmployeeRepository.Get(id));

                var education = _database.EducationRepository.Get(id).Result;
                var scientifick = _database.ScientificRepository.Get(id).Result;

                employee.Result.LevelEducation = education.LevelEducation;
                employee.Result.NameInstitution = education.NameInstitution;
                employee.Result.YearReceiving = education.DateReceiving;

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
            }
            return sex;
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

        public Task<ActualResult> UpdateMainInfoEmployeeAsync(MainInfoEmployeeDTO dto)
        {
            throw new System.NotImplementedException();
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        public Task<ActualResult> DeleteAsync(long id)
        {
            throw new System.NotImplementedException();
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<ActualResult> CheckIdentificationСode(string identificationСode)
        {
            return await Task.Run(() =>
            {
                var code = _database.EmployeeRepository.Find(i => i.IdentificationСode == identificationСode);
                return code.Result.Any() ?
                    new ActualResult { IsValid = false } :
                    new ActualResult { IsValid = true };
            });
        }

        public async Task<ActualResult> CheckMechnikovCard(string mechnikovCard)
        {
            return await Task.Run(() =>
            {
                var card = _database.EmployeeRepository.Find(p => p.MechnikovCard == mechnikovCard);
                return card.Result.Any() ?
                    new ActualResult { IsValid = false } :
                    new ActualResult { IsValid = true };
            });
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}