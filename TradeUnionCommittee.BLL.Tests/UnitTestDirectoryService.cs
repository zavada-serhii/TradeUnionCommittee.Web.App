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

        private readonly EmployeeService _employeeService = new EmployeeService(new UnitOfWork("Server=anton-db-server.postgres.database.azure.com;Database=TradeUnionCommitteeEmployeesCore;Port=5432;User Id=postgres@anton-db-server;Password=7355608@123veyder;"));

        [TestMethod]
        public async Task TestEmployeeService()
        {
            var result = await _employeeService.GetMainInfoEmployeeAsync(1);
        }
    }
}