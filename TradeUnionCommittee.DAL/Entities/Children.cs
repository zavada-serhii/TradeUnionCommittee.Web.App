using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [ConcurrencyCheck]
        public long IdEmployee { get; set; }
        [ConcurrencyCheck]
        public string FirstName { get; set; }
        [ConcurrencyCheck]
        public string SecondName { get; set; }
        [ConcurrencyCheck]
        public string Patronymic { get; set; }
        [ConcurrencyCheck]
        public DateTime BirthDate { get; set; }

        public Employee IdEmployeeNavigation { get; set; }
        public ICollection<ActivityChildrens> ActivityChildrens { get; set; }
        public ICollection<CulturalChildrens> CulturalChildrens { get; set; }
        public ICollection<EventChildrens> EventChildrens { get; set; }
        public ICollection<GiftChildrens> GiftChildrens { get; set; }
        public ICollection<HobbyChildrens> HobbyChildrens { get; set; }
    }
}
