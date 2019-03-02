using System;

namespace TradeUnionCommittee.BLL.DTO.Employee
{
    public class ActivityEmployeesDTO
    {
        public string HashId { get; set; }
        public string HashIdEmployee { get; set; }
        public string HashIdActivities { get; set; }
        public string NameActivities { get; set; }
        public DateTime DateEvent { get; set; }
        public uint RowVersion { get; set; }
    }
}