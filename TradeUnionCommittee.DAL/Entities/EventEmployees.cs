using System;
using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.DAL.Entities
{
    public class EventEmployees
    {
        public long Id { get; set; }
        [ConcurrencyCheck]
        public long IdEmployee { get; set; }
        [ConcurrencyCheck]
        public long IdEvent { get; set; }
        [ConcurrencyCheck]
        public decimal Amount { get; set; }
        [ConcurrencyCheck]
        public decimal Discount { get; set; }
        [ConcurrencyCheck]
        public DateTime StartDate { get; set; }
        [ConcurrencyCheck]
        public DateTime EndDate { get; set; }

        public Employee IdEmployeeNavigation { get; set; }
        public Event IdEventNavigation { get; set; }
    }
}
