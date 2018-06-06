using System;

namespace TradeUnionCommittee.DAL.Entities
{
    public class GiftChildrens
    {
        public long Id { get; set; }
        public long IdChildren { get; set; }
        public string NameEvent { get; set; }
        public string NameGift { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public DateTime DateGift { get; set; }

        public Children IdChildrenNavigation { get; set; }
    }
}
