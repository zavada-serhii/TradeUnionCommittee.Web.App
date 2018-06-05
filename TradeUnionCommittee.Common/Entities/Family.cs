using System;
using System.Collections.Generic;

namespace TradeUnionCommittee.Common.Entities
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
        public long IdEmployee { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Patronymic { get; set; }
        public DateTime? BirthDate { get; set; }

        public Employee IdEmployeeNavigation { get; set; }
        public ICollection<ActivityFamily> ActivityFamily { get; set; }
        public ICollection<CulturalFamily> CulturalFamily { get; set; }
        public ICollection<EventFamily> EventFamily { get; set; }
    }
}
