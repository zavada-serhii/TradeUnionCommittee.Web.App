using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.DAL.Repositories.Lists
{
    public class ActivityGrandChildrensRepository : Repository<ActivityGrandChildrens>
    {
        public ActivityGrandChildrensRepository(TradeUnionCommitteeContext db) : base(db)
        {
        }
    }
}