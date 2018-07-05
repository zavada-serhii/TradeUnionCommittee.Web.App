using System;
using System.Linq;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Interfaces.User;

namespace TradeUnionCommittee.DAL.Repositories.Account
{
    public class UsersRepository : Repository<Users>, IUserRepository
    {
        private readonly TradeUnionCommitteeEmployeesCoreContext _db;

        public UsersRepository(TradeUnionCommitteeEmployeesCoreContext db) : base(db)
        {
            _db = db;
        }

        public ActualResult UpdatePersonalData(long idUser, string email, long idRole)
        {
            var result = new ActualResult();
            try
            {
                var user = _db.Users.Where(x => x.Id == idUser);
                foreach (var userse in user)
                {
                    userse.Email = email;
                    userse.IdRole = idRole;
                }
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                result.IsValid = false;
                result.ErrorsList.Add(e.Message);
            }
            return result;
        }

        public ActualResult UpdatePassword(long idUser, string password)
        {
            var result = new ActualResult();
            try
            {
                var user = _db.Users.Where(x => x.Id == idUser);
                foreach (var userse in user)
                {
                    userse.Password = password;
                }
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                result.IsValid = false;
                result.ErrorsList.Add(e.Message);
            }
            return result;
        }
    }
}