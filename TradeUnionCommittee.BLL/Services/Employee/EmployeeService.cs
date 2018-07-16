using System.Linq;
using System.Threading.Tasks;
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

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}