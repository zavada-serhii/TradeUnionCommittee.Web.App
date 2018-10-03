using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeUnionCommittee.DAL.Entities
{
    public class SocialActivity
    {
        public SocialActivity()
        {
            SocialActivityEmployees = new HashSet<SocialActivityEmployees>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        [Timestamp]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column("xmin", TypeName = "xid")]
        public uint RowVersion { get; set; }

        public ICollection<SocialActivityEmployees> SocialActivityEmployees { get; set; }
    }
}
