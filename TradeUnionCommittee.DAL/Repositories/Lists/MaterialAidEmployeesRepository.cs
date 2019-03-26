using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.DAL.Repositories.Lists
{
    public class MaterialAidEmployeesRepository : Repository<MaterialAidEmployees>
    {
        public MaterialAidEmployeesRepository(TradeUnionCommitteeContext db) : base(db)
        {
        }
    }
}