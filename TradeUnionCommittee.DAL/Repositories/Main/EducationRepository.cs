using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.DAL.Repositories.Main
{
    public class EducationRepository : Repository<Education>
    {
        public EducationRepository(TradeUnionCommitteeEmployeesCoreContext db) : base(db)
        {
        }
    }
}