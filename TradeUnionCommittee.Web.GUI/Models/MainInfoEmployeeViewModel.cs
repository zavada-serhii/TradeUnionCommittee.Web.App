using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradeUnionCommittee.Web.GUI.Models
{
    public class MainInfoEmployeeViewModel
    {
        public long IdEmployee { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Patronymic { get; set; }

        public Dictionary<string, string> CollectionSex { get; } = new Dictionary<string, string>
        {
            {"Male","Чоловіча"},
            {"Female","Жіноча"}
        };
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

        public string ScientifickDegree { get; set; }
        public string ScientifickTitle { get; set; }

        public string LevelEducation { get; set; }
        public string NameInstitution { get; set; }
        public int? YearReceiving { get; set; }
    }
}
