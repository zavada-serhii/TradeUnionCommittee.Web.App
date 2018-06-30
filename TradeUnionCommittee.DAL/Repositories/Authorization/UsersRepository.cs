using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.DAL.Repositories.Authorization
{
    public class UsersRepository : Repository<Users>
    {
        public UsersRepository(TradeUnionCommitteeEmployeesCoreContext db) : base(db)
        {
        }
    }
}