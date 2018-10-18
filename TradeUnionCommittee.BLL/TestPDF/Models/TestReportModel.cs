using System.Collections.Generic;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.BLL.TestPDF.Models
{
    public class TestReportModel : TestBaseModel
    {
        public ReportType Type { get; set; }
        public string FullNameEmployee { get; set; }

        public IEnumerable<MaterialAidEmployees> MaterialAidEmployees { get; set; }
        public IEnumerable<AwardEmployees> AwardEmployees { get; set; }
        public IEnumerable<CulturalEmployees> CulturalEmployees { get; set; }
        public IEnumerable<EventEmployees> EventEmployees { get; set; }
        public IEnumerable<GiftEmployees> GiftEmployees { get; set; }
    }
}
