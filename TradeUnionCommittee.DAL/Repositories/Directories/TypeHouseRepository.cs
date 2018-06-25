using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.DAL.Repositories.Directories
{
    public class TypeHouseRepository : Repository<TypeHouse>
    {
        public TypeHouseRepository(TradeUnionCommitteeEmployeesCoreContext db) : base(db)
        {
        }
    }
}