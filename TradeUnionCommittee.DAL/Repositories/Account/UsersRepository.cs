using System;
using System.Linq;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.DAL.Repositories.Account
{
    public class UsersRepository : Repository<Users>
    {
        private readonly TradeUnionCommitteeEmployeesCoreContext _db;

        public UsersRepository(TradeUnionCommitteeEmployeesCoreContext db) : base(db)
        {
            _db = db;
        }

        public override ActualResult Update(Users item)
        {
            var result = new ActualResult();
            try
            {
                var user = _db.Users.Where(x => x.Id == item.Id);

                if (item.Password == null)
                {
                    foreach (var userse in user)
                    {
                        userse.Email = item.Email;
                        userse.IdRole = item.IdRole;
                    }
                }
                else
                {
                    foreach (var userse in user)
                    {
                        userse.Password = item.Password;
                    }
                }
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