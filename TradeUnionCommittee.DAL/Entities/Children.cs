using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeUnionCommittee.DAL.Entities
{
    public class Children
    {
        public Children()
        {
            ActivityChildrens = new HashSet<ActivityChildrens>();
            CulturalChildrens = new HashSet<CulturalChildrens>();
            EventChildrens = new HashSet<EventChildrens>();
            GiftChildrens = new HashSet<GiftChildrens>();
            HobbyChildrens = new HashSet<HobbyChildrens>();
        }

        public long Id { get; set; }
        public long IdEmployee { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Patronymic { get; set; }
        public DateTime BirthDate { get; set; }
        [Timestamp]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column("xmin", TypeName = "xid")]
        public uint RowVersion { get; set; }

        public Employee IdEmployeeNavigation { get; set; }
        public ICollection<ActivityChildrens> ActivityChildrens { get; set; }
        public ICollection<CulturalChildrens> CulturalChildrens { get; set; }
        public ICollection<EventChildrens> EventChildrens { get; set; }
        public ICollection<GiftChildrens> GiftChildrens { get; set; }
        public ICollection<HobbyChildrens> HobbyChildrens { get; set; }
    }
}
