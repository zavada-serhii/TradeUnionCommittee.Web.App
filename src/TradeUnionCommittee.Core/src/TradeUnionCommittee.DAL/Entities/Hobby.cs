using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeUnionCommittee.DAL.Entities
{
    public class Hobby
    {
        public Hobby()
        {
            HobbyChildrens = new HashSet<HobbyChildrens>();
            HobbyEmployees = new HashSet<HobbyEmployees>();
            HobbyGrandChildrens = new HashSet<HobbyGrandChildrens>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        [Timestamp]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column("xmin", TypeName = "xid")]
        public uint RowVersion { get; set; }

        public ICollection<HobbyChildrens> HobbyChildrens { get; set; }
        public ICollection<HobbyEmployees> HobbyEmployees { get; set; }
        public ICollection<HobbyGrandChildrens> HobbyGrandChildrens { get; set; }
    }
}
