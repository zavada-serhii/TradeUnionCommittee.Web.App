using System;

namespace TradeUnionCommittee.DAL.Entities
{
    public class FluorographyEmployees
    {
        public long Id { get; set; }
        public long IdEmployee { get; set; }
        public string PlacePassing { get; set; }
        public string Result { get; set; }
        public DateTime DatePassage { get; set; }

        public Employee IdEmployeeNavigation { get; set; }
    }
}
