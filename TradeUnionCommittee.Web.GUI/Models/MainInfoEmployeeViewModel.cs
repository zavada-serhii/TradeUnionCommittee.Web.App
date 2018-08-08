using System;

namespace TradeUnionCommittee.Web.GUI.Models
{
    public class MainInfoEmployeeViewModel
    {
        public long? IdEmployee { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Patronymic { get; set; }
        public string Sex { get; set; }

        public DateTime BirthDate { get; set; }
        public string IdentificationСode { get; set; }
        public string MechnikovCard { get; set; }
        public string MobilePhone { get; set; }
        public string CityPhone { get; set; }
        public string Note { get; set; }

        public string BasicProfession { get; set; }
        public int StartYearWork { get; set; }
        public int EndYearWork { get; set; }

        public DateTime StartDateTradeUnion { get; set; }
        public DateTime? EndDateTradeUnion { get; set; }
    }
}
