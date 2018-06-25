using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.DAL.Repositories.Main
{
    public class ChildrenRepository : Repository<Children>
    {
        public ChildrenRepository(TradeUnionCommitteeEmployeesCoreContext db) : base(db)
        {
        }
    }
}