using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeUnionCommittee.DAL.Entities
{
    public class Scientific
    {
        public long Id { get; set; }
        public long IdEmployee { get; set; }
        public string ScientificDegree { get; set; }
        public string ScientificTitle { get; set; }
        [Timestamp]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column("xmin", TypeName = "xid")]
        public uint RowVersion { get; set; }

        public Employee IdEmployeeNavigation { get; set; }
    }
}
