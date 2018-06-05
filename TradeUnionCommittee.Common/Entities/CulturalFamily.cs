using System;

namespace TradeUnionCommittee.Common.Entities
{
    public class CulturalFamily
    {
        public long Id { get; set; }
        public long IdFamily { get; set; }
        public long IdCultural { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public DateTime DateVisit { get; set; }

        public Cultural IdCulturalNavigation { get; set; }
        public Family IdFamilyNavigation { get; set; }
    }
}
