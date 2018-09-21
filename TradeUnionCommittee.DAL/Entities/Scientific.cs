using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.DAL.Entities
{
    public class Scientific
    {
        public long Id { get; set; }
        [ConcurrencyCheck]
        public long IdEmployee { get; set; }
        [ConcurrencyCheck]
        public string ScientificDegree { get; set; }
        [ConcurrencyCheck]
        public string ScientificTitle { get; set; }

        public Employee IdEmployeeNavigation { get; set; }
    }
}
