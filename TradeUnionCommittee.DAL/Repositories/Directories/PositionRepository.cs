using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.DAL.Repositories.Directories
{
    public class PositionRepository : Repository<Position>
    {
        public PositionRepository(TradeUnionCommitteeEmployeesCoreContext db) : base(db)
        {
        }
    }
}