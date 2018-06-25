using Microsoft.VisualStudio.TestTools.UnitTesting;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Repositories;

namespace TradeUnionCommittee.DAL.Tests
{
    [TestClass]
    public class UnitTestDirectoryRepositories
    {
        private readonly UnitOfWork _work = new UnitOfWork("Host=127.0.0.1;Port=5432;Database=TradeUnionCommitteeEmployeesCore;Username=AdminTradeUnionCommitteeEmployees;Password=admin");

        [TestMethod]
        public void TestCreatePositionRepository()
        {
            var result = _work.PositionRepository.Create(new Position
            {
                Name = "Доцент"
            });
            Assert.AreEqual(result.IsValid, true);
        }

        [TestMethod]
        public void TestEditPositionRepository()
        {
            var result = _work.PositionRepository.Update(new Position
            {
                Id = 5,
                Name = "Преподаватель"
            });
            Assert.AreEqual(result.IsValid, true);
        }

        [TestMethod]
        public void TestRemovePositionRepository()
        {
            var result = _work.PositionRepository.Delete(5);

            Assert.AreEqual(result.IsValid, true);
        }

        [TestMethod]
        public void TestGetPositionRepository()
        {
            var result = _work.PositionRepository.Get(1);

            Assert.AreEqual(result.IsValid, true);
        }

        [TestMethod]
        public void TestGetAllPositionRepository()
        {
            var result = _work.PositionRepository.GetAll();

            Assert.AreEqual(result.IsValid, true);
        }
    }
}