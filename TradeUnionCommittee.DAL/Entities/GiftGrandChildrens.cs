using System;
using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.DAL.Entities
{
    public class GiftGrandChildrens
    {
        public long Id { get; set; }
        [ConcurrencyCheck]
        public long IdGrandChildren { get; set; }
        [ConcurrencyCheck]
        public string NameEvent { get; set; }
        [ConcurrencyCheck]
        public string NameGifts { get; set; }
        [ConcurrencyCheck]
        public decimal Price { get; set; }
        [ConcurrencyCheck]
        public decimal Discount { get; set; }
        [ConcurrencyCheck]
        public DateTime DateGift { get; set; }

        public GrandChildren IdGrandChildrenNavigation { get; set; }
    }
}
