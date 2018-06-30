using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.DAL.Repositories.Authorization
{
    public class RolesRepository : Repository<Roles>
    {
        public RolesRepository(TradeUnionCommitteeEmployeesCoreContext db) : base(db)
        {
        }
    }
}