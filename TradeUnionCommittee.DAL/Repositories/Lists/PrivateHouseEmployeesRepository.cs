using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.DAL.Repositories.Lists
{
    class PrivateHouseEmployeesRepository : Repository<PrivateHouseEmployees>
    {
        public PrivateHouseEmployeesRepository(TradeUnionCommitteeContext db) : base(db)
        {
        }
    }
}