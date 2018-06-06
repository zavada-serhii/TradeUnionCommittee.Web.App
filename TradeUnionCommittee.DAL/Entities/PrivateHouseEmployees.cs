using System;

namespace TradeUnionCommittee.DAL.Entities
{
    public class PrivateHouseEmployees
    {
        public long Id { get; set; }
        public long IdEmployee { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string NumberHouse { get; set; }
        public string NumberApartment { get; set; }
        public DateTime? DateReceiving { get; set; }

        public Employee IdEmployeeNavigation { get; set; }
    }
}
