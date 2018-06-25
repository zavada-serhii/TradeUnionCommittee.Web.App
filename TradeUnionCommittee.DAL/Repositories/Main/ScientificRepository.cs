using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.DAL.Repositories.Main
{
    public class ScientificRepository : Repository<Scientific>
    {
        public ScientificRepository(TradeUnionCommitteeEmployeesCoreContext db) : base(db)
        {
        }
    }
}