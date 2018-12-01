using System;
using TradeUnionCommittee.PDF.Service.Enums;

namespace TradeUnionCommittee.PDF.Service.Entities
{
    public abstract class BaseReportEntity
    {
        public string Name { get; set; }
        public decimal Amount { get; set; }
    }

    //---------------------------------------------------

    public class MaterialIncentivesEmployeeEntity : BaseReportEntity
    {
        public DateTime Date { get; set; }
    }

    public class CulturalEmployeeEntity : MaterialIncentivesEmployeeEntity
    {
        public decimal Discount { get; set; }
    }

    //---------------------------------------------------

    public class EventEmployeeEntity : BaseReportEntity
    {
        public decimal Discount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TypeEvent TypeEvent { get; set; }
    }

    public class GiftEmployeeEntity : CulturalEmployeeEntity
    {
        public string NameGift { get; set; }
    }
}
