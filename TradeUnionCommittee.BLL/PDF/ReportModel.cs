using System;
using System.Collections.Generic;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.BLL.PDF
{
    public class ReportModel
    {
        public string PathToSave { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IEnumerable<MaterialAidEmployees> MaterialAidEmployees { get; set; }
        public IEnumerable<AwardEmployees> AwardEmployees { get; set; }
    }
}
