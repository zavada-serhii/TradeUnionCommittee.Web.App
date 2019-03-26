using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.DAL.Repositories.Lists
{
    public class GiftGrandChildrensRepository : Repository<GiftGrandChildrens>
    {
        public GiftGrandChildrensRepository(TradeUnionCommitteeContext db) : base(db)
        {
        }
    }
}