using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.DAL.Repositories.Lists
{
    public class EventEmployeesRepository : Repository<EventEmployees>
    {
        public EventEmployeesRepository(TradeUnionCommitteeContext db) : base(db)
        {
        }
    }
}