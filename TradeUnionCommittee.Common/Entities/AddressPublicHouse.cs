using System.Collections.Generic;

namespace TradeUnionCommittee.Common.Entities
{
    public class AddressPublicHouse
    {
        public AddressPublicHouse()
        {
            PublicHouseEmployees = new HashSet<PublicHouseEmployees>();
        }

        public long Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string NumberHouse { get; set; }
        public string NumberDormitory { get; set; }
        public long Type { get; set; }

        public TypeHouse TypeNavigation { get; set; }
        public ICollection<PublicHouseEmployees> PublicHouseEmployees { get; set; }
    }
}
