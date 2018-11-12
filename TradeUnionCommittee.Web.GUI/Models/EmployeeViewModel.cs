using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace TradeUnionCommittee.Web.GUI.Models
{
    public class CreateEmployeeViewModel
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
        public string BasicProfession { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? StartYearWork { get; set; }
        public DateTime? StartDateTradeUnion { get; set; }
        [Remote("CheckIdentificationСode", "Employee", ErrorMessage = "Цей ІНН вже використовуеться!")]
        public string IdentificationСode { get; set; }
        [Remote("CheckMechnikovCard", "Employee", ErrorMessage = "Цей номер Mechnikov Card вже використовуеться!")]
        public string MechnikovCard { get; set; }
        public string MobilePhone { get; set; }
        public string CityPhone { get; set; }
        public string Note { get; set; }

        public string LevelEducation { get; set; }
        public string NameInstitution { get; set; }
        public int? YearReceiving { get; set; }

        public string ScientificDegree { get; set; }
        public string ScientificTitle { get; set; }

        public string HashIdPosition { get; set; }
        public DateTime? StartDatePosition { get; set; }
        public string HashIdMainSubdivision { get; set; }
        public string HashIdSubordinateSubdivision { get; set; }

        //------------------------------------------------------------------------------------------------------------------------------------------

        public Dictionary<string, string> CollectionAccommodation { get; } = new Dictionary<string, string>
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

        public string HashIdDormitory { get; set; }
        public string NumberRoomDormitory { get; set; }

        public string HashIdDepartmental { get; set; }
        public string NumberRoomDepartmental { get; set; }

        //------------------------------------------------------------------------------------------------------------------------------------------
        
        public bool SocialActivity { get; set; }
        public string HashIdSocialActivity { get; set; }
        public string NoteSocialActivity { get; set; }

        public bool Privileges { get; set; }
        public string HashIdPrivileges { get; set; }
        public string NotePrivileges { get; set; }
    }

    public class UpdateEmployeeViewModel
    {
        public string HashIdEmployee { get; set; }
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
        public int? EndYearWork { get; set; }

        public DateTime StartDateTradeUnion { get; set; }
        public DateTime? EndDateTradeUnion { get; set; }

        public string ScientificDegree { get; set; }
        public string ScientificTitle { get; set; }

        public string LevelEducation { get; set; }
        public string NameInstitution { get; set; }
        public int? YearReceiving { get; set; }

        public uint RowVersion { get; set; }
    }
}