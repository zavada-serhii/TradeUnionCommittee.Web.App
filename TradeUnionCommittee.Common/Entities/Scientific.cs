namespace TradeUnionCommittee.Common.Entities
{
    public class Scientific
    {
        public long Id { get; set; }
        public long IdEmployee { get; set; }
        public string ScientificDegree { get; set; }
        public string ScientificTitle { get; set; }

        public Employee IdEmployeeNavigation { get; set; }
    }
}
