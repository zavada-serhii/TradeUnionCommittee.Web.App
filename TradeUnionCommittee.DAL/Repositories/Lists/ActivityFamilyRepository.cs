using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.DAL.Repositories.Lists
{
    public class ActivityFamilyRepository : Repository<ActivityFamily>
    {
        public ActivityFamilyRepository(TradeUnionCommitteeEmployeesCoreContext db) : base(db)
        {
        }
    }
}