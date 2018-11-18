using System;
using TradeUnionCommittee.BLL.Enums;

namespace TradeUnionCommittee.BLL.DTO
{
    public class JournalDTO
    {
        public string Guid { get; set; }
        public Operations Operation { get; set; }
        public DateTime DateTime { get; set; }
        public string EmailUser { get; set; }
        public Tables Table { get; set; }
    }
}