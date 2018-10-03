using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeUnionCommittee.DAL.Entities
{
    public class PublicHouseEmployees
    {
        public long IdAddressPublicHouse { get; set; }
        public long IdEmployee { get; set; }
        public string NumberRoom { get; set; }
        [Timestamp]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column("xmin", TypeName = "xid")]
        public uint RowVersion { get; set; }

        public AddressPublicHouse IdAddressPublicHouseNavigation { get; set; }
        public Employee IdEmployeeNavigation { get; set; }
    }
}
