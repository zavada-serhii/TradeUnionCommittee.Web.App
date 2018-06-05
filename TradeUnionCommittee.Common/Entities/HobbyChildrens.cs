namespace TradeUnionCommittee.Common.Entities
{
    public class HobbyChildrens
    {
        public long Id { get; set; }
        public long IdChildren { get; set; }
        public long IdHobby { get; set; }

        public Children IdChildrenNavigation { get; set; }
        public Hobby IdHobbyNavigation { get; set; }
    }
}
