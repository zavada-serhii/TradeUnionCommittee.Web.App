using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.DAL.Repositories.Directories
{
    public class SubdivisionsRepository : Repository<Subdivisions>
    {
        public SubdivisionsRepository(TradeUnionCommitteeContext db) : base(db)
        {
           
        }
    }
}