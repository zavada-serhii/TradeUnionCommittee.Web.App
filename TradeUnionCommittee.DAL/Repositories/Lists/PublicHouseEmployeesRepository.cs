using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.DAL.Repositories.Lists
{
    public class PublicHouseEmployeesRepository : Repository<PublicHouseEmployees>
    {
        public PublicHouseEmployeesRepository(TradeUnionCommitteeEmployeesCoreContext db) : base(db)
        {
        }
    }
}