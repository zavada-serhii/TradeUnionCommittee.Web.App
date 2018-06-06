using System;

namespace TradeUnionCommittee.DAL.Entities
{
    public class CulturalChildrens
    {
        public long Id { get; set; }
        public long IdChildren { get; set; }
        public long IdCultural { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public DateTime DateVisit { get; set; }

        public Children IdChildrenNavigation { get; set; }
        public Cultural IdCulturalNavigation { get; set; }
    }
}
