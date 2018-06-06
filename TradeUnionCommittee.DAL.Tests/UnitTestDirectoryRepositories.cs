using Microsoft.VisualStudio.TestTools.UnitTesting;
using TradeUnionCommittee.DAL.Repositories;

namespace TradeUnionCommittee.DAL.Tests
{
    [TestClass]
    public class UnitTestDirectoryRepositories
    {
        [TestMethod]
        public void TestPositionRepository()
        {
            var work = new UnitOfWork();

            work.PositionRepository.GetAll();
        }
    }
}