using System;
using TradeUnionCommittee.BLL.Enums;

namespace TradeUnionCommittee.BLL.TestPDF.Models
{
    public class TestBaseModel
    {
        public ReportType Type { get; set; }
        public string PathToSave { get; set; }
        public string FullNameEmployee { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}