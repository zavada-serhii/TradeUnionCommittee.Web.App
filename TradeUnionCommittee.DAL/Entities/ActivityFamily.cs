using System;

namespace TradeUnionCommittee.DAL.Entities
{
    public class ActivityFamily
    {
        public long Id { get; set; }
        public long IdFamily { get; set; }
        public long IdActivities { get; set; }
        public DateTime DateEvent { get; set; }

        public Activities IdActivitiesNavigation { get; set; }
        public Family IdFamilyNavigation { get; set; }
    }
}
