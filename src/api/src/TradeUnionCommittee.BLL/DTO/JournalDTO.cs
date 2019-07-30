using System;

namespace TradeUnionCommittee.BLL.DTO
{
    public class JournalDTO
    {
        public string Operation { get; set; }
        public DateTime DateTime { get; set; }
        public string EmailUser { get; set; }
        public string Tables { get; set; }
    }
}