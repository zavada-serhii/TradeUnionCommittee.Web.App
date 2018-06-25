using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.DAL.Repositories.Directories
{
    public class AwardRepository : Repository<Award>
    {
        public AwardRepository(TradeUnionCommitteeEmployeesCoreContext db) : base(db)
        {
        }
    }
}