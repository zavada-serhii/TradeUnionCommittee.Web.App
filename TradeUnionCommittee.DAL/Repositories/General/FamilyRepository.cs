using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.DAL.Repositories.Main
{
    public class FamilyRepository : Repository<Family>
    {
        public FamilyRepository(TradeUnionCommitteeEmployeesCoreContext db) : base(db)
        {
        }
    }
}