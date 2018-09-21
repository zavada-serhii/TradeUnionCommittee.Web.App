using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.DAL.Entities
{
    public class PublicHouseEmployees
    {
        public long IdAddressPublicHouse { get; set; }
        [ConcurrencyCheck]
        public long IdEmployee { get; set; }
        [ConcurrencyCheck]
        public string NumberRoom { get; set; }

        public AddressPublicHouse IdAddressPublicHouseNavigation { get; set; }
        public Employee IdEmployeeNavigation { get; set; }
    }
}
