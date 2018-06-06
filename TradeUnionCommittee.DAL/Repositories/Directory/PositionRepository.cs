using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.DAL.Repositories.Directory
{
    public class PositionRepository : Repository<Position>
    {
        public PositionRepository(TradeUnionCommitteeEmployeesCoreContext db) : base(db)
        {
        }
    }
}