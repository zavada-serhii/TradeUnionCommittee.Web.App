using System;

namespace TradeUnionCommittee.DAL.Entities
{
    public class CulturalGrandChildrens
    {
        public long Id { get; set; }
        public long IdGrandChildren { get; set; }
        public long IdCultural { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public DateTime DateVisit { get; set; }

        public Cultural IdCulturalNavigation { get; set; }
        public GrandChildren IdGrandChildrenNavigation { get; set; }
    }
}
