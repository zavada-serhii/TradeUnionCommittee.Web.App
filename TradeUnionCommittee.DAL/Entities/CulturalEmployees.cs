using System;

namespace TradeUnionCommittee.DAL.Entities
{
    public class CulturalEmployees
    {
        public long Id { get; set; }
        public long IdEmployee { get; set; }
        public long IdCultural { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public DateTime DateVisit { get; set; }

        public Cultural IdCulturalNavigation { get; set; }
        public Employee IdEmployeeNavigation { get; set; }
    }
}
