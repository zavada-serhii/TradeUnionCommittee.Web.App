using System;

namespace TradeUnionCommittee.BLL.DTO
{
    public class ActivityFamilyDTO
    {
        public string HashId { get; set; }
        public string HashIdFamily { get; set; }
        public string HashIdActivities { get; set; }
        public string NameActivities { get; set; }
        public DateTime DateEvent { get; set; }
        public uint RowVersion { get; set; }
    }
}