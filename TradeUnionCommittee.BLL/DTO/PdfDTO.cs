using System;
using TradeUnionCommittee.BLL.Enums;

namespace TradeUnionCommittee.BLL.DTO
{
    public class BasePdfDTO
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class ReportPdfDTO : BasePdfDTO
    {
        public string HashUserId { get; set; }
        public TypeReport Type { get; set; }
    }
}