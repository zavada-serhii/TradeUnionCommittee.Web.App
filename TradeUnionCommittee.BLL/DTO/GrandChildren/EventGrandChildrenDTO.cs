using System;

namespace TradeUnionCommittee.BLL.DTO.GrandChildren
{
    public class EventGrandChildrenDTO
    {
        public string HashId { get; set; }
        public string HashIdGrandChildren { get; set; }
        public string HashIdEvent { get; set; }
        public string NameEvent { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public uint RowVersion { get; set; }
    }

    public class TravelGrandChildrenDTO : EventGrandChildrenDTO
    {
    }

    public class TourGrandChildrenDTO : EventGrandChildrenDTO
    {
    }
}