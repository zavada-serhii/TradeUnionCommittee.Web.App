using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.DAL.Repositories.Lists
{
    public class SocialActivityEmployeesRepository : Repository<SocialActivityEmployees>
    {
        public SocialActivityEmployeesRepository(TradeUnionCommitteeContext db) : base(db)
        {
        }
    }
}