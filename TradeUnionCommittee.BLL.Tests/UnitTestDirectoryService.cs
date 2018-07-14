using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Services.Directory;
using TradeUnionCommittee.BLL.Services.Employee;
using TradeUnionCommittee.BLL.Services.Search;
using TradeUnionCommittee.DAL.Repositories;

namespace TradeUnionCommittee.BLL.Tests
{
    [TestClass]
    public class UnitTestDirectoryService
    {
        private readonly PositionService _service = new PositionService(new UnitOfWork("Host=127.0.0.1;Port=5432;Database=TradeUnionCommitteeEmployeesCore;Username=AdminTradeUnionCommitteeEmployees;Password=admin"));

        [TestMethod]
        public void TestGetPositionService()
        {
            var result = _service.GetAsync(1);
            Assert.AreEqual(result.Result.IsValid, true);
        }

        [TestMethod]
        public void TestGetAllPositionService()
        {
            var result = _service.GetAllAsync();
            Assert.AreEqual(result.Result.IsValid, true);
        }

        [TestMethod]
        public void TestCreatePositionService()
        {
            var result = _service.CreateAsync(new DirectoryDTO {Name = "Test"});
            Assert.AreEqual(result.Result.IsValid, true);
        }

        [TestMethod]
        public void TestEditPositionService()
        {
            var result = _service.UpdateAsync(new DirectoryDTO {Id = 8, Name = "Test1" });
            Assert.AreEqual(result.Result.IsValid, true);
        }

        [TestMethod]
        public void TestRemovePositionService()
        {
            var result = _service.DeleteAsync(8);
            Assert.AreEqual(result.Result.IsValid, true);
        }


        private readonly SearchService _searchService = new SearchService(new UnitOfWork("Host=127.0.0.1;Port=5432;Database=TradeUnionCommitteeEmployeesCore;Username=postgres;Password=postgres"));


        [TestMethod]
        public async Task TestSearchService()
        {
            var result = await _searchService.ListAddedEmployeesTemp();
            //Assert.AreEqual(result.Result.IsValid, true);
        }

        private readonly EmployeeService _employeeService = new EmployeeService(new UnitOfWork("Host=127.0.0.1;Port=5432;Database=TradeUnionCommitteeEmployeesCore;Username=postgres;Password=postgres"));

        [TestMethod]
        public async Task TestEmployeeService()
        {
            var result = await _employeeService.AddEmployee(new AddEmployeeDTO
            {
                FirstName = "Петров",
                SecondName = "Петр",
                Patronymic = "Петрович",
                Sex = "Male",
                BasicProfission = "Программист",
                BirthDate = Convert.ToDateTime("01.01.1970"),
                StartYearWork = 1990,
                StartDateTradeUnion = Convert.ToDateTime("01.01.1990"),
                IdentificationСode = "0000000001",
                MechnikovCard = "0-000000-000001",
                MobilePhone = "+38(050)000-00-01",
                CityPhone = "000-00-01",
                Note = "Примечание"
            });
        }
    }
}