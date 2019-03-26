using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.DAL.Repositories.Lists
{
    public class CulturalChildrensRepository : Repository<CulturalChildrens>
    {
        public CulturalChildrensRepository(TradeUnionCommitteeContext db) : base(db)
        {
        }
    }
}