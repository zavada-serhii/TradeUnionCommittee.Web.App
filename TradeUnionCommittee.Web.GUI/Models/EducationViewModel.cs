namespace TradeUnionCommittee.Web.GUI.Models
{
    public class EducationViewModel
    {
        public long IdEducation { get; set; }
        public long? IdEmployee { get; set; }
        public string LevelEducation { get; set; }
        public string NameInstitution { get; set; }
        public int? YearReceiving { get; set; }
    }
}