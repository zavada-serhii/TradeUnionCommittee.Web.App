using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.DAL.Repositories.Lists
{
    public class PrivilegeEmployeesRepository : Repository<PrivilegeEmployees>
    {
        public PrivilegeEmployeesRepository(TradeUnionCommitteeContext db) : base(db)
        {
        }
    }
}