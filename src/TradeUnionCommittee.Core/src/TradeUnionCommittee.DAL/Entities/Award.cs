using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeUnionCommittee.DAL.Entities
{
    public class Award
    {
        public Award()
        {
            AwardEmployees = new HashSet<AwardEmployees>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        [Timestamp]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column("xmin", TypeName = "xid")]
        public uint RowVersion { get; set; }

        public ICollection<AwardEmployees> AwardEmployees { get; set; }
    }
}
