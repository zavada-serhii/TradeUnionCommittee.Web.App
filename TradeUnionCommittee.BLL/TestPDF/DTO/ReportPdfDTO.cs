using TradeUnionCommittee.BLL.Enums;

namespace TradeUnionCommittee.BLL.TestPDF.DTO
{
    public class ReportPdfDTO : BasePdfDTO
    {
        public long HashUserId { get; set; }
        public ReportType Type { get; set; }
    }
}
