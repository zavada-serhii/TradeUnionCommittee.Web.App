using TradeUnionCommittee.BLL.Enums;

namespace TradeUnionCommittee.BLL.PDF.DTO
{
    public class ReportPdfDTO : BasePdfDTO
    {
        public long HashUserId { get; set; }
        public ReportType Type { get; set; }
    }
}
