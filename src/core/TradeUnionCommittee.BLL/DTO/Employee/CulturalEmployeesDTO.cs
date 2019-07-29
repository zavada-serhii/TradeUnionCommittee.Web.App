using System;

namespace TradeUnionCommittee.BLL.DTO.Employee
{
    public class CulturalEmployeesDTO
    {
        public string HashId { get; set; }
        public string HashIdEmployee { get; set; }
        public string HashIdCultural { get; set; }
        public string NameCultural { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public DateTime DateVisit { get; set; }
        public uint RowVersion { get; set; }
    }
}