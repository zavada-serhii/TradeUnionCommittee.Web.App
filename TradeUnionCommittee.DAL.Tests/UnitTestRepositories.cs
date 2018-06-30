using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.DAL.Entities;

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


            var role = Work.RolesRepository.GetAll();
            var user = Work.UsersRepository.Find(x => x.Email == "stewie.griffin@test.com" && x.Password == "test123");

            var resUser = from u in user.Result
                join r in role.Result
                on u.IdRole equals r.Id
                select new
                {
                    r.Name
                };
        }


        [TestMethod]
        public async Task TestMethodCreateRolesRepository()
        {
            var res = Work.RolesRepository.Create(new Roles {Name = "Admin"});
            await Work.SaveAsync();
            Assert.AreEqual(res.IsValid, true);
        }

        [TestMethod]
        public void TestMethodGetAllRolesRepository()
        {
            var res = Work.RolesRepository.GetAll();
            Assert.AreEqual(res.IsValid, true);
        }

        [TestMethod]
        public void TestMethodFindRolesRepository()
        {
            var res = Work.RolesRepository.Find(x => x.Name == "Admin");
            Assert.AreEqual(res.IsValid, true);
        }

        [TestMethod]
        public void TestMethodGetRoleRepository()
        {
            var res = Work.RolesRepository.Get(1);
            Assert.AreEqual(res.IsValid, true);
        }

        [TestMethod]
        public async Task TestMethodUpdateRolesRepository()
        {
            var res = Work.RolesRepository.Update(new Roles {Id = 1, Name = "Admin.Test"});
            await Work.SaveAsync();
            Assert.AreEqual(res.IsValid, true);
        }

        [TestMethod]
        public async Task TestMethodDeleteRolesRepository()
        {
            var res = Work.RolesRepository.Delete(1);
            await Work.SaveAsync();
            Assert.AreEqual(res.IsValid, true);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [TestMethod]
        public async Task TestMethodCreateUserRepository()
        {
            var res = Work.UsersRepository.Create(
                new Users {Email = "stewie.griffin@test.com", IdRole = 1, Password = "test123"});
            await Work.SaveAsync();
            Assert.AreEqual(res.IsValid, true);
        }

        [TestMethod]
        public void TestMethodGetAllUserRepository()
        {
            var res = Work.UsersRepository.GetAll();
            Assert.AreEqual(res.IsValid, true);
        }

        [TestMethod]
        public void TestMethodFindUserRepository()
        {
            var res = Work.UsersRepository.Find(x => x.Email == "stewie.griffin@test.com");
            Assert.AreEqual(res.IsValid, true);
        }

        [TestMethod]
        public void TestMethodGetUserRepository()
        {
            var res = Work.UsersRepository.Get(1);
            Assert.AreEqual(res.IsValid, true);
        }

        [TestMethod]
        public async Task TestMethodUpdateUserRepository()
        {
            var res = Work.UsersRepository.Update(new Users
            {
                Id = 1,
                Email = "stewie.griffin.test@test.com",
                IdRole = 1,
                Password = "test123test"
            });
            await Work.SaveAsync();
            Assert.AreEqual(res.IsValid, true);
        }

        [TestMethod]
        public async Task TestMethodDeleteUserRepository()
        {
            var res = Work.UsersRepository.Delete(1);
            await Work.SaveAsync();
            Assert.AreEqual(res.IsValid, true);
        }
    }
}