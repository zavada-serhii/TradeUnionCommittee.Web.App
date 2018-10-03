using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeUnionCommittee.DAL.Entities
{
    public class GiftGrandChildrens
    {
        public long Id { get; set; }
        public long IdGrandChildren { get; set; }
        public string NameEvent { get; set; }
        public string NameGifts { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public DateTime DateGift { get; set; }
        [Timestamp]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column("xmin", TypeName = "xid")]
        public uint RowVersion { get; set; }

        public GrandChildren IdGrandChildrenNavigation { get; set; }
    }
}
