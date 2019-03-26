using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.DAL.Repositories.Directories
{
    public class AwardRepository : Repository<Award>
    {
        public AwardRepository(TradeUnionCommitteeContext db) : base(db)
        {
        }
    }
}