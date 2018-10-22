using TradeUnionCommittee.BLL.Enums;

namespace TradeUnionCommittee.BLL.TestPDF.Models
{
    public class ReportModel : DataModel
    {
        public ReportType Type { get; set; }
        public string FullNameEmployee { get; set; }
    }
}
