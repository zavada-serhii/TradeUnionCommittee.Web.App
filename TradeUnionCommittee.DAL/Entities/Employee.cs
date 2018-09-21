using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.DAL.Entities
{
    public class Employee
    {
        public Employee()
        {
            ActivityEmployees = new HashSet<ActivityEmployees>();
            ApartmentAccountingEmployees = new HashSet<ApartmentAccountingEmployees>();
            AwardEmployees = new HashSet<AwardEmployees>();
            Children = new HashSet<Children>();
            CulturalEmployees = new HashSet<CulturalEmployees>();
            EventEmployees = new HashSet<EventEmployees>();
            Family = new HashSet<Family>();
            FluorographyEmployees = new HashSet<FluorographyEmployees>();
            GiftEmployees = new HashSet<GiftEmployees>();
            GrandChildren = new HashSet<GrandChildren>();
            HobbyEmployees = new HashSet<HobbyEmployees>();
            MaterialAidEmployees = new HashSet<MaterialAidEmployees>();
            PrivateHouseEmployees = new HashSet<PrivateHouseEmployees>();
            PublicHouseEmployees = new HashSet<PublicHouseEmployees>();
        }

        public long Id { get; set; }
        [ConcurrencyCheck]
        public string FirstName { get; set; }
        [ConcurrencyCheck]
        public string SecondName { get; set; }
        [ConcurrencyCheck]
        public string Patronymic { get; set; }
        [ConcurrencyCheck]
        public string Sex { get; set; }
        [ConcurrencyCheck]
        public DateTime BirthDate { get; set; }
        [ConcurrencyCheck]
        public string IdentificationСode { get; set; }
        [ConcurrencyCheck]
        public string MechnikovCard { get; set; }
        [ConcurrencyCheck]
        public string MobilePhone { get; set; }
        [ConcurrencyCheck]
        public string CityPhone { get; set; }
        [ConcurrencyCheck]
        public string BasicProfession { get; set; }
        [ConcurrencyCheck]
        public int StartYearWork { get; set; }
        [ConcurrencyCheck]
        public int? EndYearWork { get; set; }
        [ConcurrencyCheck]
        public DateTime StartDateTradeUnion { get; set; }
        [ConcurrencyCheck]
        public DateTime? EndDateTradeUnion { get; set; }
        [ConcurrencyCheck]
        public string Note { get; set; }
        [ConcurrencyCheck]
        public DateTime DateAdded { get; set; }

        public Education Education { get; set; }
        public PositionEmployees PositionEmployees { get; set; }
        public PrivilegeEmployees PrivilegeEmployees { get; set; }
        public Scientific Scientific { get; set; }
        public SocialActivityEmployees SocialActivityEmployees { get; set; }
        public ICollection<ActivityEmployees> ActivityEmployees { get; set; }
        public ICollection<ApartmentAccountingEmployees> ApartmentAccountingEmployees { get; set; }
        public ICollection<AwardEmployees> AwardEmployees { get; set; }
        public ICollection<Children> Children { get; set; }
        public ICollection<CulturalEmployees> CulturalEmployees { get; set; }
        public ICollection<EventEmployees> EventEmployees { get; set; }
        public ICollection<Family> Family { get; set; }
        public ICollection<FluorographyEmployees> FluorographyEmployees { get; set; }
        public ICollection<GiftEmployees> GiftEmployees { get; set; }
        public ICollection<GrandChildren> GrandChildren { get; set; }
        public ICollection<HobbyEmployees> HobbyEmployees { get; set; }
        public ICollection<MaterialAidEmployees> MaterialAidEmployees { get; set; }
        public ICollection<PrivateHouseEmployees> PrivateHouseEmployees { get; set; }
        public ICollection<PublicHouseEmployees> PublicHouseEmployees { get; set; }
    }
}
