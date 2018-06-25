using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.DAL.Repositories.Lists
{
    public class EventEmployeesRepository : Repository<EventEmployees>
    {
        public EventEmployeesRepository(TradeUnionCommitteeEmployeesCoreContext db) : base(db)
        {
        }
    }
}