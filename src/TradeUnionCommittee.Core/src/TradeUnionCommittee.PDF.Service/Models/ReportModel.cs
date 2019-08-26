using TradeUnionCommittee.PDF.Service.Enums;

namespace TradeUnionCommittee.PDF.Service.Models
{
    public class ReportModel : DataModel
    {
        public string FileName { get; set; }
        public TypeReport Type { get; set; }
        public string HashIdEmployee { get; set; }
        public string FullNameEmployee { get; set; }
    }
}
