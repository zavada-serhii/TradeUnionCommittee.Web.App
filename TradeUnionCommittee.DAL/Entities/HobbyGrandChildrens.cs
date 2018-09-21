using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.DAL.Entities
{
    public class HobbyGrandChildrens
    {
        public long Id { get; set; }
        [ConcurrencyCheck]
        public long IdGrandChildren { get; set; }
        [ConcurrencyCheck]
        public long IdHobby { get; set; }

        public GrandChildren IdGrandChildrenNavigation { get; set; }
        public Hobby IdHobbyNavigation { get; set; }
    }
}
