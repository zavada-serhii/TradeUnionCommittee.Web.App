using System;
using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.DAL.Entities
{
    public class PrivateHouseEmployees
    {
        public long Id { get; set; }
        [ConcurrencyCheck]
        public long IdEmployee { get; set; }
        [ConcurrencyCheck]
        public string City { get; set; }
        [ConcurrencyCheck]
        public string Street { get; set; }
        [ConcurrencyCheck]
        public string NumberHouse { get; set; }
        [ConcurrencyCheck]
        public string NumberApartment { get; set; }
        [ConcurrencyCheck]
        public DateTime? DateReceiving { get; set; }

        public Employee IdEmployeeNavigation { get; set; }
    }
}
