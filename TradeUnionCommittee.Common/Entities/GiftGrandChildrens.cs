using System;

namespace TradeUnionCommittee.Common.Entities
{
    public class GiftGrandChildrens
    {
        public long Id { get; set; }
        public long IdGrandChildren { get; set; }
        public string NameEvent { get; set; }
        public string NameGifts { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public DateTime DateGift { get; set; }

        public GrandChildren IdGrandChildrenNavigation { get; set; }
    }
}
