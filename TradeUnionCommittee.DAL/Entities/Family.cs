using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.DAL.Entities
{
    public class Family
    {
        public Family()
        {
            ActivityFamily = new HashSet<ActivityFamily>();
            CulturalFamily = new HashSet<CulturalFamily>();
            EventFamily = new HashSet<EventFamily>();
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
        public DateTime? BirthDate { get; set; }

        public Employee IdEmployeeNavigation { get; set; }
        public ICollection<ActivityFamily> ActivityFamily { get; set; }
        public ICollection<CulturalFamily> CulturalFamily { get; set; }
        public ICollection<EventFamily> EventFamily { get; set; }
    }
}
