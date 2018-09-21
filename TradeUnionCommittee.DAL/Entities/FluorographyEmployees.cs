using System;
using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.DAL.Entities
{
    public class FluorographyEmployees
    {
        public long Id { get; set; }
        [ConcurrencyCheck]
        public long IdEmployee { get; set; }
        [ConcurrencyCheck]
        public string PlacePassing { get; set; }
        [ConcurrencyCheck]
        public string Result { get; set; }
        [ConcurrencyCheck]
        public DateTime DatePassage { get; set; }

        public Employee IdEmployeeNavigation { get; set; }
    }
}
