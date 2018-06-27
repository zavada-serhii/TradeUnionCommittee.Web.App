using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace TradeUnionCommittee.GUI.Models.ViewModels
{
    public class AddEmployeeViewModel
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Patronymic { get; set; }
        public Dictionary<string, string> CollectionSex { get; } = new Dictionary<string, string>
        {
            {"Male","Чоловіча"},
            {"Female","Жіноча"}
        };
        public string Sex { get; set; }
        public string BasicProfission { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? StartYearWork { get; set; }
        public DateTime? StartDateTradeUnion { get; set; }
        [Remote("CheckIdentificationСode", "NewEmployees", ErrorMessage = "Цей ІНН вже використовуеться")]
        public string IdentificationСode { get; set; }
        [Remote("CheckMechnikovCard", "NewEmployees", ErrorMessage = "Цей номер Mechnikov Card вже використовуеться")]
        public string MechnikovCard { get; set; }
        public string MobilePhone { get; set; }
        public string CityPhone { get; set; }
        public string CityPhoneAdditional { get; set; }
        public string Note { get; set; }

        public string LevelEducation { get; set; }
        public string NameInstitution { get; set; }
        public int? YearReceiving { get; set; }

        public int? Position { get; set; }
        public DateTime? StartDatePosition { get; set; }
        public int? MainSubdivision { get; set; }
        public int? SubordinateSubdivision { get; set; }

        //------------------------------------------------------------------------------------------------------------------------------------------

        public Dictionary<string,string> CollectionAccommodation { get; } = new Dictionary<string, string>
        {
            {"privateHouse","Приватне житло"},
            {"fromUniversity","Від університету"},
            {"dormitory","Гуртожиток"},
            {"departmental","Відомче"}
        };
        public string TypeAccommodation { get; set; }

        public string CityPrivateHouse { get; set; }
        public string StreetPrivateHouse { get; set; }
        public string NumberHousePrivateHouse { get; set; }
        public string NumberApartmentPrivateHouse { get; set; }

        public string CityHouseUniversity { get; set; }
        public string StreetHouseUniversity { get; set; }
        public string NumberHouseUniversity { get; set; }
        public string NumberApartmentHouseUniversity { get; set; }
        public DateTime? DateReceivingHouseFromUniversity { get; set; }

        public int? IdDormitory { get; set; }
        public string NumberRoomDormitory { get; set; }

        public int? IdDepartmental { get; set; }
        public string NumberRoomDepartmental { get; set; }

        //------------------------------------------------------------------------------------------------------------------------------------------

        public bool Scientifick { get; set; }
        public string ScientifickDegree { get; set; }
        public string ScientifickTitle { get; set; }

        public bool SocialActivity { get; set; }
        public int? IdSocialActivity { get; set; }
        public string NoteSocialActivity { get; set; }

        public bool Privileges { get; set; }
        public int? IdPrivileges { get; set; }
        public string NotePrivileges { get; set; }
    }
}