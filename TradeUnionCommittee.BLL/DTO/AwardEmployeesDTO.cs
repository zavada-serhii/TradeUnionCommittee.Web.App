using System;

namespace TradeUnionCommittee.BLL.DTO
{
    public class AwardEmployeesDTO
    {
        public string HashId { get; set; }
        public string HashIdEmployee { get; set; }
        public string HashIdAward { get; set; }
        public string NameAward { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateIssue { get; set; }
        public uint RowVersion { get; set; }
    }
}
