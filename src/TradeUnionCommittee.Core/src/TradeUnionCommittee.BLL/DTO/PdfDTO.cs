using System;
using TradeUnionCommittee.BLL.Enums;

namespace TradeUnionCommittee.BLL.DTO
{
    public class BasePdfDTO
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string EmailUser { get; set; }
        public string IpUser { get; set; }
    }

    public class ReportPdfDTO : BasePdfDTO
    {
        public string HashIdEmployee { get; set; }
        public TypeReport Type { get; set; }
    }
}