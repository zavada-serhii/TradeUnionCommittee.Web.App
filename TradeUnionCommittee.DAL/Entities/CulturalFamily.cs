using System;
using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.DAL.Entities
{
    public class CulturalFamily
    {
        public long Id { get; set; }
        [ConcurrencyCheck]
        public long IdFamily { get; set; }
        [ConcurrencyCheck]
        public long IdCultural { get; set; }
        [ConcurrencyCheck]
        public decimal Amount { get; set; }
        [ConcurrencyCheck]
        public decimal Discount { get; set; }
        [ConcurrencyCheck]
        public DateTime DateVisit { get; set; }

        public Cultural IdCulturalNavigation { get; set; }
        public Family IdFamilyNavigation { get; set; }
    }
}
