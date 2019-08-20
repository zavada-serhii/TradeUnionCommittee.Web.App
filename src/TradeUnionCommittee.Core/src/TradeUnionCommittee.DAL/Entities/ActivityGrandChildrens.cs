using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeUnionCommittee.DAL.Entities
{
    public class ActivityGrandChildrens
    {
        public long Id { get; set; }
        public long IdGrandChildren { get; set; }
        public long IdActivities { get; set; }
        public DateTime DateEvent { get; set; }
        [Timestamp]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column("xmin", TypeName = "xid")]
        public uint RowVersion { get; set; }

        public Activities IdActivitiesNavigation { get; set; }
        public GrandChildren IdGrandChildrenNavigation { get; set; }
    }
}
