using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.DAL.Repositories.Directories
{
    public class EventRepository : Repository<Event>
    {
        public EventRepository(TradeUnionCommitteeContext db) : base(db)
        {
        }
    }
}