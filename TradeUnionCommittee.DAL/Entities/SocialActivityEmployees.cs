using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeUnionCommittee.DAL.Entities
{
    public class SocialActivityEmployees
    {
        public long Id { get; set; }
        public long IdEmployee { get; set; }
        public long IdSocialActivity { get; set; }
        public string Note { get; set; }
        public bool CheckSocialActivity { get; set; }
        [Timestamp]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column("xmin", TypeName = "xid")]
        public uint RowVersion { get; set; }

        public Employee IdEmployeeNavigation { get; set; }
        public SocialActivity IdSocialActivityNavigation { get; set; }
    }
}
