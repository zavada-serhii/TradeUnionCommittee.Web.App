using System;
using TradeUnionCommittee.BLL.Enums;

namespace TradeUnionCommittee.BLL.DTO.Employee
{
    public class GeneralInfoEmployeeDTO
    {
        public string HashIdEmployee { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Patronymic { get; set; }
        public string Sex { get; set; }
        public DateTime BirthDate { get; set; }
        public int CountYear { get; set; }
        public string IdentificationCode { get; set; }
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

    public class CreateEmployeeDTO
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Patronymic { get; set; }
        public string Sex { get; set; }
        public string BasicProfession { get; set; }
        public DateTime BirthDate { get; set; }
        public int StartYearWork { get; set; }
        public DateTime StartDateTradeUnion { get; set; }
        public string IdentificationCode { get; set; }
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
        public DateTime StartDatePosition { get; set; }
        public string HashIdSubdivision { get; set; }

        //------------------------------------------------------------------------------------------------------------------------------------------

        public AccommodationType TypeAccommodation { get; set; }

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
}