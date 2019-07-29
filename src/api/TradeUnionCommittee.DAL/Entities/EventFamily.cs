using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeUnionCommittee.DAL.Entities
{
    public class EventFamily
    {
        public long Id { get; set; }
        public long IdFamily { get; set; }
        public long IdEvent { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [Timestamp]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column("xmin", TypeName = "xid")]
        public uint RowVersion { get; set; }

        public Event IdEventNavigation { get; set; }
        public Family IdFamilyNavigation { get; set; }
    }
}
