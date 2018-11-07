using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Patronymic { get; set; }
        public string Sex { get; set; }
        public DateTime BirthDate { get; set; }
        public string IdentificationСode { get; set; }
        public string MechnikovCard { get; set; }
        public string MobilePhone { get; set; }
        public string CityPhone { get; set; }
        public string BasicProfession { get; set; }
        public int StartYearWork { get; set; }
        public int? EndYearWork { get; set; }
        public DateTime StartDateTradeUnion { get; set; }
        public DateTime? EndDateTradeUnion { get; set; }
        public string LevelEducation { get; set; }
        public string NameInstitution { get; set; }
        public int? YearReceiving { get; set; }
        public string ScientificDegree { get; set; }
        public string ScientificTitle { get; set; }
        public string Note { get; set; }
        public DateTime DateAdded { get; set; }
        [Timestamp]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column("xmin", TypeName = "xid")]
        public uint RowVersion { get; set; }

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
