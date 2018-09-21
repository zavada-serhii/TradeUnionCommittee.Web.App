using System;
using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.DAL.Entities
{
    public class ActivityEmployees
    {
        public long Id { get; set; }
        [ConcurrencyCheck]
        public long IdEmployee { get; set; }
        [ConcurrencyCheck]
        public long IdActivities { get; set; }
        [ConcurrencyCheck]
        public DateTime DateEvent { get; set; }

        public Activities IdActivitiesNavigation { get; set; }
        public Employee IdEmployeeNavigation { get; set; }
    }
}
