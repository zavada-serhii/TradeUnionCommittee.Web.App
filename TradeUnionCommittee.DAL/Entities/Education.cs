using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.DAL.Entities
{
    public class Education
    {
        public long Id { get; set; }
        [ConcurrencyCheck]
        public long IdEmployee { get; set; }
        [ConcurrencyCheck]
        public string LevelEducation { get; set; }
        [ConcurrencyCheck]
        public string NameInstitution { get; set; }
        [ConcurrencyCheck]
        public int? YearReceiving { get; set; }

        public Employee IdEmployeeNavigation { get; set; }
    }
}
