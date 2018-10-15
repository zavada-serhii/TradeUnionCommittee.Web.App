using System;

namespace TradeUnionCommittee.BLL.DTO
{
    public class ReportDTO
    {
        public long HashId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string PathToSave { get; set; }
    }
}