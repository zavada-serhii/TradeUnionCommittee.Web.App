using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace TradeUnionCommittee.DAL.Tests
{
    [TestClass]
    public class UnitTestRepositories : BaseClass
    {
        [TestMethod]
        public void TestMethodRepository()
        {
            var award = Work.AwardRepository.GetAll();
            var awardEmployee = Work.AwardEmployeesRepository.GetWithInclude(x => x.IdEmployee == 1);

            var result = (from ae in awardEmployee.Result
                          join a in award.Result 
                          on ae.IdAward equals a.Id
                          select new
                          {
                              ae.Id,
                              a.Name,
                              ae.Amount,
                              ae.DateIssue
                          }).ToList();
        }
    }
}