using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.DAL.Repositories.Main
{
    public class EmployeeRepository : Repository<Employee>
    {
        public EmployeeRepository(TradeUnionCommitteeEmployeesCoreContext db) : base(db)
        {
        }
    }
}