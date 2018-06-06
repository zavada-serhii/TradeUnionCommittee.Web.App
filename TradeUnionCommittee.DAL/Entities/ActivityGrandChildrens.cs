using System;

namespace TradeUnionCommittee.DAL.Entities
{
    public class ActivityGrandChildrens
    {
        public long Id { get; set; }
        public long IdGrandChildren { get; set; }
        public long IdActivities { get; set; }
        public DateTime DateEvent { get; set; }

        public Activities IdActivitiesNavigation { get; set; }
        public GrandChildren IdGrandchildrenNavigation { get; set; }
    }
}
