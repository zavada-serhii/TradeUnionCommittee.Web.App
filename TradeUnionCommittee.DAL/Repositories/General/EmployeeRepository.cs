using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.DAL.Repositories.General
{
    public class EmployeeRepository : Repository<Employee>
    {
        public EmployeeRepository(TradeUnionCommitteeContext db) : base(db)
        {
        }
    }
}