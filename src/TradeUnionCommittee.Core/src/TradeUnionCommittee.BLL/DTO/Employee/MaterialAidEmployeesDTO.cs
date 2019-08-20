using System;

namespace TradeUnionCommittee.BLL.DTO.Employee
{
    public class MaterialAidEmployeesDTO
    {
        public string HashId { get; set; }
        public string HashIdEmployee { get; set; }
        public string HashIdMaterialAid { get; set; }
        public string NameMaterialAid { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateIssue { get; set; }
        public uint RowVersion { get; set; }
    }
}