using System.Collections.Generic;

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
        public long? IdSubordinate { get; set; }
        public string DeptName { get; set; }
        public string Abbreviation { get; set; }

        public Subdivisions IdSubordinateNavigation { get; set; }
        public ICollection<Subdivisions> InverseIdSubordinateNavigation { get; set; }
        public ICollection<PositionEmployees> PositionEmployees { get; set; }
    }
}
