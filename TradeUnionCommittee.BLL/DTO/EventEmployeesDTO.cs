using System;

namespace TradeUnionCommittee.BLL.DTO
{
    public abstract class EventEmployeesDTO
    {
        public string HashId { get; set; }
        public string HashIdEmployee { get; set; }
        public string HashIdEvent { get; set; }
        public string NameEvent { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public uint RowVersion { get; set; }
    }

    public class TravelEmployeesDTO : EventEmployeesDTO { }

    public class WellnessEmployeesDTO : EventEmployeesDTO { }

    public class TourEmployeesDTO : EventEmployeesDTO { }
}