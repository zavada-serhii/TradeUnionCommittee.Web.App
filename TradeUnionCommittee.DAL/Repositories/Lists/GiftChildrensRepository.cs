using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.DAL.Repositories.Lists
{
    public class GiftChildrensRepository : Repository<GiftChildrens>
    {
        public GiftChildrensRepository(TradeUnionCommitteeContext db) : base(db)
        {
        }
    }
}