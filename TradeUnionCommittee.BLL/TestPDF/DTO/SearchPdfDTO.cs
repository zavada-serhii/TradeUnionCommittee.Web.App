using TradeUnionCommittee.BLL.Enums;

namespace TradeUnionCommittee.BLL.TestPDF.DTO
{
    public class SearchPdfDTO : BasePdfDTO
    {
        public long HashEventId { get; set; }
        public SearchType Type { get; set; }
    }
}
