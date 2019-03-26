using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.DAL.Repositories.Lists
{
    public class PositionEmployeesRepository : Repository<PositionEmployees>
    {
        public PositionEmployeesRepository(TradeUnionCommitteeContext db) : base(db)
        {
        }
    }
}