using System;

namespace TradeUnionCommittee.Common.Entities
{
    public class EventEmployees
    {
        public long Id { get; set; }
        public long IdEmployee { get; set; }
        public long IdEvent { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Employee IdEmployeeNavigation { get; set; }
        public Event IdEventNavigation { get; set; }
    }
}
