using Microsoft.VisualStudio.TestTools.UnitTesting;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Services.Directory;
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
    }
}