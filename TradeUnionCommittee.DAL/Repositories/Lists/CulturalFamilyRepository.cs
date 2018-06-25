using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.DAL.Repositories.Lists
{
    public class CulturalFamilyRepository : Repository<CulturalFamily>
    {
        public CulturalFamilyRepository(TradeUnionCommitteeEmployeesCoreContext db) : base(db)
        {
        }
    }
}