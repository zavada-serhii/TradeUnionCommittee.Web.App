using System;

namespace TradeUnionCommittee.DAL.Entities
{
    public class ApartmentAccountingEmployees
    {
        public long Id { get; set; }
        public long IdEmployee { get; set; }
        public long FamilyComposition { get; set; }
        public string NameAdministration { get; set; }
        public string PriorityType { get; set; }
        public DateTime DateAdoption { get; set; }
        public DateTime? DateInclusion { get; set; }
        public string Position { get; set; }
        public int StartYearWork { get; set; }

        public Employee IdEmployeeNavigation { get; set; }
    }
}
