using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeUnionCommittee.DAL.Entities
{
    public class PrivateHouseEmployees
    {
        public long Id { get; set; }
        public long IdEmployee { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string NumberHouse { get; set; }
        public string NumberApartment { get; set; }
        public DateTime? DateReceiving { get; set; }
        [Timestamp]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column("xmin", TypeName = "xid")]
        public uint RowVersion { get; set; }

        public Employee IdEmployeeNavigation { get; set; }
    }
}
