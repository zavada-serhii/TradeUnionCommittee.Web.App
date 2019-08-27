using System;
using TradeUnionCommittee.PDF.Service.Enums;

namespace TradeUnionCommittee.PDF.Service.Entities
{
    public class MaterialIncentivesEmployeeEntity
    {
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }

    public class CulturalEmployeeEntity
    {
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public DateTime Date { get; set; }
    }

    public class EventEmployeeEntity
    {
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TypeEvent TypeEvent { get; set; }
    }

    public class GiftEmployeeEntity
    {
        public string Name { get; set; }
        public string NameGift { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public DateTime Date { get; set; }
    }
}
