using System;

namespace TradeUnionCommittee.DAL.Entities
{
    public class AwardEmployees
    {
        public long Id { get; set; }
        public long IdEmployee { get; set; }
        public long IdAward { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateIssue { get; set; }

        public Award IdAwardNavigation { get; set; }
        public Employee IdEmployeeNavigation { get; set; }
    }
}
