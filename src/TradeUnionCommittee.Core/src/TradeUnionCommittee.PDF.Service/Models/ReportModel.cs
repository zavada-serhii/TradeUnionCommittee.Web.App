using TradeUnionCommittee.PDF.Service.Enums;

namespace TradeUnionCommittee.PDF.Service.Models
{
    public class ReportModel : DataModel
    {
        public TypeReport TypeReport { get; set; }
        public string HashIdEmployee { get; set; }
        public string FullNameEmployee { get; set; }
    }
}
