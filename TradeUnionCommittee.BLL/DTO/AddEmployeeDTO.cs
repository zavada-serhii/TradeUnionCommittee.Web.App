using System;

namespace TradeUnionCommittee.BLL.DTO
{
    public class AddEmployeeDTO
    {
        public long IdEmployee { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Patronymic { get; set; }
        public string Sex { get; set; }
        public string BasicProfission { get; set; }
        public DateTime BirthDate { get; set; }
        public int StartYearWork { get; set; }
        public DateTime StartDateTradeUnion { get; set; }
        public string IdentificationСode { get; set; }
        public string MechnikovCard { get; set; }
        public string MobilePhone { get; set; }
        public string CityPhone { get; set; }
        public string Note { get; set; }

        public string LevelEducation { get; set; }
        public string NameInstitution { get; set; }
        public int? YearReceiving { get; set; }

        public long Position { get; set; }
        public DateTime StartDatePosition { get; set; }
        public long IdSubdivision { get; set; }

        //------------------------------------------------------------------------------------------------------------------------------------------
        
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

        public long IdDormitory { get; set; }
        public string NumberRoomDormitory { get; set; }

        public long IdDepartmental { get; set; }
        public string NumberRoomDepartmental { get; set; }

        //------------------------------------------------------------------------------------------------------------------------------------------

        public bool Scientifick { get; set; }
        public string ScientifickDegree { get; set; }
        public string ScientifickTitle { get; set; }

        public bool SocialActivity { get; set; }
        public long IdSocialActivity { get; set; }
        public string NoteSocialActivity { get; set; }

        public bool Privileges { get; set; }
        public long IdPrivileges { get; set; }
        public string NotePrivileges { get; set; }
    }
}