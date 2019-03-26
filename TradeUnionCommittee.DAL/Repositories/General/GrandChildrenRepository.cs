using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.DAL.Repositories.General
{
    public class GrandChildrenRepository : Repository<GrandChildren>
    {
        public GrandChildrenRepository(TradeUnionCommitteeContext db) : base(db)
        {
        }
    }
}