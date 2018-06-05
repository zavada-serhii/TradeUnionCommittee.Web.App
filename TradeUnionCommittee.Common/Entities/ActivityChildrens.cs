using System;

namespace TradeUnionCommittee.Common.Entities
{
    public class ActivityChildrens
    {
        public long Id { get; set; }
        public long IdChildren { get; set; }
        public long IdActivities { get; set; }
        public DateTime DateEvent { get; set; }

        public Activities IdActivitiesNavigation { get; set; }
        public Children IdChildrenNavigation { get; set; }
    }
}
