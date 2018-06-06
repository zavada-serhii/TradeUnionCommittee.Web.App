using TradeUnionCommittee.Common.EF;
using TradeUnionCommittee.Common.Entities;

namespace TradeUnionCommittee.DAL.Repositories.Directory
{
    public class PositionRepository : Repository<Position>
    {
        public PositionRepository(TradeUnionCommitteeEmployeesCoreContext db) : base(db)
        {
        }
    }
}