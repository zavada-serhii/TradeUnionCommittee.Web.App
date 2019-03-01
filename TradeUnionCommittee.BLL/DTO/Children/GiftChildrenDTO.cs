using System;

namespace TradeUnionCommittee.BLL.DTO.Children
{
    public class GiftChildrenDTO
    {
        public string HashId { get; set; }
        public string HashIdChildren { get; set; }
        public string NameEvent { get; set; }
        public string NameGift { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public DateTime DateGift { get; set; }
        public uint RowVersion { get; set; }
    }
}