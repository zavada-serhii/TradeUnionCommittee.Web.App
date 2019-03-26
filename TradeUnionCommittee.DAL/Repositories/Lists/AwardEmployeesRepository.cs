using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.DAL.Repositories.Lists
{
    public class AwardEmployeesRepository : Repository<AwardEmployees>
    {
        public AwardEmployeesRepository(TradeUnionCommitteeContext db) : base(db)
        {
        }
    }
}