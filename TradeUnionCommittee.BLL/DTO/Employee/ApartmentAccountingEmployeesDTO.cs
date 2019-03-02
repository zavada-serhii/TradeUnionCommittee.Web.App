using System;

namespace TradeUnionCommittee.BLL.DTO.Employee
{
    public class ApartmentAccountingEmployeesDTO
    {
        public string HashId { get; set; }
        public string HashIdEmployee { get; set; }
        public long FamilyComposition { get; set; }
        public string NameAdministration { get; set; }
        public string PriorityType { get; set; }
        public DateTime DateAdoption { get; set; }
        public DateTime? DateInclusion { get; set; }
        public string Position { get; set; }
        public int StartYearWork { get; set; }
        public uint RowVersion { get; set; }
    }
}