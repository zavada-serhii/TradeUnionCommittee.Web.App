using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.DAL.Repositories.Directories
{
    public class CulturalRepository : Repository<Cultural>
    {
        public CulturalRepository(TradeUnionCommitteeContext db) : base(db)
        {
        }
    }
}