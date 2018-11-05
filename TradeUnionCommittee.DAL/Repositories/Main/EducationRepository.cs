using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.DAL.Repositories.Main
{
    public class EducationRepository : Repository<Education>
    {
        private readonly TradeUnionCommitteeEmployeesCoreContext _db;

        public EducationRepository(TradeUnionCommitteeEmployeesCoreContext db) : base(db)
        {
            _db = db;
        }
    }
}