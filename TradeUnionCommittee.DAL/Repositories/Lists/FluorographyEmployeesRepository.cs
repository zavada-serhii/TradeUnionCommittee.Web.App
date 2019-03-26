using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.DAL.Repositories.Lists
{
    public class FluorographyEmployeesRepository : Repository<FluorographyEmployees>
    {
        public FluorographyEmployeesRepository(TradeUnionCommitteeContext db) : base(db)
        {
        }
    }
}