using System;

namespace TradeUnionCommittee.Common.Entities
{
    public class EventChildrens
    {
        public long Id { get; set; }
        public long IdChildren { get; set; }
        public long IdEvent { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Children IdChildrenNavigation { get; set; }
        public Event IdEventNavigation { get; set; }
    }
}
