namespace TradeUnionCommittee.Common.Entities
{
    public class HobbyGrandChildrens
    {
        public long Id { get; set; }
        public long IdGrandChildren { get; set; }
        public long IdHobby { get; set; }

        public GrandChildren IdGrandChildrenNavigation { get; set; }
        public Hobby IdHobbyNavigation { get; set; }
    }
}
