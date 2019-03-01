using System;

namespace TradeUnionCommittee.BLL.DTO.Children
{
    public class CulturalChildrenDTO
    {
        public string HashId { get; set; }
        public string HashIdChildren { get; set; }
        public string HashIdCultural { get; set; }
        public string NameCultural { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public DateTime DateVisit { get; set; }
        public uint RowVersion { get; set; }
    }
}