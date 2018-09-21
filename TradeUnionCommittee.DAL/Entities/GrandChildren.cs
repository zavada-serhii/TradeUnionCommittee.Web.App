using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.DAL.Entities
{
    public class GrandChildren
    {
        public GrandChildren()
        {
            ActivityGrandChildrens = new HashSet<ActivityGrandChildrens>();
            CulturalGrandChildrens = new HashSet<CulturalGrandChildrens>();
            EventGrandChildrens = new HashSet<EventGrandChildrens>();
            GiftGrandChildrens = new HashSet<GiftGrandChildrens>();
            HobbyGrandChildrens = new HashSet<HobbyGrandChildrens>();
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
        public ICollection<ActivityGrandChildrens> ActivityGrandChildrens { get; set; }
        public ICollection<CulturalGrandChildrens> CulturalGrandChildrens { get; set; }
        public ICollection<EventGrandChildrens> EventGrandChildrens { get; set; }
        public ICollection<GiftGrandChildrens> GiftGrandChildrens { get; set; }
        public ICollection<HobbyGrandChildrens> HobbyGrandChildrens { get; set; }
    }
}
