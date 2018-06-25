using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.DAL.Repositories.Lists
{
    public class EventFamilyRepository : Repository<EventFamily>
    {
        public EventFamilyRepository(TradeUnionCommitteeEmployeesCoreContext db) : base(db)
        {
        }
    }
}