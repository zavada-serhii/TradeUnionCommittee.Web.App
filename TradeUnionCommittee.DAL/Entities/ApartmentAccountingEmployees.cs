using System;
using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.DAL.Entities
{
    public class ApartmentAccountingEmployees
    {
        public long Id { get; set; }
        [ConcurrencyCheck]
        public long IdEmployee { get; set; }
        [ConcurrencyCheck]
        public long FamilyComposition { get; set; }
        [ConcurrencyCheck]
        public string NameAdministration { get; set; }
        [ConcurrencyCheck]
        public string PriorityType { get; set; }
        [ConcurrencyCheck]
        public DateTime DateAdoption { get; set; }
        [ConcurrencyCheck]
        public DateTime? DateInclusion { get; set; }
        [ConcurrencyCheck]
        public string Position { get; set; }
        [ConcurrencyCheck]
        public int StartYearWork { get; set; }

        public Employee IdEmployeeNavigation { get; set; }
    }
}
