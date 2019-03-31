using System;

namespace TradeUnionCommittee.BLL.DTO.Employee
{
    public class PositionEmployeesDTO
    {
        public string HashId { get; set; }
        public string HashIdEmployee { get; set; }
        public string HashIdSubdivision { get; set; }
        public string HashIdPosition { get; set; }
        public bool CheckPosition { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public uint RowVersion { get; set; }
    }
}