using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.DAL.Repositories.Lists
{
    public class CulturalGrandChildrensRepository : Repository<CulturalGrandChildrens>
    {
        public CulturalGrandChildrensRepository(TradeUnionCommitteeEmployeesCoreContext db) : base(db)
        {
        }
    }
}