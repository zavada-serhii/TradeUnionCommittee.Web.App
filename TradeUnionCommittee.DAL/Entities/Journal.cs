using System;

namespace TradeUnionCommittee.DAL.Entities
{
    public class Journal
    {
        public string Guid { get; set; }
        public Operations Operation { get; set; }
        public DateTime DateTime { get; set; }
        public string EmailUser { get; set; }
        public Tables Table { get; set; }
    }
}
