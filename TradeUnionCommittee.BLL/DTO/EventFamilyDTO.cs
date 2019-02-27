using System;

namespace TradeUnionCommittee.BLL.DTO
{
    public class EventFamilyDTO
    {
        public string HashId { get; set; }
        public string HashIdFamily { get; set; }
        public string HashIdEvent { get; set; }
        public string NameEvent { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public uint RowVersion { get; set; }
    }

    public class TravelFamilyDTO : EventFamilyDTO { }
    public class WellnessFamilyDTO : EventFamilyDTO { }
    public class TourFamilyDTO : EventFamilyDTO { }
}