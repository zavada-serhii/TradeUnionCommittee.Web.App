using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.DAL.Repositories.Account
{
    public class UsersRepository : Repository<Users>
    {
        public UsersRepository(TradeUnionCommitteeEmployeesCoreContext db) : base(db)
        {
        }
    }
}