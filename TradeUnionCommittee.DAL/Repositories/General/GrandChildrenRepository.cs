using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.DAL.Repositories.Main
{
    public class GrandChildrenRepository : Repository<GrandChildren>
    {
        public GrandChildrenRepository(TradeUnionCommitteeEmployeesCoreContext db) : base(db)
        {
        }
    }
}