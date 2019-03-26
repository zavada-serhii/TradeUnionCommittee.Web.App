using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.DAL.Repositories.Lists
{
    public class ActivityEmployeesRepository : Repository<ActivityEmployees>
    {
        public ActivityEmployeesRepository(TradeUnionCommitteeContext db) : base(db)
        {
        }
    }
}