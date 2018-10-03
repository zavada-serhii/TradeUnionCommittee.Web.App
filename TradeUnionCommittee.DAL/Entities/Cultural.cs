using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeUnionCommittee.DAL.Entities
{
    public class Cultural
    {
        public Cultural()
        {
            CulturalChildrens = new HashSet<CulturalChildrens>();
            CulturalEmployees = new HashSet<CulturalEmployees>();
            CulturalFamily = new HashSet<CulturalFamily>();
            CulturalGrandChildrens = new HashSet<CulturalGrandChildrens>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        [Timestamp]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column("xmin", TypeName = "xid")]
        public uint RowVersion { get; set; }

        public ICollection<CulturalChildrens> CulturalChildrens { get; set; }
        public ICollection<CulturalEmployees> CulturalEmployees { get; set; }
        public ICollection<CulturalFamily> CulturalFamily { get; set; }
        public ICollection<CulturalGrandChildrens> CulturalGrandChildrens { get; set; }
    }
}
