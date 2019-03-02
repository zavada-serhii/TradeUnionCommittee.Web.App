using System;

namespace TradeUnionCommittee.BLL.DTO.GrandChildren
{
    public class CulturalGrandChildrenDTO
    {
        public string HashId { get; set; }
        public string HashIdGrandChildren { get; set; }
        public string HashIdCultural { get; set; }
        public string NameCultural { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public DateTime DateVisit { get; set; }
        public uint RowVersion { get; set; }
    }
}