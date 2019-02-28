using System;

namespace TradeUnionCommittee.BLL.DTO
{
    public class CulturalFamilyDTO
    {
        public string HashId { get; set; }
        public string HashIdFamily { get; set; }
        public string HashIdCultural { get; set; }
        public string NameCultural { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public DateTime DateVisit { get; set; }
        public uint RowVersion { get; set; }
    }
}