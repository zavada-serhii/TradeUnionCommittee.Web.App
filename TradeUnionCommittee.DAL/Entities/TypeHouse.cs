using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.DAL.Entities
{
    public class TypeHouse
    {
        public TypeHouse()
        {
            AddressPublicHouse = new HashSet<AddressPublicHouse>();
        }

        public long Id { get; set; }
        [ConcurrencyCheck]
        public string Name { get; set; }

        public ICollection<AddressPublicHouse> AddressPublicHouse { get; set; }
    }
}
