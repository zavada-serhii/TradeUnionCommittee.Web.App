using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.DAL.Repositories.Lists
{
    public class EventGrandChildrensRepository : Repository<EventGrandChildrens>
    {
        public EventGrandChildrensRepository(TradeUnionCommitteeEmployeesCoreContext db) : base(db)
        {
        }
    }
}