using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.DAL.Repositories.Directories
{
    public class TypeEventRepository : Repository<TypeEvent>
    {
        public TypeEventRepository(TradeUnionCommitteeEmployeesCoreContext db) : base(db)
        {
        }
    }
}