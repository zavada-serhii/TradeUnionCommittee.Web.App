using System.Collections.Generic;

namespace TradeUnionCommittee.DAL.Entities
{
    public class TypeHouse
    {
        public TypeHouse()
        {
            AddressPublicHouse = new HashSet<AddressPublicHouse>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public ICollection<AddressPublicHouse> AddressPublicHouse { get; set; }
    }
}
