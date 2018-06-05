using System;

namespace TradeUnionCommittee.Common.Entities
{
    public class EventGrandChildrens
    {
        public long Id { get; set; }
        public long IdGrandChildren { get; set; }
        public long IdEvent { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Event IdEventNavigation { get; set; }
        public GrandChildren IdGrandChildrenNavigation { get; set; }
    }
}
