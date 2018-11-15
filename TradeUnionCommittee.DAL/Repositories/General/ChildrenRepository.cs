using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.DAL.Repositories.General
{
    public class ChildrenRepository : Repository<Children>
    {
        public ChildrenRepository(TradeUnionCommitteeEmployeesCoreContext db) : base(db)
        {
        }
    }
}