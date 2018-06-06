namespace TradeUnionCommittee.DAL.Entities
{
    public class PublicHouseEmployees
    {
        public long IdAddressPublicHouse { get; set; }
        public long IdEmployee { get; set; }
        public string NumberRoom { get; set; }

        public AddressPublicHouse IdAddressPublicHouseNavigation { get; set; }
        public Employee IdEmployeeNavigation { get; set; }
    }
}
