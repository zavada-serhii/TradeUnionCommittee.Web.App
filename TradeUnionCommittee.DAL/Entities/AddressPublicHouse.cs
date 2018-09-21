using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.DAL.Entities
{
    public class AddressPublicHouse
    {
        public AddressPublicHouse()
        {
            PublicHouseEmployees = new HashSet<PublicHouseEmployees>();
        }

        public long Id { get; set; }
        [ConcurrencyCheck]
        public string City { get; set; }
        [ConcurrencyCheck]
        public string Street { get; set; }
        [ConcurrencyCheck]
        public string NumberHouse { get; set; }
        [ConcurrencyCheck]
        public string NumberDormitory { get; set; }
        [ConcurrencyCheck]
        public long Type { get; set; }

        public TypeHouse TypeNavigation { get; set; }
        public ICollection<PublicHouseEmployees> PublicHouseEmployees { get; set; }
    }
}
