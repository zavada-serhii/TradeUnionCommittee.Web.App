using System;

namespace TradeUnionCommittee.BLL.PDF.DTO
{
    public class BasePdfDTO
    {
        public string PathToSave { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
