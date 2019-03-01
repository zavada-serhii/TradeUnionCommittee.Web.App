using System;

namespace TradeUnionCommittee.BLL.DTO
{
    public class EventChildrenDTO
    {
        public string HashId { get; set; }
        public string HashIdChildren { get; set; }
        public string HashIdEvent { get; set; }
        public string NameEvent { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public uint RowVersion { get; set; }
    }

    public class TravelChildrenDTO : EventChildrenDTO { }

    public class WellnessChildrenDTO : EventChildrenDTO { }

    public class TourChildrenDTO : EventChildrenDTO { }
}