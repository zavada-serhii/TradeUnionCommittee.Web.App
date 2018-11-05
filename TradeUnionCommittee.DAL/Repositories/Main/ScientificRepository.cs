using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.DAL.Repositories.Main
{
    public class ScientificRepository : Repository<Scientific>
    {
        private readonly TradeUnionCommitteeEmployeesCoreContext _db;

        public ScientificRepository(TradeUnionCommitteeEmployeesCoreContext db) : base(db)
        {
            _db = db;
        }
    }
}