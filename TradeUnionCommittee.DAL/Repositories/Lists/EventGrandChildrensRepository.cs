using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.DAL.Repositories.Lists
{
    public class EventGrandChildrensRepository : Repository<EventGrandChildrens>
    {
        public EventGrandChildrensRepository(TradeUnionCommitteeContext db) : base(db)
        {
        }
    }
}