using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.DAL.Repositories.Lists
{
    public class GiftEmployeesRepository : Repository<GiftEmployees>
    {
        public GiftEmployeesRepository(TradeUnionCommitteeContext db) : base(db)
        {
        }
    }
}