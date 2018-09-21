using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.DAL.Entities
{
    public class Subdivisions
    {
        public Subdivisions()
        {
            InverseIdSubordinateNavigation = new HashSet<Subdivisions>();
            PositionEmployees = new HashSet<PositionEmployees>();
        }

        public long Id { get; set; }
        [ConcurrencyCheck]
        public long? IdSubordinate { get; set; }
        [ConcurrencyCheck]
        public string Name { get; set; }
        [ConcurrencyCheck]
        public string Abbreviation { get; set; }

        public Subdivisions IdSubordinateNavigation { get; set; }
        public ICollection<Subdivisions> InverseIdSubordinateNavigation { get; set; }
        public ICollection<PositionEmployees> PositionEmployees { get; set; }
    }
}
