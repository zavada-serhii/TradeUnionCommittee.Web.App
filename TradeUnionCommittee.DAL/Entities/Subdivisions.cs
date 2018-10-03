using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        [Timestamp]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column("xmin", TypeName = "xid")]
        public uint RowVersion { get; set; }

        public Subdivisions IdSubordinateNavigation { get; set; }
        public ICollection<Subdivisions> InverseIdSubordinateNavigation { get; set; }
        public ICollection<PositionEmployees> PositionEmployees { get; set; }
    }
}
