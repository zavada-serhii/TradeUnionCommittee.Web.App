using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeUnionCommittee.DAL.Entities
{
    public class ActivityChildrens
    {
        public long Id { get; set; }
        public long IdChildren { get; set; }
        public long IdActivities { get; set; }
        public DateTime DateEvent { get; set; }
        [Timestamp]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column("xmin", TypeName = "xid")]
        public uint RowVersion { get; set; }

        public Activities IdActivitiesNavigation { get; set; }
        public Children IdChildrenNavigation { get; set; }
    }
}
