namespace TradeUnionCommittee.Common.Entities
{
    public class Education
    {
        public long Id { get; set; }
        public long IdEmployee { get; set; }
        public string LevelEducation { get; set; }
        public string NameInstitution { get; set; }
        public int? DateReceiving { get; set; }

        public Employee IdEmployeeNavigation { get; set; }
    }
}
