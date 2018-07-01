using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.DAL.Repositories.Account
{
    public class RolesRepository : Repository<Roles>
    {
        public RolesRepository(TradeUnionCommitteeEmployeesCoreContext db) : base(db)
        {
        }
    }
}