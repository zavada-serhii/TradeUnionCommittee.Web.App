using System;

namespace TradeUnionCommittee.BLL.DTO.GrandChildren
{
    public class ActivityGrandChildrenDTO
    {
        public string HashId { get; set; }
        public string HashIdGrandChildren { get; set; }
        public string HashIdActivities { get; set; }
        public string NameActivities { get; set; }
        public DateTime DateEvent { get; set; }
        public uint RowVersion { get; set; }
    }
}