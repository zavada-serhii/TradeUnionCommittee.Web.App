namespace TradeUnionCommittee.DAL.Entities
{
    public class Education
    {
        public long Id { get; set; }
        public long IdEmployee { get; set; }
        public string LevelEducation { get; set; }
        public string NameInstitution { get; set; }
        public int? YearReceiving { get; set; }

        public Employee IdEmployeeNavigation { get; set; }
    }
}
