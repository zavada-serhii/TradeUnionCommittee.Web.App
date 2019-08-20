using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeUnionCommittee.DAL.Entities
{
    public class AwardEmployees
    {
        public long Id { get; set; }
        public long IdEmployee { get; set; }
        public long IdAward { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateIssue { get; set; }
        [Timestamp]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column("xmin", TypeName = "xid")]
        public uint RowVersion { get; set; }


        public Award IdAwardNavigation { get; set; }
        public Employee IdEmployeeNavigation { get; set; }
    }
}
