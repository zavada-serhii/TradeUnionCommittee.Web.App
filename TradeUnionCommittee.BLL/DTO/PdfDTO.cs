using System;

namespace TradeUnionCommittee.BLL.DTO
{
    public class PdfDTO
    {
        public long HashUserId { get; set; }
        public long HashEventId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string PathToSave { get; set; }
    }
}