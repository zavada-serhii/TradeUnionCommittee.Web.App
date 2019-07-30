using System;
using TradeUnionCommittee.DAL.Audit.Enums;

namespace TradeUnionCommittee.DAL.Audit.Entities
{
    public class Journal
    {
        public string Guid { get; set; }
        public Operations Operation { get; set; }
        public string IpUser { get; set; }
        public DateTime DateTime { get; set; }
        public string EmailUser { get; set; }
        public Tables Table { get; set; }
    }
}