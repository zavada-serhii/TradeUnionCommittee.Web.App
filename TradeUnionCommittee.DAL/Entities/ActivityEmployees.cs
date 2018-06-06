using System;

namespace TradeUnionCommittee.DAL.Entities
{
    public class ActivityEmployees
    {
        public long Id { get; set; }
        public long IdEmployee { get; set; }
        public long IdActivities { get; set; }
        public DateTime DateEvent { get; set; }

        public Activities IdActivitiesNavigation { get; set; }
        public Employee IdEmployeeNavigation { get; set; }
    }
}
