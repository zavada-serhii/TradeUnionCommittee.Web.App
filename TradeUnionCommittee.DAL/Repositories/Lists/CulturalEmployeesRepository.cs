using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.DAL.Repositories.Lists
{
    public class CulturalEmployeesRepository : Repository<CulturalEmployees>
    {
        public CulturalEmployeesRepository(TradeUnionCommitteeContext db) : base(db)
        {
        }
    }
}