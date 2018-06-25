using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.DAL.Repositories.Lists
{
    public class AwardEmployeesRepository : Repository<AwardEmployees>
    {
        public AwardEmployeesRepository(TradeUnionCommitteeEmployeesCoreContext db) : base(db)
        {
        }
    }
}