using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.DAL.Repositories.Lists
{
    public class EventChildrensRepository : Repository<EventChildrens>
    {
        public EventChildrensRepository(TradeUnionCommitteeContext db) : base(db)
        {
        }
    }
}